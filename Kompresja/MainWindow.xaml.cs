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
using System.IO.Compression;
using System.Diagnostics;

namespace Kompresja
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Send_Click(object send, EventArgs e)
        {
            string spTekst = sp.Text;
            string szTekst = sz.Text;
            if (File.Exists(spTekst) && File.Exists(szTekst))
            {
                CompressFile(spTekst, szTekst);
                MessageBox.Show("Plik został skompresowany i zapisany w " + szTekst);
            }
            else
            {
                MessageBox.Show("Ścieżka do pliku jest nie poprawna. Ścieżka do pliku powina wyglądać tak: C:\\folder\\plik.rozszeżenie pliku");
            }
        }
        static void CompressFile(string sourceFile, string compressedFile)
        {
            using (FileStream originalFileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }
    }
}