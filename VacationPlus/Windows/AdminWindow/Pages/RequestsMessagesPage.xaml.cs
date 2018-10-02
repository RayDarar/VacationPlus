using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace VacationPlus.Windows.AdminWindow.Pages
{
    public partial class RequestsMessagesPage : Page
    {
        DispatcherTimer timer;
        VP.BAL.Classes.VPMessage temp;
        VP.BAL.Classes.VPRequest temp1;
        public RequestsMessagesPage()
        {
            InitializeComponent();
            SentMessages.ItemsSource = AdminWindow.logic.GetSentMessageList();
            RecievedMessages.ItemsSource = AdminWindow.logic.GetReceivedMessageList();
            RequestList.ItemsSource = AdminWindow.logic.GetClearRequestList();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SentMessages.ItemsSource = AdminWindow.logic.GetSentMessageList();
            RecievedMessages.ItemsSource = AdminWindow.logic.GetReceivedMessageList();
            RequestList.ItemsSource = AdminWindow.logic.GetClearRequestList();
        }

        private void SentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.ChecknSentLoginMessage(LoginTextBox.Text, SentMessageTextBox.Text, TitleTextBox.Text))
            {
                LoginTextBox.Text = "";
                SentMessageTextBox.Text = "";
                TitleTextBox.Text = "";
                AdminWindow.SetSettingLabel("Сообщение успешно отправлено!");
            }
            else
                AdminWindow.SetSettingLabel("Проверьте свои данные");
        }
        private void SentMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListView).SelectedItem != null)
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
                AdminWindow.logic.DeleteMessage(temp.id);
                FromLabel.Content = "";
                ToLabel.Content = "";
                MessageTextBox.Text = "";
                AdminWindow.SetSettingLabel("Сообщение успешно удалено!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите сообщение!");
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (temp1 != null)
            {
                AdminWindow.logic.AcceptRequest(temp1.id, temp1.type, temp1.fromID);
                rLabel.Content = "";
                rTextBox.Text = "";
                AdminWindow.SetSettingLabel("Запрос был принят!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите запрос!");
        }
        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            if (temp1 != null)
            {
                AdminWindow.logic.DeclineRequest(temp1.id);
                rLabel.Content = "";
                rTextBox.Text = "";
                AdminWindow.SetSettingLabel("Запрос был отклонен!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите запрос!");
        }
        private void RequestList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListView).SelectedItem != null)
            {
                temp1 = (sender as ListView).SelectedItem as VP.BAL.Classes.VPRequest;
                rLabel.Content = temp1.fromName;
                rTextBox.Text = temp1.message;
            }
        }
    }
}