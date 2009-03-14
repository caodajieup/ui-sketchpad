#region Header
using System.ComponentModel;
using System;
using System.Collections.Generic;
#endregion

#region CProperty
public class CProperty
{

    private string m_Name = string.Empty;

    private bool m_bReadOnly = false;

    private bool m_bVisible = true;

    private object m_Value = null;

    private string m_Category = string.Empty;

    TypeConverter m_Converter = null;

    object m_Editor = null;


    public CProperty(string name, object value)
    {

        m_Name = name;

        m_Value = value;

    }

    public CProperty(string name, object value, bool bReadOnly, bool bVisible)
    {
        m_Name = name;

        m_Value = value;

        m_bReadOnly = bReadOnly;

        m_bVisible = bVisible;

    }

    public bool ReadOnly
    {

        get { return m_bReadOnly; }

        set { m_bReadOnly = value; }

    }

    public virtual TypeConverter Converter
    {

        get { return m_Converter; }

        set { m_Converter = value; }

    }

    public string Name
    {

        get { return m_Name; }

        set { m_Name = value; }

    }

    public bool Visible
    {

        get { return m_bVisible; }

        set { m_bVisible = value; }

    }

    public virtual object Value
    {

        get { return m_Value; }

        set { m_Value = value; }

    }

    public string Category
    {

        get { return m_Category; }

        set { m_Category = value; }

    }

    public virtual object Editor
    {

        get { return m_Editor; }

        set { m_Editor = value; }

    }

}
#endregion

#region CPropertyCollection
public class CPropertyCollection : List<CProperty>, ICustomTypeDescriptor
{
    public new void Add(CProperty value)
    {
        base.Add(value);
    }

    #region "TypeDescriptor"
    public String GetClassName()
    {
        return TypeDescriptor.GetClassName(this, true);
    }

    public AttributeCollection GetAttributes()
    {
        return TypeDescriptor.GetAttributes(this, true);
    }

    public String GetComponentName()
    {
        return TypeDescriptor.GetComponentName(this, true);
    }



    public TypeConverter GetConverter()
    {
        return TypeDescriptor.GetConverter(this, true);
    }

    public EventDescriptor GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent(this, true);
    }

    public PropertyDescriptor GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty(this, true);
    }

    public object GetEditor(Type editorBaseType)
    {
        return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    public EventDescriptorCollection GetEvents(Attribute[] attributes)
    {
        return TypeDescriptor.GetEvents(this, attributes, true);
    }

    public EventDescriptorCollection GetEvents()
    {
        return TypeDescriptor.GetEvents(this, true);
    }

    public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    {
        PropertyDescriptor[] propDes = new PropertyDescriptor[this.Count];

        for (int i = 0; i < this.Count; i++)
        {
            CProperty prop = (CProperty)this[i];

            propDes[i] = new CPropertyDescriptor(ref prop, attributes);
        }

        return new PropertyDescriptorCollection(propDes);
    }

    public PropertyDescriptorCollection GetProperties()
    {
        return TypeDescriptor.GetProperties(this, true);
    }

    public object GetPropertyOwner(PropertyDescriptor pd)
    {
        return this;
    }

    #endregion
}

#endregion

#region CPropertyDescriptor
public class CPropertyDescriptor : PropertyDescriptor
{

    CProperty m_Property;

    public CPropertyDescriptor(ref CProperty property, Attribute[] attrs)
        : base(property.Name, attrs)
    {
        m_Property = property;
    }

    #region PropertyDescriptor "region"
    public override bool CanResetValue(object component)
    {
        return false;
    }

    public override Type ComponentType
    {
        get { return null; }
    }

    public override object GetValue(object component)
    {
        return m_Property.Value;
    }

    public override string Description
    {
        get { return m_Property.Name; }
    }

    public override string Category
    {
        get { return m_Property.Category; }
    }

    public override string DisplayName
    {
        get { return m_Property.Name; }
    }

    public override bool IsReadOnly
    {
        get { return m_Property.ReadOnly; }
    }

    public override TypeConverter Converter
    {
        get { return m_Property.Converter; }
    }

    public override void ResetValue(object component)
    {
    }

    public override bool ShouldSerializeValue(object component)
    {
        return false;
    }

    public override void SetValue(object component, object value)
    {
        m_Property.Value = value;
    }

    public override Type PropertyType
    {
        get { return m_Property.Value.GetType(); }
    }

    public override object GetEditor(Type editorBaseType)
    {
        return m_Property.Editor == null ? base.GetEditor(editorBaseType) : m_Property.Editor;
    }

    #endregion
}

#endregion