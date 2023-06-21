using SecurityGuard.WPF.Services;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SecurityGuard.WPF.Components
{
    /// <summary>
    /// Логика взаимодействия для RequestDetailForm.xaml
    /// </summary>
    public partial class RequestDetailForm : UserControl
    {
        public RequestDetailForm()
        {
            InitializeComponent();
        }

        private void BtnSaveWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imagePath = System.IO.Path.GetTempFileName() + ".png";
                PrintWindowService.SaveAsPng(PrintWindowService.GetImage(this), imagePath);
                PrintWindowService.createDocxFromImage(imagePath);
                System.IO.File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception();
            }
        }

        private void BtnSavePdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imagePath;
                string pdfFileName;
                using (MemoryStream ms = new MemoryStream())
                {

                    imagePath = System.IO.Path.GetTempFileName() + ".png";
                    PrintWindowService.SaveAsPng(PrintWindowService.GetImage(this), imagePath);
                    using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF files (.pdf)|.pdf";
                        saveFileDialog.Title = "Save PDF File";

                        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            pdfFileName = saveFileDialog.FileName;
                            MessageBox.Show("Документ успешно сохранен!", "Всплывающее окно", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                        }
                        else
                        {
                            return;
                        }
                        saveFileDialog.Dispose();
                    }
                    ms.Close();
                }
                PrintWindowService.createPdfFromImage(imagePath, pdfFileName);
                System.IO.File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
