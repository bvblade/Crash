using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// [Brough, Heath]
// [5/9/2023]
// teleports the player to the specified scene and gets rid of the player 

public class PortalBehavior : MonoBehaviour
{
    public GameObject player;
    // Variable to handle what scene the player will go to next
    public int newSceneIndex;

     GameObject[] playerObjects;

    // happens before the start function
    private void Awake()
    {
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
<<<<<<< HEAD
=======
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> parent of d0e887b (more stuff)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // save the player and canvas through loads
            DontDestroyOnLoad(other);
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        }
        switchScene();
    }

    // swap the scenes
    public void switchScene()
    {
        // will load the build idnex that we set newSceneIndex to in unity
        SceneManager.LoadScene(newSceneIndex);
    }
}
