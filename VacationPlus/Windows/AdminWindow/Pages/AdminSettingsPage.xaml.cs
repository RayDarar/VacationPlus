using System.Windows;
using System.Windows.Controls;

namespace VacationPlus.Windows.AdminWindow.Pages
{
    public partial class AdminSettingsPage : Page
    {
        public AdminSettingsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.EditSettings(rLoginBox.Text, rPasswordBox.Text))
            {
                AdminWindow.SetSettingLabel("Изменения успешно сохранены!");
                rLoginBox.Text = "";
                rPasswordBox.Text = "";
            }
            else
                AdminWindow.SetSettingLabel("Оба поля не должны быть пустыми!");
        }
    }
}