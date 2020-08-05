public class GasMode
{
    public enum InteractMode
    {
        None,
        Painting,
        Storms,
        Rings
    }

    public InteractMode CurrentInteractMode { get; private set; } = InteractMode.Painting;

    public void ChangeInteractMode(InteractMode mode)
    {
        CurrentInteractMode = mode;
        //Debug.Log(mode); // add using UnityEngine if uncommented.
    }
}
