using System.Windows;
using System.Windows.Controls;

namespace VacationPlus.Windows.AdminWindow.Pages
{
    public partial class ListsPage : Page
    {
        public ListsPage()
        {
            InitializeComponent();
            UpdateData();
            SetCityComboBox();
        }
        private void ChangeDeptButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeptList.SelectedItem != null)
            {
                if (AdminWindow.logic.EditDepartment((DeptList.SelectedItem as VP.BAL.Classes.VPDepartment).id, rDeptNameBox.Text, rDeptDescriptionBox.Text))
                {
                    rDeptNameBox.Text = "";
                    rDeptDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Отдел был успешно изменен!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Все поля должны быть заполнены!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите отдел!");
        }
        private void AddNewDeptButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.CheckDepartment(rDeptNameBox.Text))
            {
                if (AdminWindow.logic.AddNewDepartment(rDeptNameBox.Text, rDeptDescriptionBox.Text))
                {
                    rDeptNameBox.Text = "";
                    rDeptDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Отдел успешно добавлен!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Все поля должны быть заполнены!");
            }
            else
                AdminWindow.SetSettingLabel("Такой отдел уже существует!");
        }
        private void DeleteDeptButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeptList.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранный отдел?\nВнимание! Удалив данный отдел вы автоматически уволите всех сотрудников связанных с этим отделом", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AdminWindow.logic.DeleteDept((DeptList.SelectedItem as VP.BAL.Classes.VPDepartment).id);
                    rDeptNameBox.Text = "";
                    rDeptDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Отдел был успешно удален!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Отмена удаления!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите отдел!");
        }

        private void ChangeCityButton_Click(object sender, RoutedEventArgs e)
        {
            if (CityList.SelectedItem != null)
            {
                if (AdminWindow.logic.EditCity((CityList.SelectedItem as VP.BAL.Classes.VPCity).id, rCityNameBox.Text, rCityDescriptionBox.Text, (CountryForCityComboBox.SelectedItem as ComboBoxItem).Content.ToString()))
                {
                    rCityNameBox.Text = "";
                    rCityDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Город был успешно изменен!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Проверьте правильность вводимых данных!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите город!");
        }
        private void AddNewCityButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.CheckCity(rCityNameBox.Text))
            {
                if (AdminWindow.logic.AddNewCity(rCityNameBox.Text, rCityDescriptionBox.Text, (CountryForCityComboBox.SelectedItem as ComboBoxItem).Content.ToString()))
                {
                    rCityNameBox.Text = "";
                    rCityDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Город успешно добавлен!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Проверьте свои данные!");
            }
            else
                AdminWindow.SetSettingLabel("Такой город уже существует!");
        }
        private void DeleteCityButton_Click(object sender, RoutedEventArgs e)
        {
            if (CityList.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранный город?\nВнимание! Удалив выбранный город, вы автоматически уволите всех работников связанных с ним", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AdminWindow.logic.DeleteCity((CityList.SelectedItem as VP.BAL.Classes.VPCity).id);
                    rDeptNameBox.Text = "";
                    rDeptDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Город был успешно удален!");
                    UpdateData();
                }
                else
                    AdminWindow.SetSettingLabel("Отмена удаления!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите город!");
        }

        private void ChangeCountryButton_Click(object sender, RoutedEventArgs e)
        {
            if(CountryList.SelectedItem != null)
            {
                if (AdminWindow.logic.EditCountry((CountryList.SelectedItem as VP.BAL.Classes.VPCountry).id, rCountryNameBox.Text, rCountryDescriptionBox.Text))
                {
                    rCountryNameBox.Text = "";
                    rCountryDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Страна была успешно изменена!");
                    UpdateData();
                    SetCityComboBox();
                }
                else
                    AdminWindow.SetSettingLabel("Все поля должны быть заполнены!");
            }
        }
        private void AddNewCountryButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWindow.logic.CheckCountry(rCountryNameBox.Text))
            {
                if (AdminWindow.logic.AddNewCountry(rCountryNameBox.Text, rCountryDescriptionBox.Text))
                {
                    rCountryNameBox.Text = "";
                    rCountryDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Страна успешно добавлена!");
                    UpdateData();
                    SetCityComboBox();
                }
                else
                    AdminWindow.SetSettingLabel("Все поля должны быть заполнены!");
            }
            else
                AdminWindow.SetSettingLabel("Такая страна уже существует!");
        }
        private void DeleteCountryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountryList.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранную страну?\nВнимание! Если удалить страну, то все города и работники связанные с ней будут так же удалены/уволены", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AdminWindow.logic.DeleteCountry((CountryList.SelectedItem as VP.BAL.Classes.VPCountry).id);
                    rCountryNameBox.Text = "";
                    rCountryDescriptionBox.Text = "";
                    AdminWindow.SetSettingLabel("Страна и все связанные города и работники были удалены/уволены!");
                    UpdateData();
                    SetCityComboBox();
                }
                else
                    AdminWindow.SetSettingLabel("Отмена удаления!");
            }
            else
                AdminWindow.SetSettingLabel("Выберите страну!");
        }

        private void SetCityComboBox()
        {
            CountryForCityComboBox.Items.Clear();
            foreach (VP.BAL.Classes.VPCountry item in AdminWindow.logic.GetCountryList())
                CountryForCityComboBox.Items.Add(new ComboBoxItem() { Content = item.name });
            CountryForCityComboBox.SelectedIndex = 0;
        }
        private void UpdateData()
        {
            ReqList.ItemsSource = AdminWindow.logic.GetFullRequestList();
            EmpList.ItemsSource = AdminWindow.logic.GetEmpList();
            DeptList.ItemsSource = AdminWindow.logic.GetDeptList();
            CityList.ItemsSource = AdminWindow.logic.GetCityList();
            CountryList.ItemsSource = AdminWindow.logic.GetCountryList();
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (ReqList.SelectedItem != null)
            {
                VP.BAL.Classes.VPRequest temp = ReqList.SelectedItem as VP.BAL.Classes.VPRequest;
                AdminWindow.logic.DeleteRequest(temp.id);
                AdminWindow.SetSettingLabel("Запрос был успешно удален!");
                UpdateData();
            }
            else
                AdminWindow.SetSettingLabel("Выберите запрос!");
        }
    }
}