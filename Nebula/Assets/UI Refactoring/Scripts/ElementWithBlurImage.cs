using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ElementWithBlurImage : MonoBehaviour
{
    private RectTransform thisRectTransform = null;
    public RectTransform ThisRectTrasform
    {
        get
        {
            if (thisRectTransform == null)
                thisRectTransform = GetComponent<RectTransform>();

            return thisRectTransform;
        }
    }

    [SerializeField] private RectTransform blurTransform = null;
    public RectTransform BlurTransform => blurTransform;

    /// <summary> Sets the values of the blurTransform equal to the values of this RectTransform. </summary>
    public void CopyTransformToBlurTransform()
    {
        if (blurTransform != null)
        {
            blurTransform.position = ThisRectTrasform.position;
            blurTransform.sizeDelta = ThisRectTrasform.sizeDelta;

            blurTransform.anchoredPosition = ThisRectTrasform.anchoredPosition;
            blurTransform.pivot = ThisRectTrasform.pivot;

            blurTransform.rotation = ThisRectTrasform.rotation;
            blurTransform.localScale = ThisRectTrasform.localScale;
        }
    }
}