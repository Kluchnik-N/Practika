using System.Windows;
using WpfAppNeeeeeee.Models;
using System.Windows.Controls;

namespace WpfAppNeeeeeee
{
    public partial class UserEditWindow : Window
    {
        public User User { get; private set; }
        private bool isEditMode;

        public UserEditWindow(User user = null)
        {
            InitializeComponent();
            if (user != null)
            {
                isEditMode = true;
                User = user;
                UsernameTextBox.Text = user.Username;
                PasswordBox.Password = user.Password;
                RoleComboBox.Text = user.Role;
                NameTextBox.Text = user.Name;
                ContactInfoTextBox.Text = user.ContactInfo;
                SpecializationTextBox.Text = user.Specialization;
                UsernameTextBox.IsEnabled = false; // Username нельзя менять
            }
            else
            {
                isEditMode = false;
                User = new User();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                RoleComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            User.Username = UsernameTextBox.Text.Trim();
            User.Password = PasswordBox.Password;
            User.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            User.Name = NameTextBox.Text.Trim();
            User.ContactInfo = ContactInfoTextBox.Text.Trim();
            User.Specialization = SpecializationTextBox.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 