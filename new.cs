using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace testing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            const string filename = "HelloWorld-rotated90.pdf";

            PdfDocument document = PdfReader.Open(filename, PdfDocumentOpenMode.Modify);

            document.Info.Title = "Import test";

            PdfPage page = document.Pages[0];
            
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            float barHeight = 25; 
            double offset = page.Width - page.Height;
            
            XRect test = page.MediaBox.ToXRect();
            test.Y = -offset;
            XRect right = new XRect(0 - (offset / 2), test.Y + (offset / 2), barHeight, page.Height);
            XRect left = new XRect(page.Height - barHeight + (offset / 2), test.Y + (offset / 2), barHeight, page.Height);
            
            double h = test.Height;
            gfx.Save();
            gfx.RotateAtTransform(90, test.Center);
            test.Height = test.Width;
            test.Width = h;
            test.Y += offset / 2;
            test.X -= offset / 2;

            //gfx.DrawRectangle(XBrushes.Blue, test);
            gfx.DrawRectangle(XBrushes.Green, left);
            gfx.DrawRectangle(XBrushes.LightGreen, right);

            gfx.Restore();
            gfx.Save();
            gfx.RotateAtTransform(180, test.Center);
            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XPoint((page.Height / 2), -offset + (barHeight / 2)), XStringFormats.Center);
            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XPoint((page.Height / 2), page.Height - (barHeight / 2)), XStringFormats.Center);
            gfx.Restore();
            
            document.Save(filename);
        }
    }
}
