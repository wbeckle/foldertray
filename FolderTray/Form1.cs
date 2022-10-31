using System.Diagnostics;
using System.Xml.Linq;

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
                    ToolStripMenuItem topMenuItem = new ToolStripMenuItem() { Name = name, Text = name, Tag = name, ToolTipText = line };
                    topMenuItem = GetSubMenu(line, topMenuItem);
                    contextMenu.Items.Add(topMenuItem);
                }
                ToolStripSeparator separator = new();
                contextMenu.Items.Add(separator);
                ToolStripMenuItem item = new ToolStripMenuItem() { Name = "Exit", Text = "Exit", Tag = "Exit", ToolTipText = "Exit" };
                item.Click += MenuClickHandler;
                contextMenu.Items.Add(item);
                return true;
            }
            return false;
        }

        ToolStripMenuItem GetSubMenu(string path, ToolStripMenuItem menuItem)
        {
            var files = Directory.EnumerateFiles(path);
            foreach (var file in files)
            {
                string name = Path.GetFileName(file);
                ToolStripMenuItem item = new ToolStripMenuItem() { Tag = file, Name = name, Text = name };
                item.Click += MenuClickHandler;
                menuItem.DropDownItems.Add(item);
            }
            ToolStripSeparator separator = new();
            menuItem.DropDownItems.Add(separator);
            ToolStripMenuItem folderItem = new ToolStripMenuItem() { Tag = "OpenFolder", Name = path, Text = "Open Folder" };
            folderItem.Click += MenuClickHandler;
            menuItem.DropDownItems.Add(folderItem);
            return menuItem;
        }

        private void MenuClickHandler(object? sender, EventArgs e)
        {
            ToolStripItem? item = sender as ToolStripItem;
            if (item is null || item.Tag is null) return;
            if (item.Tag.ToString()!.ToLower() == "exit") Application.Exit();
            if (item.Tag.ToString()!.ToLower() == "openfolder")
            {
                OpenFolder(item.Name);
                return;
            }
            if (item.Tag.ToString()!.ToLower().EndsWith("ps1"))
            {
                RunPowerShell(item.Tag.ToString());
            }
            else
            {
                RunFile(item.Tag.ToString());
            }
        }

        void OpenFolder(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) return;
            ProcessStartInfo psi = new ProcessStartInfo("pwsh.exe");
            psi.UseShellExecute = true;
            psi.WorkingDirectory = path;
            Process.Start(psi);
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
            notifyIcon.ContextMenuStrip = contextMenu;
        }
    }
}