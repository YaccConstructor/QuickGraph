using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;

namespace QuickGraph.Unit.Reports
{
    public abstract class FileReportBase : ReportBase
    {
        private string outputFolderName = null;
        private string defaultFileExtension;
        private string outputFileName;
        private bool displayOnDisposed = false;

        public FileReportBase(
            IContainer container,
            string defaultFileExtension
            )
            : base(container)
        {
            this.defaultFileExtension = defaultFileExtension.ToLower();
        }

        public bool DisplayOnDisposed
        {
            get { return this.displayOnDisposed; }
            set { this.displayOnDisposed = value; }
        }

        public string OutputFolderName
        {
            get { return this.outputFolderName; }
        }

        public void SetOutputFolderName(string path, string testAssemblyName)
        {
            string folderName = ReportPath.GetName(testAssemblyName,this.CreationTime);
            SetOutputFolderName(Path.Combine(path, folderName));
        }

        public void SetOutputFolderName(string outputFolderName)
        {
            this.outputFolderName = Path.GetFullPath(outputFolderName);
        }

        public string DefaultFileExtension
        {
            get { return this.defaultFileExtension; }
        }

        public string OutputFileName
        {
            get { return this.outputFileName; }
        }

        public override void Generate()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(this.OutputFileName));
            using (StreamWriter swriter = new StreamWriter(this.OutputFileName))
            {
                this.Generate(swriter);
            }
        }

        public abstract void Generate(TextWriter writer);

        public virtual void SetOutputFileName(string outputFileName)
        {
            this.outputFileName = outputFileName.Replace('.', '_');
            if (!this.OutputFileName.ToLower().EndsWith('.' + this.DefaultFileExtension))
                this.outputFileName += '.' + this.DefaultFileExtension;

            this.outputFileName = ReportPath.EscapeFileName(this.outputFileName);
            if (!String.IsNullOrEmpty(this.OutputFolderName))
                this.outputFileName = Path.Combine(
                    this.OutputFolderName,
                    this.OutputFileName);

            this.outputFileName = Path.GetFullPath(this.OutputFileName);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.DisplayOnDisposed && this.OutputFileName != null)
                {
                    System.Diagnostics.Process.Start(this.OutputFileName);
                    this.DisplayOnDisposed = false;
                }
            }
        }

    }
}
