using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string NombreUsuario { get; set; }
        public string EMail { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public Nullable<int> IdTipoUsuario { get; set; }
        public byte[] img { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public Nullable<int> IdEntidad { get; set; }
        public Nullable<decimal> Comision { get; set; }
        public Nullable<bool> Activar { get; set; }
        public Nullable<bool> EsVendedor { get; set; }
        public Nullable<bool> PuedeCotizar { get; set; }
        public Nullable<bool> Autoriza { get; set; }
    }
}