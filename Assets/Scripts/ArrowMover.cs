using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Brough, Heath]
// [5/11/2023]
// moves the arrow until it hits the arrowStopPoint

public class ArrowMover : MonoBehaviour
{
    // how fast the arrow moves
    public int moveSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    // moves the arrow
    public void movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the arrow gets the the endpoint, destroy it
        if (other.CompareTag("arrowStopPoint"))
        {
            Destroy(this.gameObject);
        }
    }
}
