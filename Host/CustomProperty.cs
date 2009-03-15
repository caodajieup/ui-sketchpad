#region Header
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Xml;
#endregion

#region Custom Property
class CProperty : ICustomTypeDescriptor
{
    // Current Selected Object
    private object mCurrentSelectObject;
    private Dictionary<string, string> mObjectAttribs = new Dictionary<string, string>();

    public CProperty(object pSelectObject, XmlNodeList pObjectPropertys)
    {
        mCurrentSelectObject = pSelectObject;
        foreach (XmlNode tmpXNode in pObjectPropertys)
        {
            mObjectAttribs.Add(tmpXNode.Attributes["Name"].Value, tmpXNode.Attributes["Caption"].Value);
        }
    }

    #region ICustomTypeDescriptor Members
    public AttributeCollection GetAttributes()
    {
        return TypeDescriptor.GetAttributes(mCurrentSelectObject);
    }

    public string GetClassName()
    {
        return TypeDescriptor.GetClassName(mCurrentSelectObject);
    }

    public string GetComponentName()
    {
        return TypeDescriptor.GetComponentName(mCurrentSelectObject);
    }

    public TypeConverter GetConverter()
    {
        return TypeDescriptor.GetConverter(mCurrentSelectObject);
    }

    public EventDescriptor GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent(mCurrentSelectObject);
    }

    public PropertyDescriptor GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty(mCurrentSelectObject);
    }

    public object GetEditor(Type editorBaseType)
    {
        return TypeDescriptor.GetEditor(mCurrentSelectObject, editorBaseType);
    }

    public EventDescriptorCollection GetEvents(Attribute[] attributes)
    {
        return TypeDescriptor.GetEvents(mCurrentSelectObject, attributes);
    }

    public EventDescriptorCollection GetEvents()
    {
        return TypeDescriptor.GetEvents(mCurrentSelectObject);
    }

    public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    {
        List<CPropertyDescriptor> tmpPDCLst = new List<CPropertyDescriptor>();
        PropertyDescriptorCollection tmpPDC = TypeDescriptor.GetProperties(mCurrentSelectObject, attributes);
        CPropertyDescriptor tmpCPD;

        foreach(PropertyDescriptor tmpPD in tmpPDC)
        {
            if (mObjectAttribs.ContainsKey(tmpPD.Name))
            {
                tmpCPD = new CPropertyDescriptor(mCurrentSelectObject, tmpPD);
                tmpCPD.SetDisplayName(mObjectAttribs[tmpPD.Name]);
                tmpCPD.SetCategory(tmpPD.Category);
                tmpPDCLst.Add(tmpCPD);
            }
        }

        return new PropertyDescriptorCollection(tmpPDCLst.ToArray());
    }

    public PropertyDescriptorCollection GetProperties()
    {
        return TypeDescriptor.GetProperties(mCurrentSelectObject);
    }

    public object GetPropertyOwner(PropertyDescriptor pd)
    {
        return mCurrentSelectObject;
    }
    #endregion
}
#endregion

#region Custom Property Descriptor
class CPropertyDescriptor : PropertyDescriptor
{
    private PropertyDescriptor mProp;
    private object mComponent;
    public CPropertyDescriptor(object pComponent, PropertyDescriptor pPD)
        : base(pPD)
    {
        mCategory = base.Category;
        mDisplayName = base.DisplayName;
        mProp = pPD;
        mComponent = pComponent;
    }

    private string mCategory;
    public override string Category
    {
        get { return mCategory; }
    }

    private string mDisplayName;
    public override string DisplayName
    {
        get { return mDisplayName; }
    }
    public void SetDisplayName(string pDispalyName)
    {
        mDisplayName = pDispalyName;
    }

    public void SetCategory(string pCategory)
    {
        mCategory = pCategory;
    }
    public override bool CanResetValue(object component)
    {
        return mProp.CanResetValue(component);
    }

    public override Type ComponentType
    {
        get { return mProp.ComponentType; }
    }

    public override object GetValue(object component)
    {
        return mProp.GetValue(component);
    }

    public override bool IsReadOnly
    {
        get { return mProp.IsReadOnly; }
    }

    public override Type PropertyType
    {
        get { return mProp.PropertyType; }
    }

    public override void ResetValue(object component) { mProp.ResetValue(component); }
    public override void SetValue(object component, object value) { mProp.SetValue(component, value); }
    
    public override bool ShouldSerializeValue(object component)
    {
        return mProp.ShouldSerializeValue(component);
    }
}
#endregion

//#region CPropertyCollection
//public class CPropertyCollection : List<CProperty>, ICustomTypeDescriptor
//{
//    public new void Add(CProperty value)
//    {
//        base.Add(value);
//    }

//    #region "TypeDescriptor"
//    public String GetClassName()
//    {
//        return TypeDescriptor.GetClassName(this, true);
//    }

//    public AttributeCollection GetAttributes()
//    {
//        return TypeDescriptor.GetAttributes(this, true);
//    }

//    public String GetComponentName()
//    {
//        return TypeDescriptor.GetComponentName(this, true);
//    }



//    public TypeConverter GetConverter()
//    {
//        return TypeDescriptor.GetConverter(this, true);
//    }

//    public EventDescriptor GetDefaultEvent()
//    {
//        return TypeDescriptor.GetDefaultEvent(this, true);
//    }

//    public PropertyDescriptor GetDefaultProperty()
//    {
//        return TypeDescriptor.GetDefaultProperty(this, true);
//    }

//    public object GetEditor(Type editorBaseType)
//    {
//        return TypeDescriptor.GetEditor(this, editorBaseType, true);
//    }

//    public EventDescriptorCollection GetEvents(Attribute[] attributes)
//    {
//        return TypeDescriptor.GetEvents(this, attributes, true);
//    }

//    public EventDescriptorCollection GetEvents()
//    {
//        return TypeDescriptor.GetEvents(this, true);
//    }

//    public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
//    {
//        PropertyDescriptor[] propDes = new PropertyDescriptor[this.Count];

//        for (int i = 0; i < this.Count; i++)
//        {
//            CProperty prop = (CProperty)this[i];

//            propDes[i] = new CPropertyDescriptor(ref prop, attributes);
//        }

//        return new PropertyDescriptorCollection(propDes);
//    }

//    public PropertyDescriptorCollection GetProperties()
//    {
//        return TypeDescriptor.GetProperties(this, true);
//    }

//    public object GetPropertyOwner(PropertyDescriptor pd)
//    {
//        return this;
//    }

//    #endregion
//}

//#endregion