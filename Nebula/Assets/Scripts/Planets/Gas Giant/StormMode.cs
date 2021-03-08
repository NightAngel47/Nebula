public class StormMode
{
    public enum InteractMode { None, LargeStorm, SmallStorm, Erase }

    public InteractMode CurrentInteractMode = InteractMode.None;

    public void ChangeStormMode(InteractMode mode)
    {
        CurrentInteractMode = mode;
    }
}