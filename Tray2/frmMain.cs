using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;

namespace Tray2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0 || appSettings.GetValues("paths") is null || appSettings.GetValues("paths")?.Length == 0)
            {
                MessageBox.Show("No paths key found in app config");
                Application.Exit();
                return;
            }

            BuildTreeView(appSettings);
        }

        private void BuildTreeView(NameValueCollection appSettings)
        {
            treeView.Nodes.Clear();
            List<string> paths = appSettings.GetValues("paths")![0].Split(";").ToList<string>();
            foreach (string path in paths)
            {
                addNode(path);
            }
        }

        private void addNode(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return;
            TreeNode node = treeView.Nodes.Add(path);
            var files = Directory.EnumerateFiles(path);
            foreach (var file in files)
            {
                string name = Path.GetFileName(file);
                node.Nodes.Add(name);
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks > 1 || e.Button != MouseButtons.Right) return;
            TreeNode node = e.Node;
            if (!node.Text.Contains('.'))
            {
                // MessageBox.Show(treeView.SelectedNode.Text, "Selected node is not a file");
                BuildTreeView(ConfigurationManager.AppSettings);
                return;
            }
            if (MessageBox.Show(node.Text, "Edit this file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EditFile(node.FullPath);
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView.SelectedNode is null) return;
            TreeNode node = treeView.SelectedNode;
            if (node.Text.EndsWith("ps1"))
            {
                RunPowerShell(node.FullPath);
                return;
            }
            if (node.Text.EndsWith("js"))
            {
                RunJavaScript(node.FullPath);
                return;
            }
            if (node.Text.EndsWith("sh"))
            {
                RunWSL(node.FullPath);
                return;
            }
            RunFile(node.FullPath);
        }

        void EditFile(string cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo("code", cmd);
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        void RunFile(string cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo(cmd);
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        void RunPowerShell(string cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo("pwsh.exe", cmd);
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        void RunJavaScript(string cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo("node.exe", cmd);
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        void RunWSL(string cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            string filename = Path.GetFileName(cmd);
            string path = Path.GetDirectoryName(cmd) ?? String.Empty;
            path = String.IsNullOrWhiteSpace(path) ? string.Empty : $"--cd {path} ";
            ProcessStartInfo psi = new ProcessStartInfo("wsl.exe", $"{path}./{filename}");
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }


        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (frmMain.ActiveForm is null) return;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                return;
            }
            treeView.Width = frmMain.ActiveForm.Width - 25;
            treeView.Height = frmMain.ActiveForm.Height - 45;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                return;
            }
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            int y = Screen.PrimaryScreen.Bounds.Bottom - (this.Height + 50);
            int x = Screen.PrimaryScreen.Bounds.Right - (this.Width + 10);
            this.Location = new Point(x, y);
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView.CollapseAll();
        }
    }
}