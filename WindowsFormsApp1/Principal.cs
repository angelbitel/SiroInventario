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
using WindowsFormsApp1.Model;
using DevExpress.XtraReports.UI;

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
            barEditItem1.EditValue = 10;
            This = this;
            var frm = new Login();
            frm.ShowDialog();
            if (frm.Cerrar)
                this.Close();
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
                        var nRenglon = new EntradaProducto { Producto = r.Producto , Factor = Factor(r.IdImpuesto), Cantidad = cnt, IdProducto = r.IdProducto, CodigoBarra = r.CodigoBarra, Costo = r.Costo??0m, IdMedida = 1 };
                        LstDetalleEntrada.Add(nRenglon);
                        textBoxCantidad.SelectAll();
                        textBoxCantidad.Focus();
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
                textBoxBuscarProducto.SelectAll();
                textBoxBuscarProducto.Focus();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("DESEA AGREGAR EL MOVIMIENTO?", "ALERTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idTipoEntrada = 0;
                if(int.TryParse(barEditItem1.EditValue.ToString(), out idTipoEntrada))
                {
                    var entrada = new Entradas { Fecha = DateTime.Now, Habilitar = true, IdUsuario = Global.IdUsuario, Posteado = false, IdTipoEntrada = idTipoEntrada };
                    var detallesEntradas = new List<DetallesEntrada>();
                    LstDetalleEntrada.ToList().ForEach(f => detallesEntradas.Add(new DetallesEntrada { Cantidad = f.Cantidad, IdProducto = f.IdProducto, CodigoBarra = f.CodigoBarra, Costo = f.Costo, IdMedida = 15, ITBM = f.ITBM, Total = f.Total, Descuento = f.Descuento, IdDetalleEntrada = f.IdDetalleEntrada, IdEntrada = entrada.IdEntrada, IdMaestroCuenta = f.IdMaestroCuenta, IdMaestroCuentaItbm = f.IdMaestroCuentaITBM, FechaExpiracion = f.FechaExpiracion }));
                    entrada.Total = detallesEntradas.Sum(s => s.Total);
                    entrada.DetallesEntrada = detallesEntradas;
                    var db = new MVOperaciones();
                    if (db.Guardar(entrada))
                    {
                        ImprimirEntrada(db.Id, entrada.IdTipoEntrada ?? 0);
                        LstDetalleEntrada.Clear();

                        var frm = new Login();
                        frm.ShowDialog();
                        if (frm.Cerrar)
                            this.Close();
                    }
                }
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var col = new string[] { "colCodigoBarra" };
            if (col.Contains(e.Column.Name))
            {
                var prod = LstProductos.SingleOrDefault(s => s.CodigoBarra == gridView1.GetFocusedRowCellValue(colCodigoBarra).ToString());
                if (prod != null)
                {
                    gridView1.SetFocusedRowCellValue(colIdProducto, prod.IdProducto);
                    gridView1.SetFocusedRowCellValue(colCosto, prod.Costo);
                }
            }
            var cols = new string[] { "colTotal", "colCantidad" };
            if (cols.Contains(e.Column.Name))
            {
                decimal descuento = 0;
                barStaticItemTotales.Caption = $"{LstDetalleEntrada.Sum(s => s.Total) - descuento}";
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            gridView1.FocusedColumn = gridView1.VisibleColumns[3];
        }

        private void ImprimirEntrada(int id, int idTipoEntrada)
        {
            string nombre = string.Empty;
            switch (idTipoEntrada)
            {
                case 10:
                    nombre = "INVENTARIO";
                    break;
                case 11:
                    nombre = "SALIDA DE INVENTARIO";
                    break;
                default:
                    nombre = "REGISTRO DE MERMAS";
                    break;
            }

            XtraReport report = new XtraReport();
            report.LoadLayout(string.Format(@"Reportes\\{0}.repx", "ENTRADA PROVEEDOR"));
            var query = @"select 
	   A.IdEntrada, A.Fecha,
       A.Entrada, A.IdTipoEntrada,
       A.Total as Entradas_Total, 
	   A.Descuento,
       (SELECT TOP 1 Empresa FROM Empresas) AS Sucursal, 
	   C.Usuario,
       D.IdDetalleEntrada,
       D.Cantidad, 
	   D.Costo,
        CASE WHEN A.IdTipoEntrada IN(10,11) THEN D.Costo*D.Cantidad ELSE D.Costo*D.Cantidad+D.Itbm END AS Total,
	   E.Producto,
       E.CodigoBarra, 
	   E.Precio1,
       E.PRecio2, 
	   E.Precio3,
       F.TipoEntrada, 
        CASE WHEN A.IdTipoEntrada IN(10,11) THEN 0 ELSE D.Itbm END AS Itbm,
	   G.Proveedores,
       G.IdProveedor, 
	   G.Telefono,
       G.URL, G.Email,
       G.Contacto, G.Celular,
       G.Contacto2, G.RUC,
       G.DiasMorosidad, G.Corriente,
       G.Morosidad30, G.Morosidad60,
       G.Morosidad90, G.Morosidad120,
       G.MorosidadMas120, G.MontoAdeudado,
       G.TotalMoroso, G.CuentaContable,
       G.Tipo, G.CuentaContableCredito,
       G.RazonSocial, G.Direccion
  from 
  Entradas A left join 
  Sucursales B on B.IdSucursal = A.IdSucursal
  left join Usuarios C on C.IdUsuario = A.IdUsuario
  inner join DetallesEntrada D on D.IdEntrada = A.IdEntrada
  inner join Productos E on E.IdProducto = D.IdProducto
  inner join TiposEntrada F on F.IdTipoEntrada = A.IdTipoEntrada
  left join Proveedores G on G.IdProveedor  = A.IdProveedor
 where D.IdEntrada = @prmIdEntrada
";
            report.DataSource = H.BindToData(query.Replace("@prmIdEntrada", id.ToString()));
            report.DataMember = "customQuery";
            var frmReport = new Reportes();
            report.Parameters["prmIdEntrada"].Value = id;
            frmReport.Report = report;
            frmReport.Name = $"MOVIMIENTO{id}";
            frmReport.Text = $"MOVIMIENTO ({id})";
            frmReport.ShowDialog();
        }

        private void repositoryItemHyperLinkEditEliminar_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("DESEA ELIMNAR ESTA FILA?", "ALERTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var prod = gridView1.GetFocusedRow() as Model.EntradaProducto;
                if (prod != null)
                {
                    LstDetalleEntrada.Remove(prod);
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LstDetalleEntrada.Clear();
        }
    }
}