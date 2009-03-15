using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Collections;
using Sketchpad.UI.Controls;

using CommandID = System.ComponentModel.Design.CommandID;
using MenuCommand = System.ComponentModel.Design.MenuCommand;

namespace Sketchpad.UI.Services
{
    class MenuCommandService : System.ComponentModel.Design.MenuCommandService
    {

        Control panel;

        public MenuCommandService(Control panel, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.panel = panel;
            //this.InitializeGlobalCommands();
        }

        public MenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.panel = null;
            //this.InitializeGlobalCommands();
        }

        //private void InitializeGlobalCommands()
        //{
        //    this.AddCommand(new MenuCommand(viewCodeCommand.CommandCallBack, viewCodeCommand.CommandID));
        //    this.AddCommand(new MenuCommand(propertiesCodeCommand.CommandCallBack, propertiesCodeCommand.CommandID));
        //}

        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            //string contextMenuPath = "/SharpDevelop/FormsDesigner/ContextMenus/";

            //if (menuID == MenuCommands.ComponentTrayMenu)
            //{
            //    contextMenuPath += "ComponentTrayMenu";
            //}
            //else if (menuID == MenuCommands.ContainerMenu)
            //{
            //    contextMenuPath += "ContainerMenu";
            //}
            //else if (menuID == MenuCommands.SelectionMenu)
            //{
            //    contextMenuPath += "SelectionMenu";
            //}
            //else if (menuID == MenuCommands.TraySelectionMenu)
            //{
            //    contextMenuPath += "TraySelectionMenu";
            //}
            //else
            //{
            //    throw new Exception();
            //}
            //Point p = panel.PointToClient(new Point(x, y));


            //MenuService.ShowContextMenu(this, contextMenuPath, panel, p.X, p.Y);
            ISelectionService selectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
            ICollection selectedComponents = selectionService.GetSelectedComponents();
            PropertyGrid propertyGrid = (PropertyGrid)this.GetService(typeof(PropertyGrid));

            if (selectedComponents.Count != 1)
                return;

            Editor ed = new Editor();
            if (ed.ShowDialog() == DialogResult.OK)
            {
                object[] comps = new object[selectedComponents.Count];
                int i = 0;

                foreach (Object o in selectedComponents)
                {
                    comps[i] = o;
                    i++;
                }
                if (comps[0].GetType() == typeof(System.Windows.Forms.Button))
                {
                    Button btn = comps[0] as Button;
                    btn.Text = ed.Name;
                    propertyGrid.SelectedObject = btn;
                }
            }
        }
    }
}