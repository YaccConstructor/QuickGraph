using System;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections.Generic;

namespace QuickGraph.Unit.Serialization
{
    [Serializable]
    public sealed class XmlMachine
    {
        private string machineName;
        private string frameworkVersion;
        private string operatingSystem;
        private EnvironmentVariableCollection environmentVariables = new EnvironmentVariableCollection();

        public static XmlMachine Create()
        {
            XmlMachine machine = new XmlMachine();

            machine.MachineName = Environment.MachineName;
            machine.FrameworkVersion = Environment.Version.ToString();
            machine.OperatingSystem = Environment.OSVersion.VersionString;

            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                machine.EnvironmentVariables.Add(
                    new EnvironmentVariable(String.Format("{0}",de.Key), String.Format("{0}",de.Value))
                    );
            }

            return machine;
        }

        [XmlAttribute]
        public string MachineName
        {
            get { return this.machineName; }
            set { this.machineName = value; }
        }

        [XmlAttribute]
        public string FrameworkVersion
        {
            get { return this.frameworkVersion; }
            set { this.frameworkVersion = value; }
        }

        [XmlAttribute]
        public string OperatingSystem
        {
            get { return this.operatingSystem; }
            set { this.operatingSystem = value; }
        }

        [XmlArray("EnvironmentVariables")]
        [XmlArrayItem("EnvironmentVariable")]
        public EnvironmentVariableCollection EnvironmentVariables
        {
            get { return this.environmentVariables; }
        }

        [Serializable]
        public sealed class EnvironmentVariableCollection : List<EnvironmentVariable>
        {}

        [Serializable]
        public sealed class EnvironmentVariable
        {
            private string name;
            private string value;

            public EnvironmentVariable() { }
            public EnvironmentVariable(
                string name,
                string value
                )
            {
                this.name = name;
                this.value = value;
            }

            [XmlAttribute]
            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            [XmlAttribute]
            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }
    }
}
