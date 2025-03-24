using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notebook

{
    public partial class MainWindow : Window
    {
        // Список записів, що відображаються в ListBox
        public ObservableCollection<string> Notes { get; set; }

        // Новий запис, який додається
        public string NewNote { get; set; }

        // Команди для додавання та видалення записів
        public ICommand AddNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            Notes = new ObservableCollection<string>();
            AddNoteCommand = new RelayCommand(AddNote, CanAddNote);
            RemoveNoteCommand = new RelayCommand(RemoveNote, CanRemoveNote);
            DataContext = this; // Прив'язка даних
        }

        // Додати запис
        private void AddNote()
        {
            if (!string.IsNullOrEmpty(NewNote))
            {
                Notes.Add(NewNote);
                NewNote = string.Empty; // Очищення поля після додавання
                NoteTextBox.Clear(); // Очищення текстового поля
            }
        }

        // Перевірка, чи можна додати запис
        private bool CanAddNote()
        {
            return !string.IsNullOrEmpty(NewNote); // Дозволяється додавати лише якщо текст не порожній
        }

        // Видалити запис
        private void RemoveNote()
        {
            if (Notes.Count > 0 && NotesListBox.SelectedItem != null)
            {
                Notes.Remove(item: NotesListBox.SelectedItem.ToString());
            }
        }

        // Перевірка, чи можна видалити запис
        private bool CanRemoveNote()
        {
            return NotesListBox.SelectedItem != null; // Дозволяється видаляти лише якщо запис вибрано
        }

        // Обробник зміни тексту в текстовому полі
        private void NoteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NewNote = NoteTextBox.Text;
        }

        // Обробник вибору запису в списку 
        private void NotesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Логіка для обробки вибору 
        }
    }

    // Клас для команди 
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (() => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute();

        public void Execute(object parameter) => _execute();
    }
}
