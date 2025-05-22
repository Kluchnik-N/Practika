using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfAppNeeeeeee.Models;
using System.Windows.Controls;
using System.Data.Entity;
using System;

namespace WpfAppNeeeeeee
{
    public partial class AdminWindow : Window
    {
        private readonly SchoolAppContext _context;

        public AdminWindow()
        {
            InitializeComponent();
            _context = new SchoolAppContext();
            LoadUsers();
            LoadSchedules();
            LoadMessages();
            LoadReceivers();
        }

        private void LoadUsers()
        {
            var users = _context.Users.ToList();
            UsersDataGrid.ItemsSource = users;
        }

        private void LoadSchedules()
        {
            var schedules = _context.Schedules.Include(s => s.Teacher).ToList();
            ScheduleDataGrid.ItemsSource = schedules;
        }

        private void LoadMessages()
        {
            var messages = _context.Messages.Include(m => m.Sender).Include(m => m.Receiver).OrderByDescending(m => m.Timestamp).ToList();
            MessagesDataGrid.ItemsSource = messages;
        }

        private void LoadReceivers()
        {
            ReceiverComboBox.ItemsSource = _context.Users.ToList();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new UserEditWindow();
            if (userWindow.ShowDialog() == true)
            {
                _context.Users.Add(userWindow.User);
                _context.SaveChanges();
                LoadUsers();
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования.");
                return;
            }
            var userWindow = new UserEditWindow(selectedUser);
            if (userWindow.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadUsers();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }
            if (MessageBox.Show($"Удалить пользователя {selectedUser.Username}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Users.Remove(selectedUser);
                    _context.SaveChanges();
                    LoadUsers();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Невозможно удалить пользователя, так как с ним связаны другие данные (например, расписание или сообщения).\nПодробности: " + ex.InnerException?.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var scheduleWindow = new ScheduleEditWindow();
            if (scheduleWindow.ShowDialog() == true)
            {
                _context.Schedules.Add(scheduleWindow.Schedule);
                _context.SaveChanges();
                LoadSchedules();
            }
        }

        private void EditScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchedule = ScheduleDataGrid.SelectedItem as Schedule;
            if (selectedSchedule == null)
            {
                MessageBox.Show("Выберите событие для редактирования.");
                return;
            }
            var scheduleWindow = new ScheduleEditWindow(selectedSchedule);
            if (scheduleWindow.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadSchedules();
            }
        }

        private void DeleteScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchedule = ScheduleDataGrid.SelectedItem as Schedule;
            if (selectedSchedule == null)
            {
                MessageBox.Show("Выберите событие для удаления.");
                return;
            }
            if (MessageBox.Show($"Удалить событие '{selectedSchedule.Description}'?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Schedules.Remove(selectedSchedule);
                    _context.SaveChanges();
                    LoadSchedules();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении события: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                var adminSender = _context.Users.FirstOrDefault(u => u.Role.ToLower() == "администратор");
                if (adminSender == null)
                {
                    MessageBox.Show("Не найден отправитель-администратор.");
                    return;
                }
                var message = new Message
                {
                    SenderID = adminSender.ID,
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