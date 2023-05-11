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
    
    private void OnTriggerEnter(Collider other)
    {
        // saves the player when they swap scenes
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(other);
        }
        // swap the scene
        switchScene();
    }
    
    // switches the scene to the specified scene
    public void switchScene()
    {
        // will load the build idnex that we set newSceneIndex to in unity
        SceneManager.LoadScene(newSceneIndex);
    }
}
