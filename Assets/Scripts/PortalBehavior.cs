using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    // Variable to handle what scene the player will go to next
    public int newSceneIndex;

     GameObject[] playerObjects;

    // happens before the start function
    private void Awake()
    {
        DontDestroyOnLoad(canvas);

        // array of player objects
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        if (playerObjects.Length > 1)
        {
            player = playerObjects[1];
            // destroy the player that is assigned
            Destroy(player);
        }
        else
        {
            // dont want to destroy the following: player
            DontDestroyOnLoad(player);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(other);
        }
        switchScene();
    }

    public void switchScene()
    {
        // will load the build idnex that we set newSceneIndex to in unity
        SceneManager.LoadScene(newSceneIndex);
    }
}
