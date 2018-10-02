using VP.DAL.Model;
namespace VP.BAL.Classes
{
    public static class StaticData
    {
        public static AdminLogin Admin = null;
        public static Employees Employee = null;
        public static Departments Department = null;
        public static Cities City = null;
        public static Countries Country = null;
        public static FiredEmployees FiredEmployee = null;
        public static Vacations Vacation = null;
        public static Messages Message = null;
        public static Requests Request = null;

        public static void ResetAllData()
        {
            Admin = null;
            Employee = null;
            Department = null;
            City = null;
            Country = null;
            FiredEmployee = null;
            Vacation = null;
            Message = null;
            Request = null;
        }
    }
}