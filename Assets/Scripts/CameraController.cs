// CamerController for Maize game

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player; 

    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    void LateUpdate()
    {
        // Maintain same offset as at start
        transform.position = player.transform.position + offset;
        // Maintain same rotation
        transform.rotation = player.transform.rotation;
    }
	
}
