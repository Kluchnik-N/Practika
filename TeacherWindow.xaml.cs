using System;
using System.Linq;
using System.Windows;
using System.Data.Entity;
using WpfAppNeeeeeee.Models;

namespace WpfAppNeeeeeee
{
    public partial class TeacherWindow : Window
    {
        private readonly SchoolAppContext _context;
        private readonly User _currentTeacher;

        public TeacherWindow(User teacher)
        {
            InitializeComponent();
            _context = new SchoolAppContext();
            _currentTeacher = _context.Users.FirstOrDefault(u => u.ID == teacher.ID);
            LoadSchedule();
            LoadMessages();
            LoadReceivers();
        }

        private void LoadSchedule()
        {
            var schedule = _context.Schedules.Where(s => s.TeacherID == _currentTeacher.ID).ToList();
            ScheduleDataGrid.ItemsSource = schedule;
        }

        private void LoadMessages()
        {
            var messages = _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderID == _currentTeacher.ID || m.ReceiverID == _currentTeacher.ID)
                .OrderByDescending(m => m.Timestamp)
                .ToList();
            MessagesDataGrid.ItemsSource = messages;
        }

        private void LoadReceivers()
        {
            ReceiverComboBox.ItemsSource = _context.Users.Where(u => u.ID != _currentTeacher.ID).ToList();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReceiverComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                MessageBox.Show("Выберите получателя и введите текст сообщения.");
                return;
            }
            try
            {
                var receiver = ReceiverComboBox.SelectedItem as User;
                var message = new Message
                {
                    SenderID = _currentTeacher.ID,
                    ReceiverID = receiver.ID,
                    Content = MessageTextBox.Text.Trim(),
                    Timestamp = DateTime.Now
                };
                _context.Messages.Add(message);
                _context.SaveChanges();
                MessageTextBox.Text = string.Empty;
                LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 