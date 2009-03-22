namespace Sketchpad.UI.Controls
{
    partial class RibbonCommand
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
            this.button_ = new System.Windows.Forms.Button();
            this.label_ = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_
            // 
            this.button_.Location = new System.Drawing.Point(0, 0);
            this.button_.Name = "button_";
            this.button_.Size = new System.Drawing.Size(120, 105);
            this.button_.TabIndex = 0;
            this.button_.UseVisualStyleBackColor = true;
            // 
            // label_
            // 
            this.label_.AutoSize = true;
            this.label_.Location = new System.Drawing.Point(26, 108);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(65, 12);
            this.label_.TabIndex = 1;
            this.label_.Text = "RibbonName";
            this.label_.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RibbonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_);
            this.Controls.Add(this.button_);
            this.MaximumSize = new System.Drawing.Size(120, 120);
            this.MinimumSize = new System.Drawing.Size(120, 120);
            this.Name = "RibbonControl";
            this.Size = new System.Drawing.Size(120, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_;
        private System.Windows.Forms.Label label_;
    }
}
