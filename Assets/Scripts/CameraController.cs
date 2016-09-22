// CamerController for Maize game

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    float rotateSpeed;

    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
        rotateSpeed = player.GetComponent<PlayerController>().rotateSpeed;
        print(rotateSpeed);
    }

    float vertRotate;
    void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        // Maintain same offset as at start
        transform.position = player.transform.position + offset;

        // Vertical rotation
        // Match player rotation, but then bring the vertical back and add
        // any from input
        vertRotate = Input.GetAxis("Mouse Y");
        float oldVertRotate = transform.rotation.eulerAngles.x;
        transform.rotation = player.transform.rotation;
        transform.RotateAround(player.transform.position, player.transform.right,
            oldVertRotate + (-vertRotate * rotateSpeed));      
    }
	
}
