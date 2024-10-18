using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using Microsoft.Win32; // Dla OpenFileDialog
using System.Windows.Forms; // Dla FolderBrowserDialog

namespace Kompresja
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath;
        private string saveFolderPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
            }
        }

        private void SelectSaveFolderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    saveFolderPath = folderDialog.SelectedPath;
                }
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath) && !string.IsNullOrEmpty(saveFolderPath))
            {
                string zipFilePath = Path.Combine(saveFolderPath, Path.GetFileNameWithoutExtension(selectedFilePath) + ".zip");
                CompressFile(selectedFilePath, zipFilePath);
                System.Windows.MessageBox.Show("Plik został skompresowany i zapisany w " + zipFilePath);
            }
            else
            {
                System.Windows.MessageBox.Show("Proszę wybrać zarówno plik do kompresji, jak i folder do zapisu.");
            }
        }

        static void CompressFile(string sourceFile, string zipFile)
        {
            // Użycie ZipFile do kompresji pliku
            using (var zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                zipArchive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile));
            }
        }
    }
}
