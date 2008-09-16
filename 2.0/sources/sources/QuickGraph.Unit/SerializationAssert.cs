using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Xml.Serialization;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    public static class SerializationAssert
    {
        public static void IsSerializable(Type type)
        {
            SerializableAttribute attribute = ReflectionHelper.GetAttribute<SerializableAttribute>(type);
            Assert.IsNotNull(attribute,
                "{0} is not tagged with [SerializableAttribute]", type);
        }

        public static void IsXmlSerializable(Type type)
        {
            new XmlSerializer(type);
        }
    }
}
