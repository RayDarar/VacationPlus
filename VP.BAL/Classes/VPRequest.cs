namespace VP.BAL.Classes
{
    public class VPRequest
    {
        public int id { get; set; }
        public int fromID { get; set; }
        public string fromName { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string statusName { get; set; }
        public int type { get; set; }
        public string typeName { get; set; }
    }
}