using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class TiposEntrada
    {
        [Key]
        public int IdTipoEntrada { get; set; }
        public string TipoEntrada { get; set; }
        public int Operacion { get; set; }
    }
}
