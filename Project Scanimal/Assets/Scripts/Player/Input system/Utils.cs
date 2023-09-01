using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera cam, Vector3 position)
    {
        if (position.x < Screen.width)
        {
            if (position.y < Screen.height)
            {
                position.z = cam.nearClipPlane;
                return cam.ScreenToWorldPoint(position);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }
}
