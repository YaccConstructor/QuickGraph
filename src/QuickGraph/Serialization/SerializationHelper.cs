using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;

namespace QuickGraph.Serialization
{
    internal struct PropertySerializationInfo
    {
        public readonly PropertyInfo Property;
        public readonly string Name;
        private readonly object _value;
        private readonly bool hasValue;

        public PropertySerializationInfo(
            PropertyInfo property,
            string name)
        {
            this.Property = property;
            this.Name = name;
            this._value = null;
            this.hasValue = false;
        }

        public PropertySerializationInfo(
            PropertyInfo property,
            string name,
            object value)
        {
            this.Property = property;
            this.Name = name;
            this._value = value;
            this.hasValue = this._value != null;
        }

        public bool TryGetDefaultValue(out object value)
        {
            value = this._value;
            return this.hasValue;
        }
    }

    internal static class SerializationHelper    
    {
        public static IEnumerable<PropertySerializationInfo> GetAttributeProperties(Type type)
        {
            var currentType = type;
            while (
                currentType != null &&
                currentType != typeof(object) &&
                currentType != typeof(ValueType))
            {
                foreach (PropertyInfo property in currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    // must have a get, and not be an index
                    if (!property.CanRead || property.GetIndexParameters().Length > 0)
                        continue;
                    // is it tagged with XmlAttributeAttribute?
                    string name;
                    if (TryGetAttributeName(property, out name))
                    {
                        object value;
                        if (TryGetDefaultValue(property, out value))
                            yield return new PropertySerializationInfo(property, name, value);
                        else
                            yield return new PropertySerializationInfo(property, name);
                    }
                }

                currentType = currentType.BaseType;
            }
        }

        public static bool TryGetAttributeName(PropertyInfo property, out string name)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(XmlAttributeAttribute))
                as XmlAttributeAttribute;
            if (attribute == null)
            {
                name = null;
                return false;
            }
            else
            {
                if (String.IsNullOrEmpty(attribute.AttributeName))
                    name = property.Name;
                else
                    name = attribute.AttributeName;
                return true;
            }
        }

        public static bool TryGetDefaultValue(PropertyInfo property, out object value)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute))
                as DefaultValueAttribute;
            if (attribute == null)
            {
                value = null;
                return false;
            }
            else
            {
                value = attribute.Value;
                return true;
            }
        }
    }
}
