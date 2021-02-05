using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<int> IdMedida { get; set; }
        public Nullable<decimal> CostoXGramo { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string CodigoBarra { get; set; }
        public Nullable<int> IdUnidadMedidaEntrada { get; set; }
        public Nullable<int> IdUnidadMedidaSalida { get; set; }
        public Nullable<decimal> Costo { get; set; }
        public Nullable<decimal> Vendido { get; set; }
        public Nullable<decimal> PesoGramos { get; set; }
        public Nullable<decimal> PesoLata { get; set; }
        public Nullable<bool> EsPintura { get; set; }
        public Nullable<decimal> CostoInventario { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio1 { get; set; }
        public Nullable<decimal> PRecio2 { get; set; }
        public Nullable<decimal> Precio3 { get; set; }
        public Nullable<int> IdDepartamento { get; set; }
        public Nullable<bool> NoITBMS { get; set; }
        public Nullable<int> CantidadVendidas { get; set; }
        public Nullable<bool> EsKiosko { get; set; }
        public byte[] Img { get; set; }
        public string Descripcion1 { get; set; }
        public string Descripcion2 { get; set; }
        public Nullable<int> CantidadEntradas { get; set; }
        public Nullable<int> CantidadTraspasos { get; set; }
        public string NumeroReferencia { get; set; }
        public Nullable<int> PuntoReorden { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> Unidades { get; set; }
        public Nullable<int> Factor { get; set; }
        public Nullable<bool> EsServicio { get; set; }
        public Nullable<bool> EsAutomotriz { get; set; }
        public Nullable<int> IdTipoRegistro { get; set; }
        public Nullable<int> Porcentaje1 { get; set; }
        public Nullable<int> Porcentaje2 { get; set; }
        public Nullable<int> Porcentaje3 { get; set; }
        public string CuentaContable { get; set; }
        public string CuentaContableDev { get; set; }
        public string CuentaCompras { get; set; }
        public Nullable<bool> Deshabilitar { get; set; }
        public string CuentaITBM { get; set; }
        public string CuentaContaItbm { get; set; }
        public Nullable<int> IdSeccion { get; set; }
        public Nullable<int> IdSubCategoria { get; set; }
        public Nullable<int> IdGondola { get; set; }
        public Nullable<int> IdImpuesto { get; set; }
    }
}
