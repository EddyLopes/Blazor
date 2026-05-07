namespace ABCSharedLibrary.Models.Requests.Identity.Users;

public class ChangeUserStatusRequest
{
    public string UserId { get; set; }
    public bool Activation { get; set; }
}
