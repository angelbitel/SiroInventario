using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Empresas
    {
        [Key]
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Ruc { get; set; }
        public string Dv { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public byte[] img { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> IdItbms { get; set; }
        public Nullable<int> IdBanco { get; set; }
        public Nullable<int> IdIngresos { get; set; }
        public Nullable<int> IdEfectivoBanco { get; set; }
        public Nullable<int> IdCaja { get; set; }
        public string Politicas { get; set; }
        public string Fiscal { get; set; }
        public string PoliticaFiscal { get; set; }
        public string TipoEmpresa { get; set; }
    }
}
