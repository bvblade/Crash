using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Brough, Heath]
// [5/8/2023]
// spawns wumpas when the player destroys the crate
public class CrateBehavior : MonoBehaviour
{
    // wumpa object reference
    public GameObject wumpaPrefab;
    // the number of wumpas that a crate will spawn, defaults to 5 wumpas
    public int numberOfWumpas = 5;
    
    // when the crate is disabled, spawn wumpas
    private void OnDisable()
    {
        
        for (int i = 0; i < 5; i++)
        {
            // spawn 5 wumpas at the crate location
            Instantiate(wumpaPrefab, transform.position, transform.rotation);
        }
    }
}
