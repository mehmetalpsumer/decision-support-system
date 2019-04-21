namespace dss_assignment
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseFileBtn = new System.Windows.Forms.Button();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.browseFileName = new System.Windows.Forms.TextBox();
            this.groupBoxSuccessRates = new System.Windows.Forms.GroupBox();
            this.successTitleNb = new System.Windows.Forms.TextBox();
            this.successTitleKn = new System.Windows.Forms.TextBox();
            this.successTitleDt = new System.Windows.Forms.TextBox();
            this.successTitleAnn = new System.Windows.Forms.TextBox();
            this.successTitleSvm = new System.Windows.Forms.TextBox();
            this.successPrcNb = new System.Windows.Forms.TextBox();
            this.successPrcKn = new System.Windows.Forms.TextBox();
            this.successPrcDt = new System.Windows.Forms.TextBox();
            this.successPrcAnn = new System.Windows.Forms.TextBox();
            this.successPrcSvm = new System.Windows.Forms.TextBox();
            this.successRtNb = new System.Windows.Forms.TextBox();
            this.successRtKn = new System.Windows.Forms.TextBox();
            this.successRtDt = new System.Windows.Forms.TextBox();
            this.successRtAnn = new System.Windows.Forms.TextBox();
            this.successRtSvm = new System.Windows.Forms.TextBox();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxSuccessRates.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // browseFileBtn
            // 
            this.browseFileBtn.Location = new System.Drawing.Point(682, 19);
            this.browseFileBtn.Name = "browseFileBtn";
            this.browseFileBtn.Size = new System.Drawing.Size(75, 23);
            this.browseFileBtn.TabIndex = 0;
            this.browseFileBtn.Text = "Browse...";
            this.browseFileBtn.UseVisualStyleBackColor = true;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.browseFileName);
            this.groupBoxInput.Controls.Add(this.browseFileBtn);
            this.groupBoxInput.Location = new System.Drawing.Point(12, 27);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(776, 56);
            this.groupBoxInput.TabIndex = 1;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Import";
            // 
            // browseFileName
            // 
            this.browseFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.browseFileName.Location = new System.Drawing.Point(17, 19);
            this.browseFileName.Name = "browseFileName";
            this.browseFileName.ReadOnly = true;
            this.browseFileName.Size = new System.Drawing.Size(659, 20);
            this.browseFileName.TabIndex = 0;
            this.browseFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxSuccessRates
            // 
            this.groupBoxSuccessRates.Controls.Add(this.successRtSvm);
            this.groupBoxSuccessRates.Controls.Add(this.successRtAnn);
            this.groupBoxSuccessRates.Controls.Add(this.successRtDt);
            this.groupBoxSuccessRates.Controls.Add(this.successRtKn);
            this.groupBoxSuccessRates.Controls.Add(this.successRtNb);
            this.groupBoxSuccessRates.Controls.Add(this.successPrcSvm);
            this.groupBoxSuccessRates.Controls.Add(this.successPrcAnn);
            this.groupBoxSuccessRates.Controls.Add(this.successPrcDt);
            this.groupBoxSuccessRates.Controls.Add(this.successPrcKn);
            this.groupBoxSuccessRates.Controls.Add(this.successPrcNb);
            this.groupBoxSuccessRates.Controls.Add(this.successTitleSvm);
            this.groupBoxSuccessRates.Controls.Add(this.successTitleAnn);
            this.groupBoxSuccessRates.Controls.Add(this.successTitleDt);
            this.groupBoxSuccessRates.Controls.Add(this.successTitleKn);
            this.groupBoxSuccessRates.Controls.Add(this.successTitleNb);
            this.groupBoxSuccessRates.Location = new System.Drawing.Point(11, 98);
            this.groupBoxSuccessRates.Name = "groupBoxSuccessRates";
            this.groupBoxSuccessRates.Size = new System.Drawing.Size(776, 133);
            this.groupBoxSuccessRates.TabIndex = 2;
            this.groupBoxSuccessRates.TabStop = false;
            this.groupBoxSuccessRates.Text = "Success Rates";
            // 
            // successTitleNb
            // 
            this.successTitleNb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successTitleNb.Location = new System.Drawing.Point(36, 32);
            this.successTitleNb.Name = "successTitleNb";
            this.successTitleNb.ReadOnly = true;
            this.successTitleNb.Size = new System.Drawing.Size(102, 13);
            this.successTitleNb.TabIndex = 0;
            this.successTitleNb.Text = "Naive-Bayes";
            this.successTitleNb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successTitleKn
            // 
            this.successTitleKn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successTitleKn.Location = new System.Drawing.Point(168, 32);
            this.successTitleKn.Name = "successTitleKn";
            this.successTitleKn.ReadOnly = true;
            this.successTitleKn.Size = new System.Drawing.Size(113, 13);
            this.successTitleKn.TabIndex = 1;
            this.successTitleKn.Text = "K-Nearest Neighbour";
            this.successTitleKn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.successTitleKn.TextChanged += new System.EventHandler(this.successTitleKn_TextChanged);
            // 
            // successTitleDt
            // 
            this.successTitleDt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successTitleDt.Location = new System.Drawing.Point(314, 32);
            this.successTitleDt.Name = "successTitleDt";
            this.successTitleDt.ReadOnly = true;
            this.successTitleDt.Size = new System.Drawing.Size(113, 13);
            this.successTitleDt.TabIndex = 2;
            this.successTitleDt.Text = "Decision Tree";
            this.successTitleDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successTitleAnn
            // 
            this.successTitleAnn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successTitleAnn.Location = new System.Drawing.Point(460, 32);
            this.successTitleAnn.Name = "successTitleAnn";
            this.successTitleAnn.ReadOnly = true;
            this.successTitleAnn.Size = new System.Drawing.Size(113, 13);
            this.successTitleAnn.TabIndex = 3;
            this.successTitleAnn.Text = "Artifician N. N.";
            this.successTitleAnn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successTitleSvm
            // 
            this.successTitleSvm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successTitleSvm.Location = new System.Drawing.Point(610, 32);
            this.successTitleSvm.Name = "successTitleSvm";
            this.successTitleSvm.ReadOnly = true;
            this.successTitleSvm.Size = new System.Drawing.Size(113, 13);
            this.successTitleSvm.TabIndex = 4;
            this.successTitleSvm.Text = "Support Vector M.";
            this.successTitleSvm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successPrcNb
            // 
            this.successPrcNb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successPrcNb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successPrcNb.Location = new System.Drawing.Point(36, 68);
            this.successPrcNb.Name = "successPrcNb";
            this.successPrcNb.ReadOnly = true;
            this.successPrcNb.Size = new System.Drawing.Size(102, 25);
            this.successPrcNb.TabIndex = 5;
            this.successPrcNb.Text = "0%";
            this.successPrcNb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successPrcKn
            // 
            this.successPrcKn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successPrcKn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successPrcKn.Location = new System.Drawing.Point(168, 68);
            this.successPrcKn.Name = "successPrcKn";
            this.successPrcKn.ReadOnly = true;
            this.successPrcKn.Size = new System.Drawing.Size(102, 25);
            this.successPrcKn.TabIndex = 6;
            this.successPrcKn.Text = "0%";
            this.successPrcKn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successPrcDt
            // 
            this.successPrcDt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successPrcDt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successPrcDt.Location = new System.Drawing.Point(334, 68);
            this.successPrcDt.Name = "successPrcDt";
            this.successPrcDt.ReadOnly = true;
            this.successPrcDt.Size = new System.Drawing.Size(89, 25);
            this.successPrcDt.TabIndex = 7;
            this.successPrcDt.Text = "0%";
            this.successPrcDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successPrcAnn
            // 
            this.successPrcAnn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successPrcAnn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successPrcAnn.Location = new System.Drawing.Point(474, 68);
            this.successPrcAnn.Name = "successPrcAnn";
            this.successPrcAnn.ReadOnly = true;
            this.successPrcAnn.Size = new System.Drawing.Size(89, 25);
            this.successPrcAnn.TabIndex = 8;
            this.successPrcAnn.Text = "0%";
            this.successPrcAnn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successPrcSvm
            // 
            this.successPrcSvm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successPrcSvm.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successPrcSvm.Location = new System.Drawing.Point(624, 68);
            this.successPrcSvm.Name = "successPrcSvm";
            this.successPrcSvm.ReadOnly = true;
            this.successPrcSvm.Size = new System.Drawing.Size(89, 25);
            this.successPrcSvm.TabIndex = 9;
            this.successPrcSvm.Text = "0%";
            this.successPrcSvm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successRtNb
            // 
            this.successRtNb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successRtNb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successRtNb.Location = new System.Drawing.Point(36, 99);
            this.successRtNb.Name = "successRtNb";
            this.successRtNb.ReadOnly = true;
            this.successRtNb.Size = new System.Drawing.Size(102, 16);
            this.successRtNb.TabIndex = 10;
            this.successRtNb.Text = "...";
            this.successRtNb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.successRtNb.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // successRtKn
            // 
            this.successRtKn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successRtKn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successRtKn.Location = new System.Drawing.Point(168, 99);
            this.successRtKn.Name = "successRtKn";
            this.successRtKn.ReadOnly = true;
            this.successRtKn.Size = new System.Drawing.Size(102, 16);
            this.successRtKn.TabIndex = 11;
            this.successRtKn.Text = "...";
            this.successRtKn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successRtDt
            // 
            this.successRtDt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successRtDt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successRtDt.Location = new System.Drawing.Point(321, 99);
            this.successRtDt.Name = "successRtDt";
            this.successRtDt.ReadOnly = true;
            this.successRtDt.Size = new System.Drawing.Size(102, 16);
            this.successRtDt.TabIndex = 11;
            this.successRtDt.Text = "...";
            this.successRtDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successRtAnn
            // 
            this.successRtAnn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successRtAnn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successRtAnn.Location = new System.Drawing.Point(461, 99);
            this.successRtAnn.Name = "successRtAnn";
            this.successRtAnn.ReadOnly = true;
            this.successRtAnn.Size = new System.Drawing.Size(102, 16);
            this.successRtAnn.TabIndex = 11;
            this.successRtAnn.Text = "...";
            this.successRtAnn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // successRtSvm
            // 
            this.successRtSvm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.successRtSvm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.successRtSvm.Location = new System.Drawing.Point(611, 99);
            this.successRtSvm.Name = "successRtSvm";
            this.successRtSvm.ReadOnly = true;
            this.successRtSvm.Size = new System.Drawing.Size(102, 16);
            this.successRtSvm.TabIndex = 11;
            this.successRtSvm.Text = "...";
            this.successRtSvm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxSuccessRates);
            this.Controls.Add(this.groupBoxInput);
            this.Name = "Form1";
            this.Text = "Decision Support Systems Assignment";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxSuccessRates.ResumeLayout(false);
            this.groupBoxSuccessRates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

       

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button browseFileBtn;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.TextBox browseFileName;
        private System.Windows.Forms.GroupBox groupBoxSuccessRates;
        private System.Windows.Forms.TextBox successTitleNb;
        private System.Windows.Forms.TextBox successTitleSvm;
        private System.Windows.Forms.TextBox successTitleAnn;
        private System.Windows.Forms.TextBox successTitleDt;
        private System.Windows.Forms.TextBox successTitleKn;
        private System.Windows.Forms.TextBox successPrcAnn;
        private System.Windows.Forms.TextBox successPrcDt;
        private System.Windows.Forms.TextBox successPrcKn;
        private System.Windows.Forms.TextBox successRtNb;
        private System.Windows.Forms.TextBox successRtSvm;
        private System.Windows.Forms.TextBox successRtAnn;
        private System.Windows.Forms.TextBox successRtDt;
        private System.Windows.Forms.TextBox successRtKn;
        public System.Windows.Forms.TextBox successPrcNb;
        public System.Windows.Forms.TextBox successPrcSvm;
    }
}

