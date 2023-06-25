using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Xceed.Wpf;

namespace SecurityGuard.WPF.Services
{
    public class PrintWindowService
    {
        public static RenderTargetBitmap GetImage(UIElement view)
        {
            Size size = new Size(view.RenderSize.Width, view.RenderSize.Height);

            if (size.IsEmpty)
            {
                return null;
            }
            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {

                context.DrawRectangle(new VisualBrush(view), null, new Rect(0, 0, (int)size.Width, (int)size.Height));

                context.Close();

            }
            result.Render(drawingVisual);
            return result;
        }

        public static void createPdfFromImage(string imageFile, string pdfFile)
        {
            using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER.Rotate(), 0, 0, 0, 0))
            {
                using (var fileStream = new FileStream(pdfFile, FileMode.Create))
                {
                    using (var ms = new MemoryStream())
                    {
                        PdfWriter.GetInstance(document, fileStream).SetFullCompression();
                        document.Open();

                        using (var fs = new FileStream(imageFile, FileMode.Open))
                        {
                            var image = iTextSharp.text.Image.GetInstance(fs);
                            image.ScaleAbsolute(document.PageSize.Width * 1.0f, document.PageSize.Height * 1.0f);
                            image.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                            document.Add(image);
                        }

                        Process.Start("explorer.exe", pdfFile);
                        document.Close();
                    }
                }
            }

        }

        public static void createDocxFromImage(string imageFile)

        {

            DocX document = Xceed.Words.NET.DocX.Create("temp.docx");
            using (Stream imageStream = File.OpenRead(imageFile))
            {

                Xceed.Document.NET.Image image = document.AddImage(imageStream);
                Picture picture = image.CreatePicture();
                picture.Width = 500;
                picture.Height = 500;
                Xceed.Document.NET.Paragraph paragraph = document.InsertParagraph();
                paragraph.AppendPicture(picture);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (.docx)|.docx";
            if (saveFileDialog.ShowDialog() == true)
            {

                document.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Документ успешно сохранен!", "Всплывающее окно", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        public static void SaveAsPng(RenderTargetBitmap src, string targetFile)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            using (var stream = System.IO.File.Create(targetFile))
            {
                encoder.Save(stream);
            }
        }

        public void ExportToPdf(DataTable dt)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("c://sample.pdf", FileMode.Create));
            document.Open();

            iTextSharp.text.Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);

            Array floatArray = Array.CreateInstance(typeof(float), dt.Columns.Count);
            float[] widths = new float[] { };
            for (int i = 0; i < dt.Columns.Count; i++)
                floatArray.SetValue(4f, i);

            table.SetWidths((float[])floatArray);

            table.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(r[i].ToString(), font5));
                    }
                }
            }
            document.Add(table);
            document.Close();
        }
    }
}

