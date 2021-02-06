using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        private byte[] Img { get; set; }
        internal string Usuario { get; set; }
        internal bool Entrar { get; set; }
        private bool Conectado { get; set; }
        internal string Contrasena { get; set; }

        public Login()
        {
            InitializeComponent();
            this.Load += Login_Load;
        }

        private async void Login_Load(object sender, EventArgs e)
        {
            Cerrar = true;
            BuscarInforImagen(Settings.Default.UltimoUsuario);
            
            textEditUsuario.Enabled = false;
            if (Contrasena != null)
            {
                textEdit2.Text = Contrasena;
                textEditUsuario.Text = Usuario;
                EjecutarBusqueda();
            }
            Timer timer = new Timer { Interval = 500, Enabled = false };
            timer.Start();

            timer.Tick += new EventHandler(timer_Tick);
            Usuario = Settings.Default.UltimoUsuario;
            if (Settings.Default.UltimoUsuario != null && Settings.Default.UltimoUsuario !=string.Empty)
            {
                textEditUsuario.Text = Settings.Default.UltimoUsuario;
                Text = string.Format("BIENVENIDO {0} a SIRO", Settings.Default.UltimoUsuario);
                BuscarInforImagen(Settings.Default.UltimoUsuario);
                textEdit2.Focus();
            }
            var cnn = new Controller.MVUsuario().Verificar();
            var allTasks = new List<Task> { cnn };
            Conectado = true;
            while (allTasks.Any())
            {
                Task finished = await Task.WhenAny(allTasks);
                if (finished == cnn)
                {
                    if (!cnn.Result)
                    {
                        barStaticItem1.Caption = "SIRO NO PUEDE COMUNICARSE CON EL SERVIDOR DE DATOS..";
                        Conectado = false;
                    }
                }
                allTasks.Remove(finished);
            }
            textEditUsuario.Enabled = true;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (Conectado) lblLet.ItemAppearance.Normal.ForeColor = Color.Green;
            else lblLet.ItemAppearance.Normal.ForeColor = Color.Red;
            if (lblLet.Caption.Count() < 5)
                lblLet.Caption = lblLet.Caption + ".";
            else
                lblLet.Caption = ".";
        }
        private void ExtraxPwd()
        {
            var pwd = new Char[textEdit2.Text.Length];
            for (int i = 0; i < pwd.Length; i++)
            {
                pwd[i] = '*';
            }
            barStaticItem1.Caption = new string(pwd);
        }
        internal bool Cerrar { get; set; }
        private void EjecutarBusqueda()
        {
            var res = new Model.Login();
            res = new Controller.MVUsuario().UserLogin(textEditUsuario.Text) ;
            string pwd = res != null ? res.Contraseña : string.Empty;
            if (pwd == textEdit2.Text)
            {
                Settings.Default.UltimoUsuario = textEditUsuario.Text;
                Settings.Default.Save();
                Principal.Global.Habilitar = true;
                Principal.Global.IdUsuario = res.IdUsuario;
                Principal.Global.Usuario = textEditUsuario.Text;
                Principal.Global.img = Img;
                Principal.Global.NombreCompleto = res.NombreUsuario;
                Principal.Global.IdPerfil = res.IdPerfil ?? 0;
                Entrar = true;
                Principal.This.CambiarImagen();
                Cerrar = false;
                Close();
            }
            else
            {
                barStaticItem1.Caption = "Usuario o Contraseña Incorrecto, Intente Nuevamente !";
            }
        }

        private void textEdit2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EjecutarBusqueda();
            }
            else
                ExtraxPwd();
        }
        private void BuscarInforImagen(string usrs)
        {
            Task.Run(async () =>
            {
                try
                {

                    var usr = new Controller.MVUsuario().UserImg(usrs.Trim());
                    if (usr == null) return;
                    Img = usr;
                    pictureEdit1.EditValue = Img;
                }
                catch (Exception) { }

            }).GetAwaiter().GetResult();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
                textEdit2.Properties.PasswordChar = '\0';
            else
                textEdit2.Properties.PasswordChar = '*';
            textEdit2.Focus();
        }
    }
}