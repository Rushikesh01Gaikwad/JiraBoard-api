namespace JiraBoard_api.Modals
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public ICollection<projectData>? ProjectData { get; set; } = new List<projectData>();

    }
}
