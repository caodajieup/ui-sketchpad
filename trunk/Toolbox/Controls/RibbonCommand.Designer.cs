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
            this.button_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_.Location = new System.Drawing.Point(10, 0);
            this.button_.MaximumSize = new System.Drawing.Size(32, 32);
            this.button_.MinimumSize = new System.Drawing.Size(16, 16);
            this.button_.Name = "button_";
            this.button_.Size = new System.Drawing.Size(32, 32);
            this.button_.TabIndex = 0;
            this.button_.UseVisualStyleBackColor = true;
            // 
            // label_
            // 
            this.label_.AutoSize = true;
            this.label_.Location = new System.Drawing.Point(0, 35);
            this.label_.MaximumSize = new System.Drawing.Size(52, 16);
            this.label_.MinimumSize = new System.Drawing.Size(52, 16);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(52, 16);
            this.label_.TabIndex = 1;
            this.label_.Text = "RibbonCommand";
            this.label_.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RibbonCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_);
            this.Controls.Add(this.button_);
            this.MaximumSize = new System.Drawing.Size(0, 48);
            this.MinimumSize = new System.Drawing.Size(52, 48);
            this.Name = "RibbonCommand";
            this.Size = new System.Drawing.Size(52, 48);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_;
        private System.Windows.Forms.Label label_;
    }
}
