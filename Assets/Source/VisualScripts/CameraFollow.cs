using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 5f;

    private void LateUpdate()
    {
        var targetPosition = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);

        transform.position = targetPosition;
    }
}