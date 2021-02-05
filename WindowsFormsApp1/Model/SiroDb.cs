using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.Model
{
    public class SiroDb: DbContext
    {

        public SiroDb() : base($"name={Principal.Global.Conexion}") { }
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<TiposEntrada> TiposEntrada { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Entradas> Entradas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Impuestos> Impuestos { get; set; }
    }
}
