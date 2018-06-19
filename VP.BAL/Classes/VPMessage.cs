namespace VP.BAL.Classes
{
    public class VPMessage
    {
        public int id { get; set; }
        public int fromID { get; set; }
        public string fromName { get; set; }
        public int toID { get; set; }
        public string toName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string MessageTime { get; set; }
    }
}