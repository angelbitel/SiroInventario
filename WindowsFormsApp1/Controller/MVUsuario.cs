using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1.Controller
{
    internal class MVUsuario
    {
        public MVUsuario() => context = new SiroDb();
        public string Message { get; set; }
        SiroDb context;
        public void Dispose() => context.Dispose();
        public byte[] UserImg(string userName)
        {
            var len = context.Database.SqlQuery<Int64>("SELECT TOP 1 DATALENGTH(Img) FROM Usuarios WHERE Usuario = @Usuario", new SqlParameter("@Usuario", userName)).Single();
            var len2 = Convert.FromBase64String(Settings.Default.Img).Length;
            if (len != len2)
            {
                var img = context.Usuarios.Where(s => s.Usuario == userName && s.Activar == true).Select(s => s.img).ToList()[0];
                Settings.Default.Img = Convert.ToBase64String(img);
                Settings.Default.Save();
                return img;
            }
            else
                return Convert.FromBase64String(Settings.Default.Img);
        }
        public Model.Login UserLogin(string userName)
        {
            var user = new Model.Login { };
            try
            {
                var res = context.Usuarios.Where(s => s.Usuario == userName).Select(s => new Model.Login { IdUsuario = s.IdUsuario, Usuario = s.Usuario, NombreUsuario = s.NombreUsuario, IdPerfil = s.IdPerfil, Contraseña = s.Contraseña }).Take(1).ToList();
                if (res.Count != 0)
                    user = res[0];
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return user;
        }
        public async Task<bool> Verificar()
        {
            bool conecto = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var cnn = new SiroDb())
                    {
                        cnn.Database.Connection.Open();
                        conecto = (cnn.Database.Connection.State == System.Data.ConnectionState.Open);
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    conecto = false;
                }
            });
            return conecto;
        }
    }
}
