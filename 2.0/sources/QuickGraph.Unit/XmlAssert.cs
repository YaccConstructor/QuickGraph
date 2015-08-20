using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace QuickGraph.Unit
{
    public static class XmlAssert
    {
        public static XmlDocument IsWellFormedXml(string xml)
        {
            Assert.IsNotNull(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        public static XmlDocument IsWellFormed(string fileName)
        {
            Assert.IsNotNull(fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return doc;
        }

        public static XmlDocument IsWellFormed(TextReader reader)
        {
            Assert.IsNotNull(reader);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            return doc;
        }

        public static XmlDocument IsWellFormed(XmlReader reader)
        {
            Assert.IsNotNull(reader);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            return doc;
        }

        public static XmlDocument IsWellFormed(Stream stream)
        {
            Assert.IsNotNull(stream);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            return doc;
        }

        public static XmlSchema IsSchemaValid(Stream stream, bool warningsAsError)
        {
            Assert.IsNotNull(stream);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.ValidateSchema(stream);
        }

        public static XmlSchema IsSchemaValid(TextReader reader, bool warningsAsError)
        {
            Assert.IsNotNull(reader);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.ValidateSchema(reader);
        }

        public static XmlSchema IsSchemaValid(XmlReader reader, bool warningsAsError)
        {
            Assert.IsNotNull(reader);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.ValidateSchema(reader);
        }

        public static XmlSchema IsSchemaValid(string fileName, bool warningsAsError)
        {
            Assert.IsNotNull(fileName);
            using (StreamReader reader = new StreamReader(fileName))
            {
                return IsSchemaValid(reader, warningsAsError);
            }
        }

        public static XmlSchema IsSchemaValidXml(string xml, bool warningsAsError)
        {
            Assert.IsNotNull(xml);
            using (StringReader reader = new StringReader(xml))
            {
                return IsSchemaValid(reader, warningsAsError);
            }
        }

        public static XmlDocument IsValidXml(string xml, bool warningsAsError)
        {
            Assert.IsNotNull(xml);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.Validate(IsWellFormedXml(xml));
        }

        public static XmlDocument IsValid(string fileName, bool warningsAsError)
        {
            Assert.IsNotNull(fileName);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.Validate(IsWellFormed(fileName));
        }

        public static XmlDocument IsValid(XmlReader reader, bool warningsAsError)
        {
            Assert.IsNotNull(reader);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.Validate(IsWellFormed(reader));
        }

        public static XmlDocument IsValid(TextReader reader, bool warningsAsError)
        {
            Assert.IsNotNull(reader);
            SchemaValidator validator = new SchemaValidator(warningsAsError);
            return validator.Validate(IsWellFormed(reader));
        }

        private sealed class SchemaValidator
        {
            private int errorCount = 0;
            private int warningCount = 0;
            private bool warningsAsError = false;

            public SchemaValidator(bool warningsAsError)
            {
                this.warningsAsError = warningsAsError;
            }

            public XmlDocument Validate(XmlDocument document)
            {
                document.Validate(new ValidationEventHandler(this.ValidationEvent));
                this.ValidateResults();
                return document;
            }

            public XmlDocument Validate(XmlDocument document, XmlNode nodeToValidate)
            {
                document.Validate(new ValidationEventHandler(this.ValidationEvent), nodeToValidate);
                this.ValidateResults();
                return document;
            }

            public XmlSchema ValidateSchema(Stream stream)
            {
                XmlSchema schema = XmlSchema.Read(
                    stream,
                    new ValidationEventHandler(ValidationEvent));
                this.ValidateResults();
                return schema;
            }

            public XmlSchema ValidateSchema(TextReader reader)
            {
                XmlSchema schema = XmlSchema.Read(
                    reader,
                    new ValidationEventHandler(ValidationEvent));
                this.ValidateResults();
                return schema;
            }

            public XmlSchema ValidateSchema(XmlReader reader)
            {
                XmlSchema schema = XmlSchema.Read(
                    reader,
                    new ValidationEventHandler(ValidationEvent));
                this.ValidateResults();
                return schema;
            }

            private void ValidateResults()
            {
                Console.WriteLine("Validation: {0} errors, {1} warnings",
                    this.errorCount,
                    this.warningCount);
                if (this.errorCount > 0)
                    Assert.Fail("{0} error in schema", this.errorCount);
                if (this.warningsAsError && this.warningCount > 0)
                    Assert.Fail("{0} warnings in schema", this.warningCount);
            }

            private void ValidationEvent(Object sender, ValidationEventArgs e)
            {
                switch (e.Severity)
                {
                    case XmlSeverityType.Error:
                        errorCount++; break;
                    case XmlSeverityType.Warning:
                        warningCount++; break;
                }

                Console.WriteLine("{0}: {1}", e.Severity, e.Message);
                Console.WriteLine(e.Exception);
                Console.WriteLine();
            }
        }
    }
}
