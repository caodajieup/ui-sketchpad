#region Header
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using Sketchpad.UI.Services;

using MyMenuCommandServices = Sketchpad.UI.Services.MenuCommandService;
#endregion

namespace Host 
{
    /// <summary>
    /// Inherits from DesignSurface and hosts the RootComponent and 
    /// all other designers. It also uses loaders (BasicDesignerLoader
    /// or CodeDomDesignerLoader) when required. It also provides various
    /// services to the designers. Adds MenuCommandService which is used
    /// for Cut, Copy, Paste, etc.
    /// </summary>
	public class HostSurface : DesignSurface
	{
		BasicDesignerLoader _loader;
		ISelectionService _selectionService;
        XmlDocument _xDoc = new XmlDocument();

		public HostSurface() : base()
		{
            AddService(typeof(ComponentSerializationService), new CodeDomComponentSerializationService(this));
            AddService(typeof(IDesignerSerializationService), new DesignerSerializationService(this));
            AddService(typeof(IMenuCommandService), new MyMenuCommandServices(this));
            MyUndoEngine undoEngine = new MyUndoEngine(this);
            //undoEngine.Enabled = false;
            AddService(typeof(UndoEngine), undoEngine);
		}
		public HostSurface(IServiceProvider parentProvider) : base(parentProvider)
		{
            AddService(typeof(ComponentSerializationService), new CodeDomComponentSerializationService(this));
            AddService(typeof(IDesignerSerializationService), new DesignerSerializationService(this));
            AddService(typeof(IMenuCommandService), new MyMenuCommandServices(this));
            MyUndoEngine undoEngine = new MyUndoEngine(this);
            //undoEngine.Enabled = false;
            AddService(typeof(UndoEngine), undoEngine);
        }

		internal void Initialize()
		{

			Control control = null;
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));

			if (host == null)
				return;

			try
			{
				// Set the backcolor
				Type hostType = host.RootComponent.GetType();
				if(hostType==typeof(Form))
				{
					control = this.View as Control;
					control.BackColor = Color.White;
				}
				else if (hostType == typeof(UserControl))
				{
					control = this.View as Control;
					control.BackColor = Color.White;
				}
				else if (hostType == typeof(Component))
				{
					control = this.View as Control;
					control.BackColor = Color.FloralWhite;
				}
				else
				{
					throw new Exception("Undefined Host Type: " + hostType.ToString());
				}

                // Loading control configuration xml
                _xDoc.Load(Application.StartupPath + "\\Components.xml");

				// Set SelectionService - SelectionChanged event handler
				_selectionService = (ISelectionService)(this.ServiceContainer.GetService(typeof(ISelectionService)));
				_selectionService.SelectionChanged += new EventHandler(selectionService_SelectionChanged);
			}
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

		public BasicDesignerLoader Loader
		{
			get
			{
				return _loader;
			}
			set
			{
				_loader = value;
			}
		}

		/// <summary>
        /// When the selection changes this sets the PropertyGrid's selected component 
		/// </summary>
        private void selectionService_SelectionChanged(object sender, EventArgs e)
		{
			if (_selectionService != null)
			{
				ICollection selectedComponents = _selectionService.GetSelectedComponents();
                // Now, we only support single selected object
                if (selectedComponents == null || selectedComponents.Count != 1)
                    return;

				PropertyGrid propertyGrid = (PropertyGrid)this.GetService(typeof(PropertyGrid));

				object[] comps = new object[selectedComponents.Count];
				int i = 0;

				foreach (Object o in selectedComponents)
				{
					comps[i] = o;
					i++;
				}

                XmlNode tmpXNode = null;
                CProperty cp = null;
                Type compType = comps[0].GetType();
                if (compType == typeof(System.Windows.Forms.Button))
                {
                    tmpXNode = _xDoc.SelectSingleNode("Components/Component[@Name=\"Button\"]");
                }
                else if (compType == typeof(System.Windows.Forms.Label))
                {
                    tmpXNode = _xDoc.SelectSingleNode("Components/Component[@Name=\"Label\"]");
                }


                if (tmpXNode != null)
                {
                    XmlNodeList tmpXPropLst = tmpXNode.SelectNodes("Propertys/Property");
                    cp = new CProperty(comps[0], tmpXPropLst);
                    propertyGrid.SelectedObject = cp;
                }
                else
                {
                    propertyGrid.SelectedObjects = comps;
                }
			}
		}

		public void AddService(Type type, object serviceInstance)
		{
			this.ServiceContainer.AddService(type, serviceInstance);
		}
	}// class
}// namespace
