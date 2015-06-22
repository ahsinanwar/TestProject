namespace Reporting
{
    partial class UCReportFilter
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
            this.btnEmpFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEmpFilter
            // 
            this.btnEmpFilter.Location = new System.Drawing.Point(56, 61);
            this.btnEmpFilter.Name = "btnEmpFilter";
            this.btnEmpFilter.Size = new System.Drawing.Size(75, 23);
            this.btnEmpFilter.TabIndex = 0;
            this.btnEmpFilter.Text = "button1";
            this.btnEmpFilter.UseVisualStyleBackColor = true;
            this.btnEmpFilter.Click += new System.EventHandler(this.btnEmpFilter_Click);
            // 
            // UCReportFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEmpFilter);
            this.Name = "UCReportFilter";
            this.Size = new System.Drawing.Size(278, 454);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEmpFilter;
    }
}
