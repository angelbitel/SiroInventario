using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class DetallesEntrada
    {
        [Key]
        public int IdDetalleEntrada { get; set; }
        public int? IdEntrada { get; set; }
        public int? IdProducto { get; set; }
        public int? IdMedida { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? ML { get; set; }
        public decimal? TSP { get; set; }
        public decimal? TBSP { get; set; }
        public decimal? FLUID_ONCES { get; set; }
        public decimal? CUP { get; set; }
        public decimal? PINT { get; set; }
        public decimal? QUART { get; set; }
        public decimal? GALLON { get; set; }
        public decimal? OUNCE { get; set; }
        public decimal? GRAM { get; set; }
        public decimal? POUND { get; set; }
        public decimal? LITER { get; set; }
        public string CodigoBarra { get; set; }
        public decimal? Costo { get; set; }
        public decimal? ITBM { get; set; }
        public decimal? Total { get; set; }
        public decimal? Descuento { get; set; }
        public int? IdMaestroCuenta { get; set; }
        public int? IdMaestroCuentaItbm { get; set; }
        public System.DateTime? FechaExpiracion { get; set; }
    }
}
