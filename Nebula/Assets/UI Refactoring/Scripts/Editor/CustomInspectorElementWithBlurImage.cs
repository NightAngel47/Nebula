using UnityEditor;

[CustomEditor(typeof(ElementWithBlurImage))]
public class CustomInspectorElementWithBlurImage : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ElementWithBlurImage element = (ElementWithBlurImage)target;
        element.BlurTransform.localPosition = element.ThisRectTrasform.localPosition;
    }
}