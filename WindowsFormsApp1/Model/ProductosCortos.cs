using System;

namespace WindowsFormsApp1.Model
{
    public class ProductosCortos
    {
        public int IdProducto { get; set; }
        public int? IdImpuesto { get; set; }
        public string Producto { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Precio1 { get; set; }
        public bool? NoITBMS { get; set; }
    }
}
