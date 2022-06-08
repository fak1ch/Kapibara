using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = 10;
        return camera.ScreenToWorldPoint(position);
    }
}
