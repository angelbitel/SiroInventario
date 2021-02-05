using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Proveedores
    {
        [Key]
        public int IdProveedor { get; set; }
        public string Proveedores1 { get; set; }
        public string Telefono { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public string Celular { get; set; }
        public string Contacto2 { get; set; }
        public string RUC { get; set; }
        public Nullable<int> DiasMorosidad { get; set; }
        public Nullable<decimal> Corriente { get; set; }
        public Nullable<decimal> Morosidad30 { get; set; }
        public Nullable<decimal> Morosidad60 { get; set; }
        public Nullable<decimal> Morosidad90 { get; set; }
        public Nullable<decimal> Morosidad120 { get; set; }
        public Nullable<decimal> MorosidadMas120 { get; set; }
        public Nullable<decimal> MontoAdeudado { get; set; }
        public Nullable<decimal> TotalMoroso { get; set; }
        public string CuentaContable { get; set; }
        public string Tipo { get; set; }
        public string CuentaContableCredito { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
    }
}