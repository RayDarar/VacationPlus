using System.Windows;
using System.Windows.Media;
using VP.BAL;

namespace VacationPlus
{
    public partial class MainWindow : Window
    {
        MainWindowLogic logic = new MainWindowLogic();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (logic.CheckLogin(rLoginBox.Text))
            {
                rLoginLabel.Foreground = new SolidColorBrush(Colors.White);
                if (logic.Authorization(rLoginBox.Text, rPasswordBox.Password))
                {
                    rPasswordLabel.Foreground = new SolidColorBrush(Colors.White);
                    if (logic.GetAdminOrEmpOrFiredEmp() == 3)
                    {
                        Windows.AdminWindow.AdminWindow adminWindow = new Windows.AdminWindow.AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else if (logic.GetAdminOrEmpOrFiredEmp() == 2)
                    {
                        Windows.EmployeeWindow.EmployeeWindow employeeWindow = new Windows.EmployeeWindow.EmployeeWindow();
                        employeeWindow.Show();
                        this.Close();
                    }
                    else if (logic.GetAdminOrEmpOrFiredEmp() == 1)
                        MessageBox.Show($"Вы были уволены!\nПисьмо от администратора:{logic.Message}");
                    else if (logic.GetAdminOrEmpOrFiredEmp() == 0)
                        MessageBox.Show($"Вы на отпуске!\nПриятного отдыха");
                }
                else
                    rPasswordLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
                rLoginLabel.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}