using System;
using System.Collections.Generic;
using System.Linq;
using VP.BAL.Classes;
using VP.DAL.Model;

namespace VP.BAL.LogicModules
{
    public class EmployeeWindowLogic
    {
        VPEmployee currentUser;
        VPDB db = new VPDB();
        public int dayscount = 0;
        public EmployeeWindowLogic()
        {
            currentUser = new VPEmployee()
            {
                address = StaticData.Employee.address,
                workbegin = StaticData.Employee.workbegin,
                cityID = StaticData.Employee.cityID,
                cityName = db.Cities.FirstOrDefault(temp => temp.id == StaticData.Employee.cityID).name,
                countryID = StaticData.Employee.countryID,
                countryName = db.Countries.FirstOrDefault(temp => temp.id == StaticData.Employee.countryID).name,
                deptID = StaticData.Employee.departmentID,
                deptName = db.Departments.FirstOrDefault(temp => temp.id == StaticData.Employee.departmentID).name,
                email = StaticData.Employee.email,
                fullName = StaticData.Employee.fullName,
                id = StaticData.Employee.id,
                login = StaticData.Employee.login,
                password = StaticData.Employee.password,
                phoneNumber = StaticData.Employee.phoneNumber,
                vacationStatus = StaticData.Employee.vacationStatus == 1 ? true : false
            };
            StaticData.Vacation = db.Vacations.FirstOrDefault(item => item.idEmp == currentUser.id);
            StaticData.Vacation.daysCount = (DateTime.Now - currentUser.workbegin.GetValueOrDefault()).Days;
            StaticData.Vacation.daysCount /= 30;//Кол-во месяцев
            StaticData.Vacation.daysCount = StaticData.Vacation.daysCount * 2;
            dayscount = StaticData.Vacation.daysCount;
            db.SaveChanges();
        }
        public VPEmployee GetUserData() { return currentUser; }
        public void UpdateVacationData()
        {
            if(currentUser != null)
                currentUser.vacationStatus = db.Employees.FirstOrDefault(item => item.id == currentUser.id).vacationStatus == 1 ? true : false;
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
        public List<VPMessage> GetSentMessageList()
        {
            List<VPMessage> arr = new List<VPMessage>();
            foreach (Messages item in db.Messages)
            {
                if (item.fromID == currentUser.id)
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
                if (item.toID == currentUser.id)
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
                if (item.fromID == currentUser.id)
                {
                    VPRequest tmp = new VPRequest()
                    {
                        fromID = item.fromID,
                        fromName = db.Employees.FirstOrDefault(temp => temp.id == item.fromID).fullName,
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
            }
            return arr;
        }

        public bool ChecknSentLoginMessage(string Login, string Message, string Title)
        {
            if (!String.IsNullOrEmpty(Message) && !String.IsNullOrEmpty(Title))
            {
                StaticData.Admin = db.AdminLogin.FirstOrDefault(item => item.login == Login);
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login);
                if (StaticData.Admin != null)
                {
                    db.Messages.Add(new Messages() { fromID = currentUser.id, toID = 0, message = Message, MessageTime = DateTime.Now, title = Title });
                    db.SaveChanges();
                    return true;
                }
                else if (StaticData.Employee != null)
                {
                    db.Messages.Add(new Messages() { fromID = currentUser.id, toID = StaticData.Employee.id, message = Message, MessageTime = DateTime.Now, title = Title });
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

        public bool SentRequest(int type, string Message)
        {
            if (type >= 0 && type <= 1 && !String.IsNullOrEmpty(Message))
            {
                if (type == 1)
                {
                    StaticData.Vacation = db.Vacations.FirstOrDefault(item => item.idEmp == currentUser.id);
                    if((DateTime.Now - currentUser.workbegin.GetValueOrDefault()).Days > 60 && StaticData.Vacation.daysCount != 0)
                    {
                        db.Requests.Add(new Requests() { fromID = currentUser.id, message = Message, status = 0, type = type });
                        db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    db.Requests.Add(new Requests() { fromID = currentUser.id, message = Message, status = 0, type = type });
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public void DeleteRequest(int id)
        {
            StaticData.Request = db.Requests.FirstOrDefault(item => item.id == id);
            db.Requests.Remove(StaticData.Request);
            db.SaveChanges();
        }
        public bool EditEmp(int id, string FullName, string Address, string email, string phoneNumber, string Login, string Password)
        {
            if (!String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(Address) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phoneNumber) && !String.IsNullOrEmpty(Login))
            {
                StaticData.Employee = db.Employees.FirstOrDefault(item => item.id == id);
                StaticData.Employee.login = Login;
                StaticData.Employee.password = Password;
                StaticData.Employee.phoneNumber = phoneNumber;
                StaticData.Employee.address = Address;
                StaticData.Employee.email = email;
                StaticData.Employee.fullName = FullName;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public int ReturnCountryIDByName(string Name) { return db.Countries.FirstOrDefault(item => item.name == Name).id; }
    }
}