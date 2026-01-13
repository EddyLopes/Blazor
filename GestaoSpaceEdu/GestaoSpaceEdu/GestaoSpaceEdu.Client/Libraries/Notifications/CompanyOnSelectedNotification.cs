namespace GestaoSpaceEdu.Client.Libraries.Notifications;

public class CompanyOnSelectedNotification
{
    public Action? OnCompanySelected { get; set; }

    public void OnSelectedNotification()
    {
        OnCompanySelected?.Invoke();
    }
}
