namespace HAWK_v.helpers
{
    /// <summary>
    /// This class manages the communication between the HAWK backend the the smartface backend.
    /// </summary>
    public class AuthenticateResponse
    {
        public int id { get; set; }
        public string username { get; set; }
        public string token { get; set; }
    }
}
