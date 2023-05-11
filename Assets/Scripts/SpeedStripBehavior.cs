using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStripBehavior : MonoBehaviour
{

    private bool isTouching = false;
    public float boostSpeed = 3;
    private GameObject player;

    public GameObject arrowSpawn;

    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("spawnArrow", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            player.gameObject.transform.position += transform.forward * boostSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
    
    private void spawnArrow()
    {
        Instantiate(arrow, arrowSpawn.transform);
    }
}
