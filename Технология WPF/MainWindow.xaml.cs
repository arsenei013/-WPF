using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Технология_WPF
{
    public partial class MainWindow : Window
    {
        private int docCounter = 1;
        private ChildWindow activeChildWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenChildWindow(string title, string content, string filePath = null)
        {
            ChildWindow child = new ChildWindow(title, content, filePath);
            child.Owner = this;

            child.Activated += (s, e) => activeChildWindow = child;
            child.Closed += (s, e) =>
            {
                if (activeChildWindow == child)
                    activeChildWindow = null;
            };

            activeChildWindow = child;
            child.Show();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindow("Новый документ " + docCounter++, "");
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                string fileName = Path.GetFileName(filePath);

                OpenChildWindow(fileName, fileContent, filePath);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (activeChildWindow != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FileName = activeChildWindow.Title;

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, activeChildWindow.DocumentText);
                    activeChildWindow.Title = Path.GetFileName(saveFileDialog.FileName);
                    activeChildWindow.FilePath = saveFileDialog.FileName;
                    MessageBox.Show("Файл успешно сохранен!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Нет открытого окна документа для сохранения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
