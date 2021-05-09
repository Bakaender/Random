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
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            float rectSize = 5; 

            //Create XRect where want text center.
            //Save/Restore the gfx state before and after rotating at each rectangle. Probably every rotation if using larger rectangles.
            //Rotate using rectangle center as transform
            //Draw string.
            XRect rectTopLeft = new XRect(page.Width * .25 - (rectSize/2), page.Height * .25 - (rectSize / 2), rectSize, rectSize);
            gfx.Save();
            DrawWords(gfx, font, rectTopLeft);
            gfx.Restore();

            XRect rectTopRight = new XRect(page.Width * .75 - (rectSize / 2), page.Height * .25 - (rectSize / 2), rectSize, rectSize);
            gfx.Save();
            gfx.RotateAtTransform(90, rectTopRight.Center);
            DrawWords(gfx, font, rectTopRight);
            gfx.Restore();

            XRect rectBotLeft = new XRect(page.Width * .25 - (rectSize / 2), page.Height * .75 - (rectSize / 2), rectSize, rectSize);
            gfx.Save();
            gfx.RotateAtTransform(180, rectBotLeft.Center);
            DrawWords(gfx, font, rectBotLeft);
            gfx.Restore();

            XRect rectBotRight = new XRect(page.Width * .75 - (rectSize / 2), page.Height * .75 - (rectSize / 2), rectSize, rectSize);
            gfx.Save();
            gfx.RotateAtTransform(270, rectBotRight.Center);
            DrawWords(gfx, font, rectBotRight);
            gfx.Restore();

            const string filename = "HelloWorld.pdf";
            document.Save(filename);
        }

        private void DrawWords(XGraphics gfx, XFont font, XRect rect)
        {
            gfx.DrawString("Hello, World!", font, XBrushes.Blue, rect, XStringFormats.Center);
            gfx.RotateAtTransform(90, rect.Center);
            gfx.DrawString("Hello, World 2!", font, XBrushes.Red, rect, XStringFormats.Center);
            gfx.RotateAtTransform(45, rect.Center);
            gfx.DrawString("Hello, World 3!", font, XBrushes.Pink, rect, XStringFormats.Center);
            gfx.RotateAtTransform(90, rect.Center);
            gfx.DrawString("Hello, World 4!", font, XBrushes.Purple, rect, XStringFormats.Center);          
        }
    }
}
