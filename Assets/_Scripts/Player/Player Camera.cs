using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed;
    [SerializeField] Vector3 offset;
    public bool smooth = true;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        if (smooth)
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        else
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0);
    }

    public void ToggleSmoothSpeed()
    {
        smooth = !smooth;
    }
}