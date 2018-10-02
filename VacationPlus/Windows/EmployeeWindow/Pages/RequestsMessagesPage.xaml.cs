using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace VacationPlus.Windows.EmployeeWindow.Pages
{
    public partial class RequestsMessagesPage : Page
    {
        DispatcherTimer timer;
        VP.BAL.Classes.VPMessage temp;
        VP.BAL.Classes.VPRequest temp1;
        public RequestsMessagesPage()
        {
            InitializeComponent();
            SentMessages.ItemsSource = EmployeeWindow.logic.GetSentMessageList();
            RecievedMessages.ItemsSource = EmployeeWindow.logic.GetReceivedMessageList();
            RequestList.ItemsSource = EmployeeWindow.logic.GetClearRequestList();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SentMessages.ItemsSource = EmployeeWindow.logic.GetSentMessageList();
            RecievedMessages.ItemsSource = EmployeeWindow.logic.GetReceivedMessageList();
            RequestList.ItemsSource = EmployeeWindow.logic.GetClearRequestList();
        }

        private void SentButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeWindow.logic.ChecknSentLoginMessage(LoginTextBox.Text, SentMessageTextBox.Text, TitleTextBox.Text))
            {
                LoginTextBox.Text = "";
                SentMessageTextBox.Text = "";
                TitleTextBox.Text = "";
                EmployeeWindow.SetSettingLabel("Сообщение успешно отправлено!");
            }
            else
                EmployeeWindow.SetSettingLabel("Проверьте свои данные");
        }
        private void SentMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                temp = (sender as ListView).SelectedItem as VP.BAL.Classes.VPMessage;
                FromLabel.Content = temp.fromName;
                ToLabel.Content = temp.toName;
                MessageTextBox.Text = temp.Message;
            }
        }
        private void DeleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (temp != null)
            {
                EmployeeWindow.logic.DeleteMessage(temp.id);
                FromLabel.Content = "";
                ToLabel.Content = "";
                MessageTextBox.Text = "";
                EmployeeWindow.SetSettingLabel("Сообщение успешно удалено!");
            }
            else
                EmployeeWindow.SetSettingLabel("Выберите сообщение!");
        }

        private void RequestList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                temp1 = (sender as ListView).SelectedItem as VP.BAL.Classes.VPRequest;
                rTextBox.Text = temp1.message;
                if (temp1.type == 0)
                    LeaveRadioButton.IsChecked = true;
                else if (temp1.type == 1)
                    VacationRadioButton.IsChecked = true;
            }
        }
        private void SentRequest_Click(object sender, RoutedEventArgs e)
        {
            if (LeaveRadioButton.IsChecked != null && VacationRadioButton.IsChecked != null)
            {
                int type = -1;
                if (LeaveRadioButton.IsChecked == true)
                    type = 0;
                else if (VacationRadioButton.IsChecked == true)
                {
                    MessageBox.Show($"Внимание! Кол-во дней отпуска: {EmployeeWindow.logic.dayscount}");
                    type = 1;
                }
                if (EmployeeWindow.logic.SentRequest(type, rTextBox.Text))
                {
                    LeaveRadioButton.IsChecked = false;
                    VacationRadioButton.IsChecked = false;
                    rTextBox.Text = "";
                    EmployeeWindow.SetSettingLabel("Запрос был отправлен!");
                }
                else
                    EmployeeWindow.SetSettingLabel("Проверьте данные!");
            }
            else
                EmployeeWindow.SetSettingLabel("Выберите тип!");
        }
        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (temp1 != null)
            {
                EmployeeWindow.logic.DeleteRequest(temp1.id);
                EmployeeWindow.SetSettingLabel("Запрос был удален!");
            }
            else
                EmployeeWindow.SetSettingLabel("Выберите запрос!");
        }
    }
}