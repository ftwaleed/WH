using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    public float yOffset = 2f;
    public Vector2 minBounds; // Minimum boundaries of the scene
    public Vector2 maxBounds; // Maximum boundaries of the scene

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(target.position.y + yOffset, minBounds.y, maxBounds.y);
        Vector3 newPos = new Vector3(clampedX, clampedY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
