using System;
using System.Linq;
using System.Windows;
using WpfAppNeeeeeee.Models;
using WpfAppNeeeeeee;

namespace WpfAppNeeeeeee
{
    public partial class LoginWindow : Window
    {
        private readonly SchoolAppContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new SchoolAppContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Введите имя пользователя и пароль.");
                return;
                Code error1
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null || user.Password != password)
                {
                    ShowError("Неверное имя пользователя или пароль.");
                    return;
                }

                OpenMainWindow(user.Role);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void OpenMainWindow(string role)
        {
            Window mainWindow;
            switch (role.ToLower())
            {
                case "admin":
                    mainWindow = new AdminWindow();
                    break;
                case "teacher":
                    mainWindow = new TeacherWindow(_context.Users.FirstOrDefault(u => u.Username == UsernameTextBox.Text));
                    break;
                default:
                    throw new Exception($"Неизвестная роль: {role}");
            }
            mainWindow.Show();
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
} 