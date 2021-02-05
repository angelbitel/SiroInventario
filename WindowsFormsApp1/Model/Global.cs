using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    public class Global
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int IdTipoUsuario { get; set; }
        public DateTime Desde { get; set; }
        public int IdCliente { get; set; }
        public int IdAgenda { get; set; }
        //public NavBarItem[] ItemMenu { get; set; }
        public bool Habilitar { get; set; }
        public byte[] img { get; set; }
        public bool EsCliente { get; set; }
        public DateTime HoraInicia { get; set; }
        public int IdClienteAnterior { get; set; }
        //public List<Model.PerfilEntidad> PerfilesDatos { get; set; }
        public string NombreCompleto { get; set; }

        public int IdPerfil { get; set; }
        public decimal ITBMS { get; set; }        
        public bool EsActualizacion { get; set; }

        public string Version { get; set; }

        public string Spoolers { get; set; }

        public string PuertoCom { get; set; }

        public string UsarFiscal { get; set; }

        public bool UsarFiscalVieja { get; set; }

        public bool CalculaItbm { get; set; }

        public bool UsarPrecio3 { get; set; }

        public string CnnReport { get; set; }

        public bool UsarImpresoraFiscal { get; set; }
        public bool PwdPrecio { get; set; }
        public bool ImprimirDetalle { get; set; }

        public bool InhabilitarPwdPrecios { get; set; }

        public string ImpresoraFiscal { get; set; }
        public int UltimaTransaccion { get; set; }
        public int UltimaNotaCredito { get; set; }
        public bool EsNotaCredito { get; internal set; }
        public int IdUsuarioSodas { get; internal set; }
        public bool ActivarContabilidad { get; set; }
        public int IdEmpresaSeleccionada { get; set; }
        public bool HabilitarVendedor { get; set; }
        public int[] IconZise { get; set; }
        public DateTime Hasta { get; set; }
        public bool HabilitarDescuentoJuvilado { get; set; }
        public bool HabilitarSincronizacion { get; internal set; }
        public string Conexion { get; set; }
    }
}
