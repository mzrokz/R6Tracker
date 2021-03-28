namespace R6Tracker.WinFormApp
{
    partial class Form1
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
            this.btnStart = new System.Windows.Forms.Button();
            this.gridPlayers = new DevExpress.XtraGrid.GridControl();
            this.gridViewPlayers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPlayerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAlias = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSyncPlayerData = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStart.Location = new System.Drawing.Point(920, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(144, 25);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // gridPlayers
            // 
            this.gridPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPlayers.Location = new System.Drawing.Point(3, 3);
            this.gridPlayers.MainView = this.gridViewPlayers;
            this.gridPlayers.Name = "gridPlayers";
            this.gridPlayers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridPlayers.Size = new System.Drawing.Size(1064, 384);
            this.gridPlayers.TabIndex = 1;
            this.gridPlayers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPlayers});
            // 
            // gridViewPlayers
            // 
            this.gridViewPlayers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPlayerName,
            this.gcAlias,
            this.gcIsActive});
            this.gridViewPlayers.GridControl = this.gridPlayers;
            this.gridViewPlayers.Name = "gridViewPlayers";
            // 
            // gcPlayerName
            // 
            this.gcPlayerName.Caption = "Player Name";
            this.gcPlayerName.FieldName = "PlayerName";
            this.gcPlayerName.Name = "gcPlayerName";
            this.gcPlayerName.Visible = true;
            this.gcPlayerName.VisibleIndex = 0;
            // 
            // gcAlias
            // 
            this.gcAlias.Caption = "Alias";
            this.gcAlias.FieldName = "Alias";
            this.gcAlias.Name = "gcAlias";
            this.gcAlias.Visible = true;
            this.gcAlias.VisibleIndex = 1;
            // 
            // gcIsActive
            // 
            this.gcIsActive.Caption = "Active";
            this.gcIsActive.FieldName = "IsActive";
            this.gcIsActive.Name = "gcIsActive";
            this.gcIsActive.Visible = true;
            this.gcIsActive.VisibleIndex = 2;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gridPlayers, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.63658F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.36342F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1070, 421);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnSyncPlayerData);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 25);
            this.panel1.TabIndex = 2;
            // 
            // btnSyncPlayerData
            // 
            this.btnSyncPlayerData.Location = new System.Drawing.Point(799, 0);
            this.btnSyncPlayerData.Name = "btnSyncPlayerData";
            this.btnSyncPlayerData.Size = new System.Drawing.Size(115, 23);
            this.btnSyncPlayerData.TabIndex = 1;
            this.btnSyncPlayerData.Text = "Sync Player Data";
            this.btnSyncPlayerData.UseVisualStyleBackColor = true;
            this.btnSyncPlayerData.Click += new System.EventHandler(this.btnSyncPlayerData_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(698, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(95, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 421);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private DevExpress.XtraGrid.GridControl gridPlayers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPlayers;
        private DevExpress.XtraGrid.Columns.GridColumn gcPlayerName;
        private DevExpress.XtraGrid.Columns.GridColumn gcAlias;
        private DevExpress.XtraGrid.Columns.GridColumn gcIsActive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSyncPlayerData;
        private System.Windows.Forms.Button btnUpdate;
    }
}

