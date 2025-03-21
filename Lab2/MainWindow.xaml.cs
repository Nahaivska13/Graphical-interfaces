using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Lab2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Реєстрація команд
            var openBinding = new CommandBinding(ApplicationCommands.Open, Execute_Open, CanExecute_Open);
            var saveBinding = new CommandBinding(ApplicationCommands.Save, Execute_Save, CanExecute_Save);

            CommandBindings.Add(openBinding);
            CommandBindings.Add(saveBinding);
        }

        // Перевірка для Save 
        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(MainTextBox.Text);
        }

        // Обробка команди Save
        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveDialog.FileName, MainTextBox.Text);  // Збереження тексту у файл
                MessageBox.Show("The file was saved!");
            }
        }

        // Перевірка для Open 
        private void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Обробка команди Open
        private void Execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog()
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (openDialog.ShowDialog() == true)
            {
                MainTextBox.Text = File.ReadAllText(openDialog.FileName);  // Відкриття тексту з файлу
            }
        }

        // Обробник для кнопки Clear
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Clear();  // Очистити текстове поле
        }
    }
}
