using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.Model
{
    public class Impuestos
    {
        [Key]
        public int IdImpuesto { get; set; }
        public string Impuesto { get; set; }
        public decimal Factor { get; set; }
    }
}