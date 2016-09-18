// PlayerController for the Maize game

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    private Rigidbody rb;

	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // W and S for forward/backward
        float vertMove = (Input.GetKey("w") ? 1.0f : 0.0f)
            - (Input.GetKey("s") ? 1.0f : 0.0f);
        // A and D for left/right
        float horzMove = (Input.GetKey("d") ? 1.0f : 0.0f)
            - (Input.GetKey("a") ? 1.0f : 0.0f);

        Vector3 movement = new Vector3(horzMove, 0.0f, vertMove);
        rb.AddRelativeForce(movement * moveSpeed);

        float sideRotate = Input.GetAxis("Mouse X");
        transform.Rotate(0.0f, sideRotate * rotateSpeed, 0.0f);
    }
}
