using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using VP.BAL.Classes;
using VP.DAL.Model;

namespace VP.BAL
{
    public class AdminWindowLogic
    {
        VPDB db = new VPDB();
        public bool EditSettings(string Login, string Password)
        {
            if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password))
            {
                StaticData.Admin = db.AdminLogin.FirstOrDefault(item => item.id == StaticData.Admin.id);
                StaticData.Admin.login = Login;
                StaticData.Admin.password = Password;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<VPEmployee> GetEmpList()
        {
            List<VPEmployee> arr = new List<VPEmployee>();
            foreach (Employees item in db.Employees)
            {
                arr.Add(new VPEmployee()
                {
                    address = item.address,
                    workbegin = item.workbegin,
                    cityID = item.cityID,
                    cityName = db.Cities.FirstOrDefault(temp => temp.id == item.cityID).name,
                    countryID = item.countryID,
                    countryName = db.Countries.FirstOrDefault(temp => temp.id == item.countryID).name,
                    deptID = item.departmentID,
                    deptName = db.Departments.FirstOrDefault(temp => temp.id == item.departmentID).name,
                    email = item.email,
                    fullName = item.fullName,
                    id = item.id,
                    login = item.login,
                    password = item.password,
                    phoneNumber = item.phoneNumber,
                    vacationStatus = item.vacationStatus == 1 ? true : false
                });
            }
            return arr;
        }
        public List<VPDepartment> GetDeptList()
        {
            List<VPDepartment> arr = new List<VPDepartment>();
            foreach (Departments item in db.Departments)
                arr.Add(new VPDepartment() { id = item.id, name = item.name, description = item.description });
            return arr;
        }
        public List<VPCity> GetCityList()
        {
            List<VPCity> arr = new List<VPCity>();
            foreach (Cities item in db.Cities)
            {
                arr.Add(new VPCity()
                {
                    id = item.id,
                    name = item.name,
                    description = item.description,
                    countryID = item.countryID,
                    countryName = db.Countries.FirstOrDefault(temp => temp.id == item.countryID).name
                });
            }
            return arr;
        }
        public List<VPCountry> GetCountryList()
        {
            List<VPCountry> arr = new List<VPCountry>();
            foreach (Countries item in db.Countries)
                arr.Add(new VPCountry() { id = item.id, name = item.name, description = item.description });
            return arr;
        }
        public List<VPFWDocument> GetDocHireList()
        {
            List<VPFWDocument> arr = new List<VPFWDocument>();
            string currentPath = Environment.CurrentDirectory;
            currentPath = currentPath.Remove(currentPath.IndexOf(@"\VacationPlus"));
            currentPath += @"\VacationPlus\VacationPlus\Documents\Hire";
            DirectoryInfo df = new DirectoryInfo(currentPath);
            foreach (FileInfo item in df.GetFiles())
            {
                string DocName = item.Name.Remove(item.Name.IndexOf(item.Extension));
                string EmpName = DocName.Substring(DocName.IndexOf("_") + 1);
                arr.Add(new VPFWDocument() { DocName = DocName, EmpName = EmpName, FullPath = item.FullName });
            }
            return arr;
        }
        public List<VPFWDocument> GetDocFireList()
        {
            List<VPFWDocument> arr = new List<VPFWDocument>();
            string currentPath = Environment.CurrentDirectory;
            currentPath = currentPath.Remove(currentPath.IndexOf(@"\VacationPlus"));
            currentPath += @"\VacationPlus\VacationPlus\Documents\Fire";
            DirectoryInfo df = new DirectoryInfo(currentPath);
            foreach (FileInfo item in df.GetFiles())
            {
                string DocName = item.Name.Remove(item.Name.IndexOf(item.Extension));
                string EmpName = DocName.Substring(DocName.IndexOf("_") + 1);
                arr.Add(new VPFWDocument() { DocName = DocName, EmpName = EmpName, FullPath = item.FullName });
            }
            return arr;
        }
        public List<VPMessage> GetSentMessageList()
        {
            List<VPMessage> arr = new List<VPMessage>();
            foreach (Messages item in db.Messages)
            {
                if (item.fromID == 0)
                {
                    arr.Add(new VPMessage()
                    {
                        id = item.id,
                        fromID = item.fromID,
                        fromName = item.fromID == 0 ? "Администратор" : db.Employees.FirstOrDefault(temp => temp.id == item.fromID).fullName,
                        Message = item.message,
                        MessageTime = item.MessageTime.ToShortTimeString(),
                        Title = item.title,
                        toID = item.toID,
                        toName = item.toID == 0 ? "Администратор" : db.Employees.FirstOrDefault(temp => temp.id == item.toID).fullName,
                    });
                }
            }
            return arr;
        }
        public List<VPMessage> GetReceivedMessageList()
        {
            List<VPMessage> arr = new List<VPMessage>();
            foreach (Messages item in db.Messages)
            {
                if (item.toID == 0)
                {
                    arr.Add(new VPMessage()
                    {
                        id = item.id,
                        fromID = item.fromID,
                        fromName = item.fromID == 0 ? "Администратор" : db.Employees.FirstOrDefault(temp => temp.id == item.fromID).fullName,
                        Message = item.message,
                        MessageTime = item.MessageTime.ToShortTimeString(),
                        Title = item.title,
                        toID = item.toID,
                        toName = item.toID == 0 ? "Администратор" : db.Employees.FirstOrDefault(temp => temp.id == item.toID).fullName,
                    });
                }
            }
            return arr;
        }
        public List<VPRequest> GetClearRequestList()
        {
            List<VPRequest> arr = new List<VPRequest>();
            foreach (Requests item in db.Requests)
            {
                if (item.status == 0)
                    arr.Add(new VPRequest()
                    {
                        fromID = item.fromID,
                        fromName = db.Employees.FirstOrDefault(temp => temp.id == item.fromID).fullName,
                        message = item.message,
                        status = item.status,
                        statusName = "Не просмотрено",
                        type = item.type,
                        typeName = item.type == 0 ? "На увольнение" : "На отпуск",
                        id = item.id
                    });
            }
            return arr;
        }
        public List<VPRequest> GetFullRequestList()
        {
            List<VPRequest> arr = new List<VPRequest>();
            foreach (Requests item in db.Requests)
            {
                VPRequest tmp = new VPRequest()
                {
                    fromID = item.fromID,
                    fromName = "",
                    message = item.message,
                    status = item.status,
                    type = item.type,
                    typeName = item.type == 0 ? "На увольнение" : "На отпуск",
                    id = item.id
                };
                if (tmp.status == 0)
                    tmp.statusName = "Не просмотрено";
                else if (tmp.status == 1)
                    tmp.statusName = "Принято";
                else if (tmp.status == 2)
                    tmp.statusName = "Отклонено";
                arr.Add(tmp);
            }
            return arr;
        }

        public bool EditDepartment(int id, string Name, string Description)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description))
            {
                StaticData.Department = db.Departments.FirstOrDefault(item => item.id == id);
                StaticData.Department.name = Name;
                StaticData.Department.description = Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool CheckDepartment(string name)
        {
            StaticData.Department = db.Departments.FirstOrDefault(item => item.name == name);
            if (StaticData.Department == null)
                return true;
            return false;
        }
        public bool AddNewDepartment(string Name, string Description)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description))
            {
                db.Departments.Add(new Departments() { description = Description, name = Name });
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public void DeleteDept(int id)
        {
            StaticData.Department = db.Departments.FirstOrDefault(item => item.id == id);
            for (; ; )
            {
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.departmentID == StaticData.Department.id);
                if (StaticData.Employee == null)
                    break;
                DeleteEmp(StaticData.Employee.id, "Вы были уволены в следствии роспуска вашего отдела");
            }
            db.Departments.Remove(StaticData.Department);
            db.SaveChanges();
        }

        public bool EditCity(int id, string Name, string Description, string CountryName)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description))
            {
                StaticData.Country = db.Countries.FirstOrDefault(item => item.name == CountryName);
                if (StaticData.Country != null)
                {
                    StaticData.City = db.Cities.FirstOrDefault(item => item.id == id);
                    StaticData.City.name = Name;
                    StaticData.City.description = Description;
                    StaticData.City.countryID = StaticData.Country.id;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool CheckCity(string Name)
        {
            StaticData.City = db.Cities.FirstOrDefault(item => item.name == Name);
            if (StaticData.City == null)
                return true;
            return false;
        }
        public bool AddNewCity(string Name, string Description, string CountryName)
        {
            StaticData.Country = db.Countries.FirstOrDefault(item => item.name == CountryName);
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) && StaticData.Country != null)
            {
                db.Cities.Add(new Cities() { name = Name, description = Description, countryID = StaticData.Country.id });
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public void DeleteCity(int id)
        {
            StaticData.City = db.Cities.FirstOrDefault(item => item.id == id);
            for (; ; )
            {
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.cityID == StaticData.City.id);
                if (StaticData.Employee == null)
                    break;
                DeleteEmp(StaticData.Employee.id, "Вы были уволены в следствии пропажи вашего города или страны");
            }
            db.Cities.Remove(StaticData.City);
            db.SaveChanges();
        }

        public bool EditCountry(int id, string Name, string Description)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description))
            {
                StaticData.Country = db.Countries.FirstOrDefault(item => item.id == id);
                StaticData.Country.name = Name;
                StaticData.Country.description = Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool CheckCountry(string Name)
        {
            StaticData.Country = db.Countries.FirstOrDefault(item => item.name == Name);
            if (StaticData.Country == null)
                return true;
            return false;
        }
        public bool AddNewCountry(string Name, string Description)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description))
            {
                db.Countries.Add(new Countries() { name = Name, description = Description });
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public void DeleteCountry(int id)
        {
            StaticData.Country = db.Countries.FirstOrDefault(item => item.id == id);
            for (; ; )
            {
                StaticData.City = db.Cities.FirstOrDefault(item => item.countryID == StaticData.Country.id);
                if (StaticData.City == null)
                    break;
                DeleteCity(StaticData.City.id);
            }
            db.Countries.Remove(StaticData.Country);
            db.SaveChanges();
        }
        public int ReturnCountryIDByName(string Name) { return db.Countries.FirstOrDefault(item => item.name == Name).id; }

        public bool EditEmp(int id, string FullName, string DeptName, string Address, string CountryName, string CityName, string email, string phoneNumber, string Login, string Password, DateTime? beginTime)
        {
            StaticData.Department = db.Departments.FirstOrDefault(item => item.name == DeptName);
            StaticData.Country = db.Countries.FirstOrDefault(item => item.name == CountryName);
            StaticData.City = db.Cities.FirstOrDefault(item => item.name == CityName);
            if (!String.IsNullOrEmpty(FullName) && StaticData.Department != null && !String.IsNullOrEmpty(Address) && StaticData.Country != null && StaticData.City != null && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phoneNumber) && !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password) && beginTime != null)
            {
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.id == id);
                StaticData.Employee.login = Login;
                StaticData.Employee.password = Password;
                StaticData.Employee.phoneNumber = phoneNumber;
                StaticData.Employee.workbegin = beginTime.GetValueOrDefault();
                StaticData.Employee.address = Address;
                StaticData.Employee.cityID = StaticData.City.id;
                StaticData.Employee.countryID = StaticData.Country.id;
                StaticData.Employee.departmentID = StaticData.Department.id;
                StaticData.Employee.email = email;
                StaticData.Employee.fullName = FullName;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool CheckEmp(string Login)
        {
            StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login);
            if (StaticData.Employee == null)
                return true;
            return false;
        }
        public bool AddNewEmp(string FullName, string DeptName, string Address, string CountryName, string CityName, string email, string phoneNumber, string Login, string Password, DateTime? beginTime)
        {
            StaticData.Department = db.Departments.FirstOrDefault(item => item.name == DeptName);
            StaticData.Country = db.Countries.FirstOrDefault(item => item.name == CountryName);
            StaticData.City = db.Cities.FirstOrDefault(item => item.name == CityName);
            if (!String.IsNullOrEmpty(FullName) && StaticData.Department != null && !String.IsNullOrEmpty(Address) && StaticData.Country != null && StaticData.City != null && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phoneNumber) && !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password) && beginTime != null)
            {
                db.Employees.Add(new Employees()
                {
                    address = Address,
                    cityID = StaticData.City.id,
                    countryID = StaticData.Country.id,
                    departmentID = StaticData.Department.id,
                    email = email,
                    fullName = FullName,
                    login = Login,
                    password = Password,
                    phoneNumber = phoneNumber,
                    vacationStatus = 0,
                    workbegin = beginTime.GetValueOrDefault()
                });
                db.SaveChanges();
                db.Vacations.Add(new Vacations()
                {
                    idEmp = db.Employees.FirstOrDefault(item => item.login == Login).id,
                    status = 0,
                    daysCount = 0,
                    dateBegin = null,
                    dateEnd = null
                });
                db.SaveChanges();
                CreateHireDocument(GetEmpList().FirstOrDefault(item => item.login == Login));
                return true;
            }
            return false;
        }
        public void DeleteEmp(int id, string Message)
        {
            StaticData.Employee = db.Employees.FirstOrDefault(item => item.id == id);
            CreateFireDocument(GetEmpList().FirstOrDefault(item => item.id == id), Message);
            StaticData.Vacation = db.Vacations.FirstOrDefault(item => item.idEmp == id);
            for (; ; )
            {
                StaticData.Message = db.Messages.FirstOrDefault(item => item.toID == StaticData.Employee.id || item.fromID == StaticData.Employee.id);
                if (StaticData.Message == null)
                    break;
                db.Messages.Remove(StaticData.Message);
                db.SaveChanges();
            }
            db.FiredEmployees.Add(new FiredEmployees() { AdminMessage = Message, idEmp = id, login = StaticData.Employee.login, password = StaticData.Employee.password });
            db.Employees.Remove(StaticData.Employee);
            db.Vacations.Remove(StaticData.Vacation);
            db.SaveChanges();
        }
        public void CreateFireDocument(VPEmployee emp, string reason)
        {
            string currentPath = Environment.CurrentDirectory;
            currentPath = currentPath.Remove(currentPath.IndexOf(@"\VacationPlus"));
            FlowDocument fw = new FlowDocument();
            Paragraph mainpr = new Paragraph();
            mainpr.Inlines.Add(new Bold(new Run($"\tОб увольнении {emp.fullName}")));
            Paragraph textpr1 = new Paragraph();
            Paragraph textpr2 = new Paragraph();
            Paragraph textpr3 = new Paragraph();
            textpr1.Inlines.Add(new Run($"Данный сотрудник был уволен из отдела -> \"{emp.deptName}\","));
            textpr2.Inlines.Add(new Run($"по причине -> \"{ reason }.\""));
            textpr3.Inlines.Add(new Run($"Дата увольнения -> {DateTime.Now.ToShortDateString()}"));
            fw.Blocks.Add(mainpr);
            fw.Blocks.Add(textpr1);
            fw.Blocks.Add(textpr2);
            fw.Blocks.Add(textpr3);
            var content = new TextRange(fw.ContentStart, fw.ContentEnd);

            if (content.CanSave(DataFormats.Rtf))
                using (FileStream stream = new FileStream(currentPath + $@"\VacationPlus\VacationPlus\Documents\Fire\{emp.id}_{emp.fullName}.rtf", FileMode.CreateNew, FileAccess.Write))
                { content.Save(stream, DataFormats.Rtf); }

            if (File.Exists(currentPath + $@"\VacationPlus\VacationPlus\Documents\Hire\{emp.id}_{emp.fullName}.rtf"))
                File.Delete(currentPath + $@"\VacationPlus\VacationPlus\Documents\Hire\{emp.id}_{emp.fullName}.rtf");
        }
        public void CreateHireDocument(VPEmployee emp)
        {
            string currentPath = Environment.CurrentDirectory;
            currentPath = currentPath.Remove(currentPath.IndexOf(@"\VacationPlus"));
            currentPath += @"\VacationPlus\VacationPlus\Documents\Hire";
            FlowDocument fw = new FlowDocument();
            Paragraph mainpr = new Paragraph();
            mainpr.Inlines.Add(new Bold(new Run($"\tОб принятии на работу {emp.fullName}")));
            Paragraph textpr1 = new Paragraph();
            Paragraph textpr2 = new Paragraph();
            textpr1.Inlines.Add(new Run($"Данный сотрудник впредь работает в отделе -> \"{emp.deptName}\""));
            textpr2.Inlines.Add(new Run($"Дата принятия -> {DateTime.Now.ToShortDateString()}"));
            fw.Blocks.Add(mainpr);
            fw.Blocks.Add(textpr1);
            fw.Blocks.Add(textpr2);
            var content = new TextRange(fw.ContentStart, fw.ContentEnd);
            if (content.CanSave(DataFormats.Rtf))
                using (FileStream stream = new FileStream(currentPath + $@"\{emp.id}_{emp.fullName}.rtf", FileMode.CreateNew, FileAccess.Write))
                { content.Save(stream, DataFormats.Rtf); }
        }

        public bool ChecknSentLoginMessage(string Login, string Message, string Title)
        {
            if (!String.IsNullOrEmpty(Message) && !String.IsNullOrEmpty(Title))
            {
                StaticData.Admin = db.AdminLogin.FirstOrDefault(item => item.login == Login);
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login);
                if (StaticData.Admin != null)
                {
                    db.Messages.Add(new Messages() { fromID = 0, toID = 0, message = Message, MessageTime = DateTime.Now, title = Title });
                    db.SaveChanges();
                    return true;
                }
                else if (StaticData.Employee != null)
                {
                    db.Messages.Add(new Messages() { fromID = 0, toID = StaticData.Employee.id, message = Message, MessageTime = DateTime.Now, title = Title });
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public void DeleteMessage(int id)
        {
            StaticData.Message = db.Messages.FirstOrDefault(item => item.id == id);
            db.Messages.Remove(StaticData.Message);
            db.SaveChanges();
        }

        public void AcceptRequest(int id, int type, int idFrom)
        {
            if(type == 0)
            {
                DeleteEmp(idFrom, "Вы отправили запрос на увольнение и были уволены!");
                StaticData.Request = db.Requests.FirstOrDefault(item => item.id == id);
                StaticData.Request.status = 1;
                db.SaveChanges();
            }
            else if(type == 1)
            {
                StaticData.Vacation = db.Vacations.FirstOrDefault(item => item.idEmp == idFrom);
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.id == idFrom);
                StaticData.Employee.vacationStatus = 1;
                StaticData.Vacation.dateBegin = DateTime.Now;
                StaticData.Vacation.dateEnd = DateTime.Now.AddDays(StaticData.Vacation.daysCount);
                StaticData.Vacation.status = 1;
                StaticData.Request = db.Requests.FirstOrDefault(item => item.id == id);
                StaticData.Request.status = 2;
                db.SaveChanges();
            }
        }
        public void DeclineRequest(int id)
        {
            StaticData.Request = db.Requests.FirstOrDefault(item => item.id == id);
            StaticData.Request.status = 2;
            db.SaveChanges();
        }
        public void DeleteRequest(int id)
        {
            StaticData.Request = db.Requests.FirstOrDefault(item => item.id == id);
            db.Requests.Remove(StaticData.Request);
            db.SaveChanges();
        }

        public void DeclineVacation(int id)
        {
            StaticData.Employee = db.Employees.FirstOrDefault(item => item.id == id);
            StaticData.Vacation = db.Vacations.FirstOrDefault(item => item.idEmp == id);
            StaticData.Employee.vacationStatus = 0;
            StaticData.Vacation.dateBegin = null;
            StaticData.Vacation.dateEnd = null;
            db.Messages.Add(new Messages() { fromID = 0, message = "Вы были отозваны от отпуска", MessageTime = DateTime.Now, title = "С возвращением", toID = id });
            db.SaveChanges();
        }
    }
}