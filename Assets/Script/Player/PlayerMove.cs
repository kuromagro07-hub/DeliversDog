using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // äOïîÇ©ÇÁèëÇ´ä∑Ç¶ÇΩÇ¢ïœêî
    [SerializeField] private float speed = 5f;
    [SerializeField] private float angle = 0f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var pos = transform.position;




        if (Input.GetKey(KeyCode.W))
        {
            pos.z += speed * Time.deltaTime;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.z -= speed * Time.deltaTime;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        transform.position = pos;
    }
}
