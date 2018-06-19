using System;

namespace VP.BAL.Classes
{
    public class VPEmployee
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string deptName { get; set; }
        public string countryName { get; set; }
        public string cityName { get; set; }
        public int deptID { get; set; }
        public int cityID { get; set; }
        public int countryID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public bool vacationStatus { get; set; }
        public DateTime? workbegin { get; set; }
    }
}