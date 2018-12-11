namespace PlanetsEditor.Controls
{
    partial class PropertiesPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudOrbitRadius = new System.Windows.Forms.NumericUpDown();
            this.nudOrbitalSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOrbitDirection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMass = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSurfaceType = new System.Windows.Forms.ComboBox();
            this.nudRandomSeed = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudRadius = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudStartOrbitAngle = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrbitRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrbitalSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartOrbitAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(6, 28);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(140, 22);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Orbit Radius (AU)";
            // 
            // nudOrbitRadius
            // 
            this.nudOrbitRadius.DecimalPlaces = 6;
            this.nudOrbitRadius.Location = new System.Drawing.Point(6, 134);
            this.nudOrbitRadius.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudOrbitRadius.Name = "nudOrbitRadius";
            this.nudOrbitRadius.Size = new System.Drawing.Size(140, 22);
            this.nudOrbitRadius.TabIndex = 2;
            this.nudOrbitRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudOrbitRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOrbitRadius.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // nudOrbitalSpeed
            // 
            this.nudOrbitalSpeed.DecimalPlaces = 4;
            this.nudOrbitalSpeed.Location = new System.Drawing.Point(6, 186);
            this.nudOrbitalSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudOrbitalSpeed.Name = "nudOrbitalSpeed";
            this.nudOrbitalSpeed.Size = new System.Drawing.Size(140, 22);
            this.nudOrbitalSpeed.TabIndex = 3;
            this.nudOrbitalSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudOrbitalSpeed.Value = new decimal(new int[] {
            29783,
            0,
            0,
            196608});
            this.nudOrbitalSpeed.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Orbital Speed (km/s)";
            // 
            // cbOrbitDirection
            // 
            this.cbOrbitDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrbitDirection.FormattingEnabled = true;
            this.cbOrbitDirection.Location = new System.Drawing.Point(6, 295);
            this.cbOrbitDirection.Name = "cbOrbitDirection";
            this.cbOrbitDirection.Size = new System.Drawing.Size(140, 24);
            this.cbOrbitDirection.TabIndex = 4;
            this.cbOrbitDirection.SelectedIndexChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Orbit Direction";
            // 
            // nudMass
            // 
            this.nudMass.DecimalPlaces = 6;
            this.nudMass.Location = new System.Drawing.Point(6, 83);
            this.nudMass.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudMass.Name = "nudMass";
            this.nudMass.Size = new System.Drawing.Size(140, 22);
            this.nudMass.TabIndex = 1;
            this.nudMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMass.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mass (of Earth)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Surface Type";
            // 
            // cbSurfaceType
            // 
            this.cbSurfaceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSurfaceType.FormattingEnabled = true;
            this.cbSurfaceType.Location = new System.Drawing.Point(184, 134);
            this.cbSurfaceType.Name = "cbSurfaceType";
            this.cbSurfaceType.Size = new System.Drawing.Size(140, 24);
            this.cbSurfaceType.TabIndex = 12;
            this.cbSurfaceType.SelectedIndexChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // nudRandomSeed
            // 
            this.nudRandomSeed.Location = new System.Drawing.Point(184, 186);
            this.nudRandomSeed.Maximum = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.nudRandomSeed.Name = "nudRandomSeed";
            this.nudRandomSeed.Size = new System.Drawing.Size(140, 22);
            this.nudRandomSeed.TabIndex = 11;
            this.nudRandomSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRandomSeed.Value = new decimal(new int[] {
            29783,
            0,
            0,
            196608});
            this.nudRandomSeed.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(181, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Random Seed";
            // 
            // nudRadius
            // 
            this.nudRadius.DecimalPlaces = 6;
            this.nudRadius.Location = new System.Drawing.Point(184, 83);
            this.nudRadius.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudRadius.Name = "nudRadius";
            this.nudRadius.Size = new System.Drawing.Size(140, 22);
            this.nudRadius.TabIndex = 9;
            this.nudRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRadius.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Radius (of Earth)";
            // 
            // nudStartOrbitAngle
            // 
            this.nudStartOrbitAngle.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudStartOrbitAngle.Location = new System.Drawing.Point(6, 239);
            this.nudStartOrbitAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudStartOrbitAngle.Name = "nudStartOrbitAngle";
            this.nudStartOrbitAngle.Size = new System.Drawing.Size(140, 22);
            this.nudStartOrbitAngle.TabIndex = 15;
            this.nudStartOrbitAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudStartOrbitAngle.ValueChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Start Orbit Angle (deg)";
            // 
            // BodyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudStartOrbitAngle);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSurfaceType);
            this.Controls.Add(this.nudRandomSeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudRadius);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudMass);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbOrbitDirection);
            this.Controls.Add(this.nudOrbitalSpeed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudOrbitRadius);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Name = "BodyEditor";
            this.Size = new System.Drawing.Size(432, 368);
            ((System.ComponentModel.ISupportInitialize)(this.nudOrbitRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrbitalSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartOrbitAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudOrbitRadius;
        private System.Windows.Forms.NumericUpDown nudOrbitalSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbOrbitDirection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSurfaceType;
        private System.Windows.Forms.NumericUpDown nudRandomSeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudRadius;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudStartOrbitAngle;
        private System.Windows.Forms.Label label9;
    }
}
