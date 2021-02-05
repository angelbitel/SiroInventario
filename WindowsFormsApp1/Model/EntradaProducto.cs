using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFormsApp1.Model
{
    public class EntradaProducto : INotifyPropertyChanged
    {
        public int IdDetalleEntrada { get; set; }
        public int? IdEntrada { get; set; }
        public int? IdProducto { get; set; }
        public string Producto { get; set; }
        public int? IdMedida { get; set; }
        public string CodigoBarra { get; set; }
        public bool NoItbms { get; set; }
        public decimal? Monto
        {
            get
            {
                return monto;
            }
            set
            {
                if (value != monto)
                {
                    monto = value ?? 0M;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Costo
        {
            get
            {
                return costo;
            }
            set
            {
                if (value != costo)
                {
                    costo = value ?? 0M;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Total { get; set; }
        public decimal? ITBM { get; set; }
        public decimal? Cantidad
        {
            get
            {
                return cantidad;
            }
            set
            {
                if (value != cantidad)
                {
                    cantidad = value;
                    NotifyPropertyChanged();
                }
            }
        }
        decimal? cantidad { get; set; }
        decimal? monto { get; set; }
        decimal? costo { get; set; }
        decimal? descuento { get; set; }
        public decimal? Factor { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            switch (propertyName)
            {
                case "Costo":
                    //ITBM = ((cantidad * (Monto)) * (Factor ?? 0));
                    //Total = ((cantidad * (Monto)) + (ITBM??0m));
                    Total = (cantidad * costo) + ((cantidad * costo) * Factor) - (descuento ?? 0m);
                    if (NoItbms)
                        ITBM = 0;
                    ITBM = ((cantidad * costo) * Factor);
                    break;
                case "Cantidad":
                    if (cantidad != 0)
                        goto case "Costo";
                    break;
                case "Descuento":
                    if (descuento != 0)
                        goto case "Costo";
                    break;
                case "NoItbms":
                    goto case "Costo";
                default:
                    break;
            }
        }
        public bool Habilitar { get; set; }
        public decimal? Descuento
        {
            get
            {
                return descuento;
            }
            set
            {
                if (value != descuento)
                {
                    descuento = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int IdMaestroCuenta { get; set; }
        public string CuentaContable { get; set; }
        public string CodigoCuenta { get; set; }
        public int IdMaestroCuentaITBM { get; set; }
        public string CuentaContableITBM { get; set; }
        public string CodigoCuentaITBM { get; set; }
        public DateTime? FechaExpiracion { get; set; }
    }
}
