using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // ŠO•”‚©‚ç‘‚«Š·‚¦‚½‚¢•Ï”
    [SerializeField] private float speed = 5f;
    [SerializeField] private float angle = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        var pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.z += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.z -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
