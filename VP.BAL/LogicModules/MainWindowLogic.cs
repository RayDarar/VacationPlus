using System;
using System.Linq;
using VP.BAL.Classes;
using VP.DAL.Model;
namespace VP.BAL
{
    public class MainWindowLogic
    {
        VPDB db = new VPDB();
        int AdminOrEmpOrFired = 0;//Admin = 3, emp = 2, FiredEmp = 1
        public string Message;
        public bool CheckLogin(string Login)
        {
            if (!String.IsNullOrEmpty(Login))
            {
                StaticData.Admin = db.AdminLogin.FirstOrDefault(item => item.login == Login);
                if (StaticData.Admin == null)
                {
                    StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login);
                    if (StaticData.Employee == null)
                    {
                        StaticData.FiredEmployee = db.FiredEmployees.FirstOrDefault(item => item.login == Login);
                        if (StaticData.FiredEmployee != null)
                        {
                            AdminOrEmpOrFired = 1;
                            Message = StaticData.FiredEmployee.AdminMessage;
                            return true;
                        }
                    }
                    else if(StaticData.Employee.vacationStatus == 1)
                    {
                        AdminOrEmpOrFired = 0;
                        return true;
                    }
                    else 
                    {
                        AdminOrEmpOrFired = 2;
                        return true;
                    }
                }
                else
                {
                    AdminOrEmpOrFired = 3;
                    return true;
                }
            }
            return false;
        }
        public bool Authorization(string Login, string Password)
        {
            if (!String.IsNullOrEmpty(Password))
            {
                if (AdminOrEmpOrFired == 3)
                {
                    StaticData.Admin = db.AdminLogin.FirstOrDefault(item => item.login == Login && item.password == Password);
                    if (StaticData.Admin != null)
                        return true;
                }
                else if(AdminOrEmpOrFired == 2)
                {
                    StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login && item.password == Password);
                    if (StaticData.Employee != null)
                        return true;
                }
                else if(AdminOrEmpOrFired == 1)
                {
                    StaticData.FiredEmployee = db.FiredEmployees.FirstOrDefault(item => item.login == Login && item.password == Password);
                    if (StaticData.FiredEmployee != null)
                        return true;
                }
                else if(AdminOrEmpOrFired == 0)
                {
                    StaticData.Employee = db.Employees.FirstOrDefault(item => item.login == Login && item.password == Password);
                    if (StaticData.Employee != null)
                        return true;
                }
            }
            return false;
        }
        public int GetAdminOrEmpOrFiredEmp() { return AdminOrEmpOrFired; }
    }
}