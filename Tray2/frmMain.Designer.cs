namespace Tray2
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            treeView = new TreeView();
            notifyIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.BackColor = SystemColors.Control;
            treeView.BorderStyle = BorderStyle.None;
            treeView.Location = new Point(5, 0);
            treeView.Name = "treeView";
            treeView.Size = new Size(261, 438);
            treeView.TabIndex = 0;
            treeView.BeforeExpand += treeView_BeforeExpand;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            treeView.NodeMouseDoubleClick += treeView_NodeMouseDoubleClick;
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "FolderTray2";
            notifyIcon.Visible = true;
            notifyIcon.Click += notifyIcon_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 450);
            Controls.Add(treeView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(100, 50);
            Name = "frmMain";
            ShowInTaskbar = false;
            Text = "FolderTray 2";
            WindowState = FormWindowState.Minimized;
            Load += frmMain_Load;
            SizeChanged += frmMain_SizeChanged;
            ResumeLayout(false);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private TreeView treeView;
        private NotifyIcon notifyIcon;
    }
}