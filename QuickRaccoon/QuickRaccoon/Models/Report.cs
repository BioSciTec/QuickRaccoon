using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRaccoon.Models
{
 /// <summary>
 /// Erstellt einen Druckbaren Report
 /// </summary>
 public class Report
 {
  protected CwaBaseTest CwaTest;

  public Report(CwaBaseTest cwat)
  {
   CwaTest = cwat;
  }

  /// <summary>
  /// Erstellt einen Druckbaren Report mit dem QR-Code codierten Link für die CWA-App und einem QR-Code mit der "CWA Test ID" (hash)
  /// </summary>
  public void Print()
  {
   Bitmap[] pages = CreatePrintingPage(600, 10);

   PrintDocument pd = new PrintDocument();

   int i = 0;
   pd.PrintPage += new PrintPageEventHandler((object o, PrintPageEventArgs pe) =>
   {
    pe.Graphics.DrawImage(pages[i], new System.Drawing.Point(0, 0));
    if (i < pages.Length - 1)
    {
     i++;
     pe.HasMorePages = true;
    }
    else
     pe.HasMorePages = false;
   });

   pd.Print();
  }

  protected int MMToPoints(double mm, int dpi)
  {
   return (int)(mm * dpi / 25.4 + 0.5);//+0,5 am ende zum richtigen runden
  }

  protected Bitmap[] CreatePrintingPage(int dpi, double border, double width = 0, double height = 0)
  {
   int widthDpi = MMToPoints(width == 0 ? 210.0 : width, dpi);
   int heightDpi = MMToPoints(height == 0 ? 297.0 : height, dpi);
   int borderDpi = MMToPoints(border, dpi);
   int printBorderDpi = MMToPoints(25, dpi);

   int pageCount = 1;
   Bitmap[] pages = new Bitmap[pageCount];
   for (int i = 0; i < pageCount; i++)
   {
    pages[i] = new Bitmap(widthDpi, heightDpi, PixelFormat.Format24bppRgb);
    pages[i].SetResolution(dpi, dpi);
    using (Graphics grp = Graphics.FromImage(pages[i]))
     grp.FillRectangle(Brushes.White, 0, 0, widthDpi, heightDpi);
   }

   using (Graphics grfx = Graphics.FromImage(pages[0]))
   {
    StringFormat stringFormat = new()
    {
     Alignment = StringAlignment.Center,
     LineAlignment = StringAlignment.Center
    };

    Bitmap large = CwaTest.GetCwaBarcodeImage();
    large.SetResolution(dpi, dpi);
    grfx.DrawImage(large, borderDpi, borderDpi);
    grfx.DrawString(CwaTest.GetHashLast6(), new Font("Consolas", 10), Brushes.Black, new Rectangle(borderDpi, borderDpi + large.Height, large.Width, 100), stringFormat);


    Bitmap small = CwaTest.GetCwaTestIdBarcodeImage();
    large.SetResolution(dpi, dpi);
    grfx.DrawImage(small, widthDpi - MMToPoints(15, dpi) - borderDpi, borderDpi, MMToPoints(15, dpi), MMToPoints(15, dpi));
    grfx.DrawString(CwaTest.GetHashLast6(), new Font("Consolas", 8), Brushes.Black, new Rectangle(widthDpi - MMToPoints(15, dpi) - borderDpi, borderDpi + MMToPoints(15, dpi), MMToPoints(15, dpi), 60), stringFormat);

   }

   return pages;
  }
 }
}
