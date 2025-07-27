namespace JiraBoard_api.Modals
{
    public class projectData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public int userId { get; set; }
        public User? user { get; set; }
    }
}
