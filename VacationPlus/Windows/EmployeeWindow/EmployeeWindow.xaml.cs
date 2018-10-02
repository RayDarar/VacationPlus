using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using VP.BAL.Classes;
using VP.BAL.LogicModules;

namespace VacationPlus.Windows.EmployeeWindow
{
    public partial class EmployeeWindow : Window
    {
        DispatcherTimer timer;
        public static Label WLabel;
        public static Label SLabel;
        public static EmployeeWindowLogic logic = new EmployeeWindowLogic();
        public EmployeeWindow()
        {
            InitializeComponent();
            WLabel = WelcomeLabel;
            SLabel = SettingsLabel;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            logic.UpdateVacationData();
            if (logic.GetUserData().vacationStatus == true)
            {
                MainWindow mw = new MainWindow();
                StaticData.ResetAllData();
                mw.Show();
                Close();
            }
        }

        private void AccSettings_Click(object sender, RoutedEventArgs e)
        {
            WelcomeLabel.Content = "Настройки аккаунта";
            MainFrame.Source = new Uri("Pages/UserSettingsPage.xaml", UriKind.RelativeOrAbsolute);
        }
        private void AccExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из аккаунта?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow();
                StaticData.ResetAllData();
                mw.Show();
                Close();
                timer.Stop();
            }
            else
                return;
        }
        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Close();
            else
                return;
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