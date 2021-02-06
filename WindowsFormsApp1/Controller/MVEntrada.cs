using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    internal class MVEntrada
    {
        SiroDb context;
        public string Mensaje{ get; set; }
        public MVEntrada() => context = new SiroDb();
        public void Dispose() => context.Dispose();
        internal List<TiposEntrada> LstTiposEntrada
        {
            get
            {
                var lst = new List<TiposEntrada>();
                try
                {
                    lst = context.TiposEntrada.Where(w=> w.EsInventario==true).OrderBy(o=> o.TipoEntrada).ToList();
                    return lst;
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                return lst;
            }
        }
        internal List<Empresas> Lstempresas
        {
            get
            {
                var lst = new List<Empresas>();
                try
                {
                    lst = context.Empresas.Where(w=> w.IdEmpresa <=2).ToList();
                    return lst;
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                return lst;
            }
        }
        internal List<Model.ProductosCortos> LstProductosCortos
        {
            get
            {
                var lst = new List<Model.ProductosCortos>();
                try
                {
                    lst= context.Database.SqlQuery<Model.ProductosCortos>("Select IdProducto, IdImpuesto, Producto, Codigo, CodigoBarra, Costo, Precio1, NoITBMS from Productos WHERE ISNULL(Deshabilitar,0) = 0").ToList();
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                return lst;
            }
        }
        internal List<Impuestos> LstImpuestos
        {
            get
            {
                var lst = new List<Impuestos>();
                try
                {
                    lst = context.Impuestos.ToList();
                    return lst;
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                return lst;
            }
        }
    }
}
