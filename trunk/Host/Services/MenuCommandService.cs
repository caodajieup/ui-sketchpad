using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Collections.Generic;
using Sketchpad.UI.Controls;

using CommandID = System.ComponentModel.Design.CommandID;
using MenuCommand = System.ComponentModel.Design.MenuCommand;

namespace Sketchpad.UI.Services
{
    class MyMenuCommands
    {
        public static readonly CommandID AddTabPage = new CommandID(StandardCommands.Undo.Guid, 0x1001);
    }

    class MenuCommandService : System.ComponentModel.Design.MenuCommandService
    {

        Control panel;

        public MenuCommandService(Control panel, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.panel = panel;
            this.InitializeGlobalCommands();
        }

        public MenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.panel =  new Control();
            this.InitializeGlobalCommands();
        }

        void ExecuteAddTabPage(object sender, EventArgs e)
        {
            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                System.Collections.ICollection selectedComps = selectionService.GetSelectedComponents();
                if (selectedComps != null && selectedComps.Count == 1)
                {
                    object[] comps = new object[selectedComps.Count];
                    int i = 0;
                    foreach (object obj in selectedComps)
                    {
                        comps[i] = obj;
                    }
                    TabControl tab = comps[0] as TabControl;
                    if (tab != null)
                    {
                        string title = "TabPage " + (tab.TabCount + 1).ToString();
                        TabPage newPage = new TabPage(title);
                        tab.TabPages.Add(newPage);
                    }
                }
            }
        }

        void ExecuteUndo(object sender, EventArgs e)
        {
            MyUndoEngine undoEngine = GetService(typeof(UndoEngine)) as MyUndoEngine;
            if (undoEngine != null)
                undoEngine.Undo();
        }
        void ExecuteRedo(object sender, EventArgs e)
        {
            MyUndoEngine undoEngine = GetService(typeof(UndoEngine)) as MyUndoEngine;
            if (undoEngine != null)
                undoEngine.Redo();
        }

        private void InitializeGlobalCommands()
        {
            this.AddCommand(new MenuCommand(ExecuteUndo, StandardCommands.Undo));
            this.AddCommand(new MenuCommand(ExecuteRedo, StandardCommands.Redo));
            this.AddCommand(new MenuCommand(ExecuteAddTabPage, MyMenuCommands.AddTabPage));
        }

        private void OnMenuClicked(object sender, EventArgs args)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                MenuCommand cmd = item.Tag as MenuCommand;
                cmd.Invoke();
            }
        }

        private MenuItem[] GetSelectionMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

            if (selectionService != null)
            {
                Dictionary<CommandID, string> selectionCommands = new Dictionary<CommandID, string>();
                selectionCommands.Add(StandardCommands.Cut, "Cut");
                selectionCommands.Add(StandardCommands.Copy, "Copy");
                selectionCommands.Add(StandardCommands.Paste, "Paste");
                selectionCommands.Add(StandardCommands.Delete, "Delete");
                selectionCommands.Add(StandardCommands.Undo, "Undo");
                selectionCommands.Add(StandardCommands.Redo, "Redo");
                selectionCommands.Add(MyMenuCommands.AddTabPage, "Add Tab Page");

                foreach (CommandID id in selectionCommands.Keys)
                {
                    MenuCommand command = FindCommand(id);
                    if (command != null)
                    {
                        MenuItem menuItem = new MenuItem(selectionCommands[id], new EventHandler(OnMenuClicked));
                        menuItem.Tag = command;
                        menuItems.Add(menuItem);
                    }
                }
            }

            return menuItems.ToArray();
        }

        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            //ISelectionService

            if (menuID == MenuCommands.SelectionMenu || menuID == MenuCommands.ContainerMenu)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem[] items = GetSelectionMenuItems();

                if (items.Length > 0)
                {
                    contextMenu.MenuItems.Add(new MenuItem("-"));
                    foreach(MenuItem item in items)
                    {
                        contextMenu.MenuItems.Add(item);
                    }
                }
                contextMenu.Show(panel, panel.PointToClient(new Point(x, y)));
            }
            else
            {
                throw new Exception();
            }
            //Point p = panel.PointToClient(new Point(x, y));


            //MenuService.ShowContextMenu(this, contextMenuPath, panel, p.X, p.Y);
            //ISelectionService selectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
            //ICollection selectedComponents = selectionService.GetSelectedComponents();
            //PropertyGrid propertyGrid = (PropertyGrid)this.GetService(typeof(PropertyGrid));

            //if (selectedComponents.Count != 1)
            //    return;

            //Editor ed = new Editor();
            //if (ed.ShowDialog() == DialogResult.OK)
            //{
            //    object[] comps = new object[selectedComponents.Count];
            //    int i = 0;

            //    foreach (Object o in selectedComponents)
            //    {
            //        comps[i] = o;
            //        i++;
            //    }
            //    if (comps[0].GetType() == typeof(System.Windows.Forms.Button))
            //    {
            //        Button btn = comps[0] as Button;
            //        btn.Text = ed.Name;
            //        propertyGrid.SelectedObject = btn;
            //    }
            //}
        }
    }
}