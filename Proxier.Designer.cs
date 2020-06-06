namespace Proxier
{
    partial class Proxier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Proxier));
            this.MenuTop = new System.Windows.Forms.MenuStrip();
            this.BtnFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnConfigMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnMenuModifyState = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableProcesses = new BrightIdeasSoftware.FastObjectListView();
            this.PnlAvailableProcesses = new System.Windows.Forms.Panel();
            this.LblSeparator = new System.Windows.Forms.Label();
            this.TxtProcessesFilter = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblInbound = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblOutbound = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblBottomSeparator = new System.Windows.Forms.Label();
            this.LblTopSeparator = new System.Windows.Forms.Label();
            this.RequestsLog = new BrightIdeasSoftware.FastObjectListView();
            this.MenuTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailableProcesses)).BeginInit();
            this.PnlAvailableProcesses.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RequestsLog)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuTop
            // 
            this.MenuTop.BackColor = System.Drawing.Color.White;
            this.MenuTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnFileMenu,
            this.BtnConfigMenu,
            this.toolStripMenuItem1,
            this.BtnMenuModifyState});
            this.MenuTop.Location = new System.Drawing.Point(0, 0);
            this.MenuTop.Name = "MenuTop";
            this.MenuTop.Size = new System.Drawing.Size(870, 24);
            this.MenuTop.TabIndex = 0;
            this.MenuTop.Text = "menuStrip1";
            // 
            // BtnFileMenu
            // 
            this.BtnFileMenu.Image = global::Proxier.Properties.Resources.Exit24;
            this.BtnFileMenu.Name = "BtnFileMenu";
            this.BtnFileMenu.Size = new System.Drawing.Size(54, 20);
            this.BtnFileMenu.Text = "Exit";
            this.BtnFileMenu.Click += new System.EventHandler(this.BtnFileMenu_Click);
            // 
            // BtnConfigMenu
            // 
            this.BtnConfigMenu.Image = global::Proxier.Properties.Resources.Configuration20;
            this.BtnConfigMenu.Name = "BtnConfigMenu";
            this.BtnConfigMenu.Size = new System.Drawing.Size(109, 20);
            this.BtnConfigMenu.Text = "Configuration";
            this.BtnConfigMenu.Click += new System.EventHandler(this.BtnConfigMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // BtnMenuModifyState
            // 
            this.BtnMenuModifyState.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.BtnMenuModifyState.Image = global::Proxier.Properties.Resources.Pause20;
            this.BtnMenuModifyState.Name = "BtnMenuModifyState";
            this.BtnMenuModifyState.Size = new System.Drawing.Size(66, 20);
            this.BtnMenuModifyState.Text = "Pause";
            this.BtnMenuModifyState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMenuModifyState.Click += new System.EventHandler(this.BtnMenuModifyState_Click);
            // 
            // AvailableProcesses
            // 
            this.AvailableProcesses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AvailableProcesses.CellEditUseWholeCell = false;
            this.AvailableProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableProcesses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableProcesses.GridLines = true;
            this.AvailableProcesses.HideSelection = false;
            this.AvailableProcesses.Location = new System.Drawing.Point(0, 25);
            this.AvailableProcesses.Name = "AvailableProcesses";
            this.AvailableProcesses.ShowGroups = false;
            this.AvailableProcesses.ShowItemToolTips = true;
            this.AvailableProcesses.Size = new System.Drawing.Size(256, 354);
            this.AvailableProcesses.TabIndex = 1;
            this.AvailableProcesses.UseCompatibleStateImageBehavior = false;
            this.AvailableProcesses.View = System.Windows.Forms.View.Details;
            this.AvailableProcesses.VirtualMode = true;
            this.AvailableProcesses.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AvailableProcesses_KeyPress);
            // 
            // PnlAvailableProcesses
            // 
            this.PnlAvailableProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PnlAvailableProcesses.Controls.Add(this.AvailableProcesses);
            this.PnlAvailableProcesses.Controls.Add(this.LblSeparator);
            this.PnlAvailableProcesses.Controls.Add(this.TxtProcessesFilter);
            this.PnlAvailableProcesses.Location = new System.Drawing.Point(12, 36);
            this.PnlAvailableProcesses.Name = "PnlAvailableProcesses";
            this.PnlAvailableProcesses.Size = new System.Drawing.Size(256, 379);
            this.PnlAvailableProcesses.TabIndex = 2;
            // 
            // LblSeparator
            // 
            this.LblSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSeparator.Location = new System.Drawing.Point(0, 20);
            this.LblSeparator.Name = "LblSeparator";
            this.LblSeparator.Size = new System.Drawing.Size(256, 5);
            this.LblSeparator.TabIndex = 3;
            // 
            // TxtProcessesFilter
            // 
            this.TxtProcessesFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtProcessesFilter.Location = new System.Drawing.Point(0, 0);
            this.TxtProcessesFilter.Name = "TxtProcessesFilter";
            this.TxtProcessesFilter.Size = new System.Drawing.Size(256, 20);
            this.TxtProcessesFilter.TabIndex = 2;
            this.TxtProcessesFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtProcessesFilter.TextChanged += new System.EventHandler(this.TxtProcessesFilter_TextChanged);
            this.TxtProcessesFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtProcessesFilter_KeyPress);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.LblStatus,
            this.toolStripStatusLabel2,
            this.LblInbound,
            this.toolStripStatusLabel3,
            this.LblOutbound});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(870, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // LblStatus
            // 
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.ForeColor = System.Drawing.Color.OrangeRed;
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(81, 17);
            this.LblStatus.Text = "Not Running";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel2.Text = "  ";
            // 
            // LblInbound
            // 
            this.LblInbound.Image = global::Proxier.Properties.Resources.Download20;
            this.LblInbound.Name = "LblInbound";
            this.LblInbound.Size = new System.Drawing.Size(60, 17);
            this.LblInbound.Text = "0 bytes";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel3.Text = " ";
            // 
            // LblOutbound
            // 
            this.LblOutbound.Image = global::Proxier.Properties.Resources.Upload20;
            this.LblOutbound.Name = "LblOutbound";
            this.LblOutbound.Size = new System.Drawing.Size(60, 17);
            this.LblOutbound.Text = "0 bytes";
            // 
            // LblBottomSeparator
            // 
            this.LblBottomSeparator.BackColor = System.Drawing.Color.Silver;
            this.LblBottomSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblBottomSeparator.Location = new System.Drawing.Point(0, 431);
            this.LblBottomSeparator.Name = "LblBottomSeparator";
            this.LblBottomSeparator.Size = new System.Drawing.Size(870, 1);
            this.LblBottomSeparator.TabIndex = 4;
            // 
            // LblTopSeparator
            // 
            this.LblTopSeparator.BackColor = System.Drawing.Color.Chocolate;
            this.LblTopSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTopSeparator.Location = new System.Drawing.Point(0, 24);
            this.LblTopSeparator.Name = "LblTopSeparator";
            this.LblTopSeparator.Size = new System.Drawing.Size(870, 1);
            this.LblTopSeparator.TabIndex = 5;
            // 
            // RequestsLog
            // 
            this.RequestsLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RequestsLog.CellEditUseWholeCell = false;
            this.RequestsLog.HideSelection = false;
            this.RequestsLog.Location = new System.Drawing.Point(292, 36);
            this.RequestsLog.Name = "RequestsLog";
            this.RequestsLog.ShowGroups = false;
            this.RequestsLog.Size = new System.Drawing.Size(566, 379);
            this.RequestsLog.TabIndex = 6;
            this.RequestsLog.UseCompatibleStateImageBehavior = false;
            this.RequestsLog.View = System.Windows.Forms.View.Details;
            this.RequestsLog.VirtualMode = true;
            // 
            // Proxier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 454);
            this.Controls.Add(this.RequestsLog);
            this.Controls.Add(this.LblTopSeparator);
            this.Controls.Add(this.LblBottomSeparator);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PnlAvailableProcesses);
            this.Controls.Add(this.MenuTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuTop;
            this.Name = "Proxier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proxier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Proxier_FormClosing);
            this.Load += new System.EventHandler(this.Proxier_Load);
            this.Shown += new System.EventHandler(this.Proxier_Shown);
            this.MenuTop.ResumeLayout(false);
            this.MenuTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailableProcesses)).EndInit();
            this.PnlAvailableProcesses.ResumeLayout(false);
            this.PnlAvailableProcesses.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RequestsLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuTop;
        private System.Windows.Forms.ToolStripMenuItem BtnFileMenu;
        private System.Windows.Forms.ToolStripMenuItem BtnConfigMenu;
        private BrightIdeasSoftware.FastObjectListView AvailableProcesses;
        private System.Windows.Forms.Panel PnlAvailableProcesses;
        private System.Windows.Forms.TextBox TxtProcessesFilter;
        private System.Windows.Forms.Label LblSeparator;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LblStatus;
        private System.Windows.Forms.ToolStripMenuItem BtnMenuModifyState;
        private System.Windows.Forms.Label LblBottomSeparator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label LblTopSeparator;
        private System.Windows.Forms.ToolStripStatusLabel LblInbound;
        private System.Windows.Forms.ToolStripStatusLabel LblOutbound;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private BrightIdeasSoftware.FastObjectListView RequestsLog;
    }
}

