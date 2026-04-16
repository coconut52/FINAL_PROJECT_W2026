using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;

    public float gravity = -9.81f;
    public float groundedVelocity = -2f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); 

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * z + right * x;

        if (move.magnitude > 1f)
            move.Normalize();

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedVelocity;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = move * speed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }
}