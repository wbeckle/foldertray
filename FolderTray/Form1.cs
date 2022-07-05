using System.Diagnostics;

namespace FolderTray
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        bool LoadMenu()
        {
            if (File.Exists("FolderTray.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines(@"FolderTray.txt");
                if (lines.Length == 0) return false;
                foreach (string line in lines)
                {
                    string name = "";
                    if (string.IsNullOrWhiteSpace(line) || !Directory.Exists(line) || line.Length < 5) continue;
                    name = line.Trim();
                    if (name.EndsWith(@"\")) name = name.Substring(0, line.Length - 1);
                    name = Path.GetFileName(name);
                    ToolStripMenuItem item = new ToolStripMenuItem() { Name = name, Text = name, Tag = name, ToolTipText = line };
                    item = GetMenu(line, item);
                    menuStrip1.Items.Add(item);
                }
                return true;
            }
            return false;
        }

        ToolStripMenuItem GetMenu(string path, ToolStripMenuItem menuItem)
        {
            var files = Directory.EnumerateFiles(path);
            foreach (var file in files)
            {
                string name = Path.GetFileName(file);
                ToolStripMenuItem item = new ToolStripMenuItem() { Tag = file, Name = name, Text = name };
                item.Click += MenuClickHandler;
                menuItem.DropDownItems.Add(item);
            }
            return menuItem;
        }

        private void MenuClickHandler(object? sender, EventArgs e)
        {
            ToolStripItem? item = sender as ToolStripItem;
            if (item is null || item.Tag is null) return;
            if (item.Tag.ToString()!.ToLower().EndsWith("ps1"))
            {
                RunPowerShell(item.Tag.ToString());
            }
            else
            {
                RunFile(item.Tag.ToString());
            }
        }

        void RunFile(string? cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo(cmd);
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        void RunPowerShell(string? cmd)
        {
            if (String.IsNullOrWhiteSpace(cmd)) return;
            ProcessStartInfo psi = new ProcessStartInfo("pwsh.exe", cmd);
            psi.UseShellExecute = true;
            psi.WorkingDirectory = Path.GetDirectoryName(cmd);
            Process.Start(psi);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!LoadMenu()) Application.Exit();
            this.Location = Properties.Settings.Default.WinLocation;
            onTop.Checked = Properties.Settings.Default.onTopCheck;
            this.TopMost = onTop.Checked;
        }

        private void onTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = onTop.Checked;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WinLocation = this.Location;
                Properties.Settings.Default.onTopCheck = onTop.Checked;
                Properties.Settings.Default.Save();
            }
        }
    }
}