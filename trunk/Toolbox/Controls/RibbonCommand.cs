using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Sketchpad.UI.Controls
{
    public enum RibbonIconSize
    {
        Large,
        Small
    }

    public partial class RibbonCommand : UserControl
    {
        public RibbonCommand()
        {
            InitializeComponent();
        }

        #region exposed properties
        public override Image BackgroundImage {
            get { 
                return this.button_.BackgroundImage; 
            }

            set { 
                this.button_.BackgroundImage = value; 
            }
        }

        [CategoryAttribute("Appearance"), DisplayName("Text"), Browsable(true)]
        public override string Text
        {
            get
            {
                return this.label_.Text;
            }

            set
            {
                this.label_.Text = value;
            }
        }

        [CategoryAttribute("Advanced"), DisplayName("MenuMacro"), Browsable(true)]
        public string MenuMacro
        {
            get
            {
                return this.menuMacro_;
            }

            set
            {
                this.menuMacro_ = value;
            }
        }

        [CategoryAttribute("Advanced"), DisplayName("MenuMacro"), Browsable(true)]
        public RibbonIconSize IconSize
        {
            get
            {
                return this.size_;
            }
            set
            {
                if (value != this.size_)
                {
                    if (value == RibbonIconSize.Large)
                    {
                        this.button_.Size = new Size(32, 32);
                        this.button_.Location = new Point((this.Size.Width - this.button_.Size.Width) / 2, 0);
                    }
                    else
                    {
                        this.button_.Size = new Size(16, 16);
                        this.button_.Location = new Point((this.Size.Width - this.button_.Size.Width) / 2, 0);
                    }
                    this.size_ = value;
                }
            }
        }

        #endregion

        #region Data members
        private string menuMacro_;
        private RibbonIconSize size_;
        #endregion
    }
}
