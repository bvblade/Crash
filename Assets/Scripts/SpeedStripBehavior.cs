using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Brough, Heath]
// [5/11/2023]
// moves the player if they are touching the speed strip and spawns arrows to show what it does

public class SpeedStripBehavior : MonoBehaviour
{
    // tells if the player is currently touching the speed strip
    private bool isTouching = false;
    // how fast the speed strip makes you go
    public float boostSpeed = 3;
    // references the player to move it
    private GameObject player;
    // where the arrow is going to spawn
    public GameObject arrowSpawn;
    // the arrow object to spawn
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        // finds the player and sets it to player
        player = GameObject.FindGameObjectWithTag("Player");
        // spawns arrows repeatedly
        InvokeRepeating("spawnArrow", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // if the player is touching the object, move them forward
        if (isTouching)
        {
            player.gameObject.transform.position += transform.forward * boostSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player touches the strip, set isTouching to true
        if (other.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if the player stops touching the strip, set is touching to false
        if (other.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
    
    // spawns an arrow at arrowSpawn
    private void spawnArrow()
    {
        Instantiate(arrow, arrowSpawn.transform);
    }
}
