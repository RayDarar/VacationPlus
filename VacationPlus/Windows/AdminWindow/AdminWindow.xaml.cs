using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VP.BAL;
using VP.BAL.Classes;

namespace VacationPlus.Windows.AdminWindow
{
    public partial class AdminWindow : Window
    {
        public static Label WLabel;
        public static Label SLabel;
        public static AdminWindowLogic logic = new AdminWindowLogic();

        public AdminWindow()
        {
            InitializeComponent();
            WLabel = WelcomeLabel;
            SLabel = SettingsLabel;
        }

        private void AccSettings_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Настройки аккаунта";
            MainFrame.Source = new Uri("Pages/AdminSettingsPage.xaml", UriKind.RelativeOrAbsolute);
        }
        private void AccExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из аккаунта?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow();
                StaticData.ResetAllData();
                mw.Show();
                this.Close();
            }
            else
                return;
        }
        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                this.Close();
            else
                return;
        }

        private void Lists_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Списки и контроль данных";
            MainFrame.Source = new Uri("Pages/ListsPage.xaml", UriKind.RelativeOrAbsolute);
        }
        private void EmplControl_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Контроль сотрудников";
            MainFrame.Source = new Uri("Pages/EmpControlPage.xaml", UriKind.RelativeOrAbsolute);
        }
        private void Documents_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Документы";
            MainFrame.Source = new Uri("Pages/DocumentsPage.xaml", UriKind.RelativeOrAbsolute);
        }
        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Сообщения/Запросы";
            MainFrame.Source = new Uri("Pages/RequestsMessagesPage.xaml", UriKind.RelativeOrAbsolute);
        }

        static public async void SetSettingLabel(string text)
        {
            System.Media.SystemSounds.Beep.Play();
            SLabel.Content = text;
            await Task.Delay(3000);
            SLabel.Content = "";
        }
    }
}