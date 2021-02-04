using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.Model
{
    public class SiroDb: DbContext
    {
        public DbSet<TiposEntrada> TiposEntradas { get; set; }
    }
}
