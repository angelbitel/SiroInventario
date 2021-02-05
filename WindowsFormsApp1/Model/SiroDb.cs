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

        public SiroDb() : base("name=SiroEntitiesConection") { }
        public DbSet<TiposEntrada> TiposEntradas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Entradas> Entradas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
    }
}
