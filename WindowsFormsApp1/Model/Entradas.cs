using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Entradas
    {

        [Key]
        public int IdEntrada { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Entrada { get; set; }
        public Nullable<int> IdTipoEntrada { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string NumeroFactura { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public Nullable<bool> Habilitar { get; set; }
        public Nullable<decimal> MontoPagado { get; set; }
        public Nullable<bool> SeGeneroCheque { get; set; }
        public Nullable<bool> Posteado { get; set; }
        public Nullable<int> IdAsiento { get; set; }

        public virtual TiposEntrada TiposEntrada { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual Proveedores Proveedores { get; set; }
    }
}
