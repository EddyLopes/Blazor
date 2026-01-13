namespace MyBlazorApp.States;

public class StateContainer
{
    private int _counter;
    public int Counter
    {
        get => _counter;
        set
        {
            _counter = value;
            NotificationHasChanged();
        }
    }

    public Action? Notification { get; set; }

    private void NotificationHasChanged()
    {
        Notification?.Invoke();
    }
}
