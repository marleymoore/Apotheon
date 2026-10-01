using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilledDialImage : Image
{
public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
{
    // Replicate the wedge math to test it independently
    bool wedgeValid = true;
    if (fillMethod == FillMethod.Radial360 && fillAmount < 1f)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, screenPoint, eventCamera, out localPoint))
        {
            localPoint -= rectTransform.rect.center;
            float angle = Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;
            if (angle < 0f) angle += 360f;

            float baseDegree = 0f;
            switch ((Origin360)fillOrigin)
            {
                case Origin360.Bottom: baseDegree = 270f; break;
                case Origin360.Right:  baseDegree = 0f;   break;
                case Origin360.Top:    baseDegree = 90f;  break;
                case Origin360.Left:   baseDegree = 180f; break;
            }

            float delta;
            if (fillClockwise)
                delta = (baseDegree - angle + 360f) % 360f;
            else
                delta = (angle - baseDegree + 360f) % 360f;

            return delta <= fillAmount * 360f + 0.5f;
                // Debug.Log($"dial wedge test: angle={angle:F1} delta={delta:F1} fillMax={fillAmount * 360f:F1} wedgeValid={wedgeValid}");
            }
    }

    
    bool baseValid = base.IsRaycastLocationValid(screenPoint, eventCamera);
    Debug.Log($"dial base test: {baseValid} | wedgeValid: {wedgeValid}");
    return baseValid && wedgeValid;
}
}