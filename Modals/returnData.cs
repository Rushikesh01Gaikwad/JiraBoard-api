namespace JiraBoard_api.Modals
{
    public class returnData
    {
        public object data { get; set; }
        public string message { get; set; } 
        public int statusCd { get; set; } = 1;
        public string exception { get; set; }
    }
}
