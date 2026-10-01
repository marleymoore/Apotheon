using UnityEngine;
using UnityEngine.UI;

public class HitRadial
{

  // void Update()
  // {
  //     if (Input.GetKeyDown(KeyCode.Space) && ImagesOverlap())
  //         radialButton.onClick.Invoke();
  // }

    public static bool ImagesOverlap(FilledDialImage radialImage, Image tickImage, float sampleStepPx = 4f)
    {
        Camera cam = GetCanvasCamera(radialImage);
        Debug.Log($"ImagesOverlap START: dial={radialImage != null} tick={tickImage != null} cam={(cam != null ? cam.name : "null")}");


        Rect a = ScreenRect(radialImage.rectTransform, cam);
        Rect b = ScreenRect(tickImage.rectTransform, cam);
        Debug.Log($"A={a} B={b}");

        // Intersection of the two screen rects
        Rect overlap = Rect.MinMaxRect(
            Mathf.Max(a.xMin, b.xMin), Mathf.Max(a.yMin, b.yMin),
            Mathf.Min(a.xMax, b.xMax), Mathf.Min(a.yMax, b.yMax));
        Debug.Log($"overlap={overlap}");
        
        if (overlap.width <= 0f || overlap.height <= 0f) return false;

        

        // Sample pixels inside the overlap; a hit on both = overlap
        for (float x = overlap.xMin; x <= overlap.xMax; x += sampleStepPx)
        {
           

            for (float y = overlap.yMin; y <= overlap.yMax; y += sampleStepPx)
            {

                Vector2 pixel = new Vector2(x, y);
                if (radialImage.IsRaycastLocationValid(pixel, cam) &&
                    tickImage.IsRaycastLocationValid(pixel, cam))
                    return true;

                if (radialImage.IsRaycastLocationValid(pixel, cam) != tickImage.IsRaycastLocationValid(pixel, cam))
                    Debug.Log($"p={pixel} dial={radialImage.IsRaycastLocationValid(pixel, cam)} tick={tickImage.IsRaycastLocationValid(pixel, cam)}");
            }
        }
        return false;
    }

    static Camera GetCanvasCamera(FilledDialImage radialImage)
    {
        Canvas canvas = radialImage.canvas;
        return (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : (canvas ? canvas.worldCamera : null);
    }

    static Rect ScreenRect(RectTransform rectTrans, Camera cam)
    {
        Vector3[] corners = new Vector3[4];
        rectTrans.GetWorldCorners(corners);

        Vector2 min = Vector2.positiveInfinity;
        Vector2 max = Vector2.negativeInfinity;
        for (int i = 0; i < 4; i++)
        {
            Vector2 size = RectTransformUtility.WorldToScreenPoint(cam, corners[i]);
            min = Vector2.Min(min, size);
            max = Vector2.Max(max, size);
        }
        return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
    }
}