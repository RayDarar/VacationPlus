using System.Windows;
using System.Windows.Controls;

namespace VacationPlus.Windows.EmployeeWindow.Pages
{
    /// <summary>
    /// Interaction logic for UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : Page
    {
        public UserSettingsPage()
        {
            InitializeComponent();
            MainGrid.DataContext = EmployeeWindow.logic.GetUserData();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeWindow.logic.EditEmp(EmployeeWindow.logic.GetUserData().id, FullNameTextBox.Text, AddressTextBox.Text, EmailTextBox.Text, PhoneNumberTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text))
                EmployeeWindow.SetSettingLabel("Изменения успешно сохранены!");
            else
                EmployeeWindow.SetSettingLabel("Проверьте правильность данных!");
        }
    }
}