// Beacon Controller for Maize game

using UnityEngine;
using System.Collections;

public class BeaconController : MonoBehaviour
{
    //public Object endBeacon;
    public GameObject wallGen;

    //GameObject myBeacon;
    int horzSize, vertSize;
    System.Random rand;

    void Start ()
    {
        // Start/seed random
        string randSeed = System.DateTime.Now.ToString();
        rand = new System.Random(randSeed.GetHashCode());

        // Learn size of map to govern placement
        horzSize = wallGen.GetComponent<PrimWallGen>().getHorzSize();
        vertSize = wallGen.GetComponent<PrimWallGen>().getVertSize();
        
        // Start out somewhere random
        placeRandomBeacon();
	}

    // Put in the center of a random cell
    void placeRandomBeacon()
    {
        float xpos, ypos;
        Vector3 pos;

        xpos = rand.Next(0, horzSize) + .5f;
        ypos = rand.Next(0, vertSize) + .5f;
        //print(xpos + " " + ypos);
        pos = new Vector3(xpos, 0, ypos);
        transform.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        // Move to another location when a player collides
        if (other.gameObject.CompareTag("Player"))
        {
            placeRandomBeacon();
        }
    }

}
