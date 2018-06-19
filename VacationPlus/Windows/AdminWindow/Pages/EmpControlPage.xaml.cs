using System;
using System.Windows;
using System.Windows.Controls;
using VP.BAL.Classes;
namespace VacationPlus.Windows.AdminWindow.Pages
{
    /// <summary>
    /// Interaction logic for EmpControlPage.xaml
    /// </summary>
    public partial class EmpControlPage : Page
    {
        public EmpControlPage()
        {
            InitializeComponent();
            EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
            UpdateAllComboBoxes();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmpList.SelectedItem != null)
            {
                if (AdminWindow.logic.EditEmp((EmpList.SelectedItem as VPEmployee).id, FullNameTextBox.Text, (DeptComboBox.SelectedItem as ComboBoxItem).Content.ToString(), AddressTextBox.Text, (CountryComboBox.SelectedItem as ComboBoxItem).Content.ToString(), (CityComboBox.SelectedItem as ComboBoxItem).Content.ToString(), EmailTextBox.Text, PhoneNumberTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text, BeginWorkDatePicker.SelectedDate))
                {
                    EmpList.UnselectAll();
                    FullNameTextBox.Text = "";
                    DeptComboBox.SelectedIndex = 0;
                    AddressTextBox.Text = "";
                    CountryComboBox.SelectedIndex = 0;
                    CityComboBox.SelectedIndex = 0;
                    EmailTextBox.Text = "";
                    PhoneNumberTextBox.Text = "";
                    LoginTextBox.Text = "";
                    PasswordTextBox.Text = "";
                    BeginWorkDatePicker.SelectedDate = null;
                    AdminWindow.SetSettingLabel("Изменения успешно сохранены!");
                    EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
                }
                else
                    AdminWindow.SetSettingLabel("Проверьте правильность данных!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите сотрудника!");
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.CheckEmp(LoginTextBox.Text))
            {
                if (AdminWindow.logic.AddNewEmp(FullNameTextBox.Text, (DeptComboBox.SelectedItem as ComboBoxItem).Content.ToString(), AddressTextBox.Text, (CountryComboBox.SelectedItem as ComboBoxItem).Content.ToString(), (CityComboBox.SelectedItem as ComboBoxItem).Content.ToString(), EmailTextBox.Text, PhoneNumberTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text, BeginWorkDatePicker.SelectedDate))
                {
                    EmpList.UnselectAll();
                    FullNameTextBox.Text = "";
                    DeptComboBox.SelectedIndex = 0;
                    AddressTextBox.Text = "";
                    CountryComboBox.SelectedIndex = 0;
                    CityComboBox.SelectedIndex = 0;
                    EmailTextBox.Text = "";
                    PhoneNumberTextBox.Text = "";
                    LoginTextBox.Text = "";
                    PasswordTextBox.Text = "";
                    BeginWorkDatePicker.SelectedDate = null;
                    AdminWindow.SetSettingLabel("Сотрудник успешно добавлен!");
                    EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
                }
                else
                    AdminWindow.SetSettingLabel("Проверьте правильность данных!");
            }
            else
                AdminWindow.SetSettingLabel("Такой сотрудник уже существует!");
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmpList.SelectedItem != null)
            {
                if (!String.IsNullOrEmpty(LetterTextBox.Text))
                {
                    AdminWindow.logic.DeleteEmp((EmpList.SelectedItem as VPEmployee).id, LetterTextBox.Text);
                    LetterTextBox.Text = "";
                    EmpList.UnselectAll();
                    EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
                    AdminWindow.SetSettingLabel("Сотрудник был уволен и успешно об этом уведомлен!");
                }
                else
                    AdminWindow.SetSettingLabel("Напишите письмо сотруднику!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите сотрудника");
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedIndex != 0)
            {
                CityComboBox.Items.Clear();
                CityComboBox.Items.Add(new ComboBoxItem() { Content = "Ничего" });
                int tempId = AdminWindow.logic.ReturnCountryIDByName((CountryComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                foreach (VPCity item in AdminWindow.logic.GetCityList())
                    if (item.countryID == tempId)
                        CityComboBox.Items.Add(new ComboBoxItem() { Content = item.name });
                CityComboBox.SelectedIndex = 0;
            }
        }
        public void UpdateAllComboBoxes()
        {
            DeptComboBox.Items.Clear();
            DeptComboBox.Items.Add(new ComboBoxItem() { Content = "Ничего" });
            foreach (VPDepartment item in AdminWindow.logic.GetDeptList())
                DeptComboBox.Items.Add(new ComboBoxItem() { Content = item.name });
            DeptComboBox.SelectedIndex = 0;

            CountryComboBox.Items.Clear();
            CountryComboBox.Items.Add(new ComboBoxItem() { Content = "Ничего" });
            foreach (VPCountry item in AdminWindow.logic.GetCountryList())
                CountryComboBox.Items.Add(new ComboBoxItem() { Content = item.name });
            CountryComboBox.SelectedIndex = 0;
        }
        private void EmpList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VPEmployee temp = EmpList.SelectedItem as VPEmployee;
            if (temp != null)
            {
                for (int i = 0; i < DeptComboBox.Items.Count; i++)
                {
                    if ((DeptComboBox.Items[i] as ComboBoxItem).Content.ToString() == temp.deptName)
                    {
                        DeptComboBox.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < CountryComboBox.Items.Count; i++)
                {
                    if ((CountryComboBox.Items[i] as ComboBoxItem).Content.ToString() == temp.countryName)
                    {
                        CountryComboBox.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < CityComboBox.Items.Count; i++)
                {
                    if ((CityComboBox.Items[i] as ComboBoxItem).Content.ToString() == temp.cityName)
                    {
                        CityComboBox.SelectedIndex = i;
                        break;
                    }
                }
                if (temp.vacationStatus == false)
                    StateLabel.Content = "Не на отпуске";
                else
                    StateLabel.Content = "На отпуске";
            }
        }
        private void DeclineVacationButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmpList.SelectedItem != null)
            {
                VPEmployee temp = EmpList.SelectedItem as VPEmployee;
                if (temp.vacationStatus == true)
                {
                    AdminWindow.logic.DeclineVacation(temp.id);
                    AdminWindow.SetSettingLabel("Сотрудник отозван!");
                    EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
                    UpdateAllComboBoxes();
                }
                else
                    AdminWindow.SetSettingLabel("Данный сотрудник не на отпуске!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите сотрудника!");
        }
    }
}