using System;
using System.Linq;
using System.Windows;
using WpfAppNeeeeeee.Models;

namespace WpfAppNeeeeeee
{
    public partial class ScheduleEditWindow : Window
    {
        public Schedule Schedule { get; private set; }
        private readonly SchoolAppContext _context;
        private bool isEditMode;

        public ScheduleEditWindow(Schedule schedule = null)
        {
            InitializeComponent();
            _context = new SchoolAppContext();
            TeacherComboBox.ItemsSource = _context.Users.Where(u => u.Role.ToLower() == "преподаватель").ToList();

            if (schedule != null)
            {
                isEditMode = true;
                Schedule = schedule;
                DescriptionTextBox.Text = schedule.Description;
                DatePicker.SelectedDate = schedule.StartDateTime.Date;
                TimeTextBox.Text = schedule.StartDateTime.ToString("HH:mm");
                LocationTextBox.Text = schedule.Location;
                TeacherComboBox.SelectedValue = schedule.TeacherID;
            }
            else
            {
                isEditMode = false;
                Schedule = new Schedule();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                DatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(TimeTextBox.Text) ||
                TeacherComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            if (!TimeSpan.TryParse(TimeTextBox.Text, out var time))
            {
                MessageBox.Show("Некорректный формат времени. Используйте ЧЧ:ММ.");
                return;
            }

            var date = DatePicker.SelectedDate.Value.Date + time;
            Schedule.Description = DescriptionTextBox.Text.Trim();
            Schedule.StartDateTime = date;
            Schedule.Location = LocationTextBox.Text.Trim();
            Schedule.TeacherID = (int)TeacherComboBox.SelectedValue;

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