using UnityEngine;
using System.Collections;

public class WallGen : MonoBehaviour
{
    public Transform wall;
	void Start ()
    {
        Instantiate(wall, new Vector3(-5.0f, 2.5f, -5.0f), Quaternion.identity);
	}
	
}
