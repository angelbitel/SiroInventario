using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    internal class MVOperaciones
    {
        SiroDb context;
        public string Mensaje { get; set; }
        public int Id { get; private set; }

        public MVOperaciones() => context = new SiroDb();

        public bool Guardar(Entradas inf)
        {
            var tl = new List<DetallesEntrada>();
            inf.DetallesEntrada.ToList().ForEach(f => tl.Add(new DetallesEntrada { IdProducto = f.IdProducto, Cantidad = f.Cantidad, CodigoBarra = f.CodigoBarra, Costo = f.Costo, CUP = f.CUP, Descuento = f.Descuento, IdEntrada = f.IdEntrada, ITBM = f.ITBM, IdMaestroCuenta = f.IdMaestroCuenta, IdMaestroCuentaItbm = f.IdMaestroCuentaItbm, FechaExpiracion = f.FechaExpiracion }));
         
            inf.DetallesEntrada = tl;
            context.Entradas.Add(inf);
            try
            {
                List<DetallesEntrada> entradaAnterior = new List<DetallesEntrada>();
                if (inf.IdEntrada > 0)
                    context.DetallesEntrada.Where(w => w.IdEntrada == inf.IdEntrada).ToList().ForEach(f => entradaAnterior.Add(f));

                var operador = context.TiposEntrada.SingleOrDefault(s => s.IdTipoEntrada == inf.IdTipoEntrada).Operacion;
                var arrEntradas = tl.Select(s => s.IdProducto).ToArray();
                inf.DetallesEntrada.ToList().ForEach(f =>
                {
                    var g = inf.DetallesEntrada.Where(s => s.IdProducto == f.IdProducto);
                    //{ IdTipo = 10, Tipo = "INVENTARIO SOBRE ESCRIBIR CANTIDADES" }
                    //{ IdTipo = 11, Tipo = SALIDA INVENTARIO RESTAR A CANTIDADES }
                    //{ IdTipo = 12, Tipo = SALIDA DE SOBRANTES DE INVENTARIO RESTAR A CANTIDADES }
                    var producto = context.Productos.SingleOrDefault(s => s.IdProducto == f.IdProducto);
                    if (f.IdDetalleEntrada == 0)
                    {
                        if (producto != null)
                        {
                            switch (inf.IdTipoEntrada)
                            {
                                case 10://"AÑADIR CANTIDADES A LAS EXISTENCIAS"
                                    producto.Cantidad = f.Cantidad;
                                    break;
                                case 11://"NO HACER NADA"
                                    producto.Cantidad = (producto.Cantidad ?? 0) + (f.Cantidad * operador);
                                    break;
                                case 12://"SOBREESCRIBIR CANTIDADES"
                                    producto.Cantidad = (producto.Cantidad ?? 0) + (f.Cantidad * operador);
                                    break;
                            }
                        }
                    }
                });
                context.SaveChanges();
                Id = inf.IdEntrada;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return false;
            }
            return true;
        }
    }
}
