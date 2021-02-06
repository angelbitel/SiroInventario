using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace WindowsFormsApp1
{
    public partial class Reportes : DevExpress.XtraEditors.XtraForm
    {
        public XtraReport Report { get; internal set; }
        internal string Query { get; set; }
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            documentViewer1.DocumentSource = Report;
            Report.CreateDocument();
        }
    }
}