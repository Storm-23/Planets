namespace PlanetsEditor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btOpen = new System.Windows.Forms.ToolStripButton();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsTree = new System.Windows.Forms.ToolStrip();
            this.btAdd = new System.Windows.Forms.ToolStripButton();
            this.btRemove = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvSolarSystem = new FastTreeNS.FastTree();
            this.pnProperties = new PlanetsEditor.Controls.PropertiesPanel();
            this.fm = new System.Windows.Forms.FileManager(this.components);
            this.toolStrip1.SuspendLayout();
            this.tsTree.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fm)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.btOpen,
            this.btSave,
            this.btSaveAs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(707, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btNew
            // 
            this.btNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNew.Image = ((System.Drawing.Image)(resources.GetObject("btNew.Image")));
            this.btNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(24, 24);
            this.btNew.Text = "&New";
            // 
            // btOpen
            // 
            this.btOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btOpen.Image = ((System.Drawing.Image)(resources.GetObject("btOpen.Image")));
            this.btOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(24, 24);
            this.btOpen.Text = "&Open";
            // 
            // btSave
            // 
            this.btSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSave.Image = global::PlanetsEditor.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(24, 24);
            this.btSave.Text = "&Save";
            // 
            // btSaveAs
            // 
            this.btSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveAs.Image = global::PlanetsEditor.Properties.Resources.save_as;
            this.btSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveAs.Name = "btSaveAs";
            this.btSaveAs.Size = new System.Drawing.Size(24, 24);
            this.btSaveAs.Text = "Save As...";
            // 
            // tsTree
            // 
            this.tsTree.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btAdd,
            this.btRemove});
            this.tsTree.Location = new System.Drawing.Point(0, 0);
            this.tsTree.Name = "tsTree";
            this.tsTree.Size = new System.Drawing.Size(235, 27);
            this.tsTree.TabIndex = 2;
            this.tsTree.Text = "toolStrip2";
            // 
            // btAdd
            // 
            this.btAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAdd.Image = global::PlanetsEditor.Properties.Resources.add__16x16;
            this.btAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(24, 24);
            this.btAdd.Text = "Add Body";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btRemove
            // 
            this.btRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRemove.Image = global::PlanetsEditor.Properties.Resources.cross;
            this.btRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(24, 24);
            this.btRemove.Text = "Remove Body";
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvSolarSystem);
            this.splitContainer1.Panel1.Controls.Add(this.tsTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnProperties);
            this.splitContainer1.Size = new System.Drawing.Size(707, 403);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 4;
            // 
            // tvSolarSystem
            // 
            this.tvSolarSystem.AutoScroll = true;
            this.tvSolarSystem.AutoScrollMinSize = new System.Drawing.Size(0, 59);
            this.tvSolarSystem.BackColor = System.Drawing.SystemColors.Window;
            this.tvSolarSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSolarSystem.ImageCheckBoxOff = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageCheckBoxOff")));
            this.tvSolarSystem.ImageCheckBoxOn = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageCheckBoxOn")));
            this.tvSolarSystem.ImageCollapse = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageCollapse")));
            this.tvSolarSystem.ImageDefaultIcon = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageDefaultIcon")));
            this.tvSolarSystem.ImageEmptyExpand = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageEmptyExpand")));
            this.tvSolarSystem.ImageExpand = ((System.Drawing.Image)(resources.GetObject("tvSolarSystem.ImageExpand")));
            this.tvSolarSystem.IsEditMode = false;
            this.tvSolarSystem.Location = new System.Drawing.Point(0, 27);
            this.tvSolarSystem.Name = "tvSolarSystem";
            this.tvSolarSystem.Readonly = true;
            this.tvSolarSystem.ShowExpandBoxes = true;
            this.tvSolarSystem.ShowIcons = true;
            this.tvSolarSystem.ShowRootNode = true;
            this.tvSolarSystem.Size = new System.Drawing.Size(235, 376);
            this.tvSolarSystem.TabIndex = 0;
            this.tvSolarSystem.NodeIconNeeded += new System.EventHandler<FastTreeNS.ImageNodeEventArgs>(this.tvSolarSystem_NodeIconNeeded);
            this.tvSolarSystem.NodeSelectedStateChanged += new System.EventHandler<FastTreeNS.NodeSelectedStateChangedEventArgs>(this.tvSolarSystem_NodeSelectedStateChanged);
            // 
            // pnProperties
            // 
            this.pnProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnProperties.Location = new System.Drawing.Point(0, 0);
            this.pnProperties.Name = "pnProperties";
            this.pnProperties.Size = new System.Drawing.Size(468, 403);
            this.pnProperties.TabIndex = 3;
            this.pnProperties.Changed += new System.Action(this.pnProperties_Changed);
            // 
            // fm
            // 
            this.fm.CurrentFileName = null;
            this.fm.DefaultFolder = null;
            this.fm.DefaultSaveExtension = null;
            this.fm.Document = null;
            this.fm.IsDocumentChanged = false;
            this.fm.MainForm = this;
            this.fm.MainFormTitle = "Planets Editor";
            this.fm.NewButton = this.btNew;
            this.fm.OpenButton = this.btOpen;
            this.fm.OpenFilter = "Planet System|*.planets";
            this.fm.SaveAsButton = this.btSaveAs;
            this.fm.SaveButton = this.btSave;
            this.fm.SaveFilter = "Planet System|*.planets";
            this.fm.Text = null;
            this.fm.DocOpenedOrCreated += new System.EventHandler(this.fm_DocOpenedOrCreated);
            this.fm.NewDocNeeded += new System.EventHandler<System.Windows.Forms.DocEventArgs>(this.fm_NewDocNeeded);
            this.fm.Saving += new System.EventHandler<System.Windows.Forms.DocEventArgs>(this.fm_Saving);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 430);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Planets Editor - New document";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tsTree.ResumeLayout(false);
            this.tsTree.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastTreeNS.FastTree tvSolarSystem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btOpen;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStrip tsTree;
        private System.Windows.Forms.ToolStripButton btAdd;
        private System.Windows.Forms.ToolStripButton btRemove;
        private Controls.PropertiesPanel pnProperties;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btSaveAs;
        private System.Windows.Forms.FileManager fm;
    }
}

