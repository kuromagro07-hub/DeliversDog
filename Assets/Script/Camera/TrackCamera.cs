using UnityEngine;



public class TrackCamera : MonoBehaviour
{
    [Tooltip("追従対象")]
    [SerializeField] GameObject target;

    [Tooltip("追従時速度")]
    [Range(0f, 1f)]
    [SerializeField] float smoothSpeed = 0.1f;

    [Tooltip("カメラオフセット")]
    [SerializeField] Vector3 offset = new Vector3(0, 5, -10);


    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }

}
