using UnityEngine;

namespace Ashking.OOP
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform targetToFollow;
        [Min(0.1f)]
        [SerializeField] float distanceFromTarget = 22f;
        [SerializeField] float smoothing = 5f;
        Vector3 offset;
        
        void Start()
        {
            offset = transform.position - targetToFollow.position;
            offset.z = -distanceFromTarget;
        }

        void LateUpdate()
        {
            offset.z = -distanceFromTarget;
            var targetPosition = targetToFollow.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        }
    }

}