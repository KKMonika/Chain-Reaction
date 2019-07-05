namespace Chain_Reaction
{
    partial class CustomGame
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlayCustom = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.nudMaxBalls = new System.Windows.Forms.NumericUpDown();
            this.nudNeedToHit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNeedToHit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total number of balls in the game:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of balls needed to expand to win the game:";
            // 
            // btnPlayCustom
            // 
            this.btnPlayCustom.Location = new System.Drawing.Point(172, 122);
            this.btnPlayCustom.Name = "btnPlayCustom";
            this.btnPlayCustom.Size = new System.Drawing.Size(138, 23);
            this.btnPlayCustom.TabIndex = 4;
            this.btnPlayCustom.Text = "Play";
            this.btnPlayCustom.UseVisualStyleBackColor = true;
            this.btnPlayCustom.Click += new System.EventHandler(this.btnPlayCustom_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(16, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // nudMaxBalls
            // 
            this.nudMaxBalls.Location = new System.Drawing.Point(16, 30);
            this.nudMaxBalls.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.nudMaxBalls.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMaxBalls.Name = "nudMaxBalls";
            this.nudMaxBalls.Size = new System.Drawing.Size(120, 20);
            this.nudMaxBalls.TabIndex = 6;
            this.nudMaxBalls.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMaxBalls.Validating += new System.ComponentModel.CancelEventHandler(this.nudMaxBalls_Validating);
            // 
            // nudNeedToHit
            // 
            this.nudNeedToHit.Location = new System.Drawing.Point(16, 84);
            this.nudNeedToHit.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.nudNeedToHit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNeedToHit.Name = "nudNeedToHit";
            this.nudNeedToHit.Size = new System.Drawing.Size(120, 20);
            this.nudNeedToHit.TabIndex = 7;
            this.nudNeedToHit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNeedToHit.Validating += new System.ComponentModel.CancelEventHandler(this.nudNeedToHit_Validating);
            // 
            // CustomGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 163);
            this.Controls.Add(this.nudNeedToHit);
            this.Controls.Add(this.nudMaxBalls);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPlayCustom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CustomGame";
            this.Text = "CustomGame";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBalls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNeedToHit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlayCustom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.NumericUpDown nudNeedToHit;
        private System.Windows.Forms.NumericUpDown nudMaxBalls;
    }
}