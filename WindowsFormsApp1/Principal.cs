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
using WindowsFormsApp1.Properties;
using WindowsFormsApp1.Controller;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Principal : DevExpress.XtraEditors.XtraForm
    {
        BindingList<Model.ProductosCortos> LstProductos = new BindingList<Model.ProductosCortos>();
        BindingList<Model.EntradaProducto> LstDetalleEntrada = new BindingList<Model.EntradaProducto>();
        public static List<Model.Impuestos> LstImpuestos = new List<Model.Impuestos>();
        public static Model.Global Global = new Model.Global();
        public static Principal This { get; set; }
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Global.Conexion = "SiroEntitiesRescafe";
            barEditItem4.EditValue = 1;
            This = this;
            var frm = new Login();
            frm.ShowDialog();
            var db = new MVEntrada();
            db.LstTiposEntrada.ForEach(f => repositoryItemImageComboBoxMovimientos.Items.Add(H.Item(f.IdTipoEntrada, f.TipoEntrada)));
            db.Lstempresas.ForEach(f => repositoryItemImageComboBoxEmpresas.Items.Add(H.Item(f.IdEmpresa, f.Empresa)));
            db.LstProductosCortos.ForEach(f => LstProductos.Add(f));
            LstImpuestos = db.LstImpuestos;
            gridControl1.DataSource = LstDetalleEntrada;
        }
        public void CambiarImagen()
        {
            barButtonItemUsuario.Caption = Settings.Default.UltimoUsuario;
            if (Global.img != null)
                barButtonItemUsuario.Glyph = H.byteArrayToImage(Global.img);
        }
        private void textBoxBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var r2 = LstProductos.Where(s => s.CodigoBarra == textBoxBuscarProducto.Text.Trim() || s.IdProducto == IdProducto(textBoxBuscarProducto.Text.Trim())).OrderByDescending(o => o.Precio1).ToList();

                if (r2 != null && r2.Count != 0)
                {
                    int cnt = 0;
                    if (int.TryParse(textBoxCantidad.Text, out cnt))
                    {
                        var r = r2[0];
                        var nRenglon = new Model.EntradaProducto {Producto = r.Producto , Factor = Factor(r.IdImpuesto), Cantidad = cnt, IdProducto = r.IdProducto, CodigoBarra = r.CodigoBarra, Costo = r.Costo??0m, IdMedida = 1 };
                        LstDetalleEntrada.Add(nRenglon);
                        textBoxBuscarProducto.Focus();
                    }
                }
            }
        }
        private decimal? Factor(int? idImpuesto)
        {
            var fact = 0m;
            var impu = LstImpuestos.SingleOrDefault(s => s.IdImpuesto == idImpuesto);
            if (impu != null)
                fact = impu.Factor;
            return fact;
        }
        private int IdProducto(string valPro)
        {
            int id = 0;
            var t = Regex.Split(valPro, "#.");
            if (t.Count() >= 2)
                int.TryParse(t[t.Length - 1], out id);
            return id;
        }

        private void textBoxCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBoxBuscarProducto.Focus();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("DESEA AGREGAR EL MOVIMIENTO?", "ALERTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var frm = new Login();
                frm.ShowDialog();
            }
        }

        private void barEditItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void repositoryItemImageComboBoxEmpresas_EditValueChanged(object sender, EventArgs e)
        {
            var cmb = (sender as ImageComboBoxEdit).EditValue;
            if (cmb.ToString() == "1")
            {
                Global.Conexion = "SiroEntitiesRescafe";
                var db = new MVEntrada();
                LstProductos.Clear();
                db.LstProductosCortos.ForEach(f => LstProductos.Add(f));
            }
            else
            {
                Global.Conexion = "SiroEntitiesDelicias";
                var db = new MVEntrada();
                LstProductos.Clear();
                db.LstProductosCortos.ForEach(f => LstProductos.Add(f));
            }
        }
    }
}