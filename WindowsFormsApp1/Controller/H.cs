using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    //HERRAMIENTAS
    public class H
    {
        internal static Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);
            }
            catch { }
            return Resize(returnImage, 20, 20);
        }
        public static Bitmap Resize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        internal static ImageComboBoxItem Item(string desc, string value) => new ImageComboBoxItem { Description = desc, Value = value };
        internal static ImageComboBoxItem Item(string desc, int value) => new ImageComboBoxItem { Description = desc, Value = value };
        internal static ImageComboBoxItem Item(int value, string desc) => new ImageComboBoxItem { Description = desc, Value = value };
        internal static SqlDataSource BindToData(string queryComand)
        {
            //BUSCAR LA CADENA DE CONEXXION DEL CONTENEDOR DE DATOS SELLECCIONADO StoredProcQuery 
            var cnnContext = new SiroDb();
            var cnn = cnnContext.Database.Connection.ConnectionString.Split(';');
            //// Create a data source with the required connection parameters.
            MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters(cnn[0].Split('=')[1], cnn[1].Split('=')[1], cnn[3].Split('=')[1], cnn[4].Split('=')[1], MsSqlAuthorizationType.SqlServer);
            SqlDataSource ds = new SqlDataSource(connectionParameters);
            CustomSqlQuery query = new CustomSqlQuery();

            query.Name = @"customQuery";
            query.Sql = queryComand;
            // Add the query to the collection. 
            ds.Queries.Add(query);
            // Make the data source structure displayed  
            // in the Field List of an End-User Report Designer. 
            ds.RebuildResultSchema();

            return ds;
        }
    }
}
