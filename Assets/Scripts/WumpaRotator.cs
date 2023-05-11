using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Valdez, Logan]
 * Date: [05/08/2023]
 * [makes the wumpas rotate]
 */

public class WumpaRotator : MonoBehaviour
{
    // how fast the wumpa rotates
    private int degreesPerSecond = 90;
    // how fast the wumpa moves up and down
    public float speed;
    // if true, wumpa moves up. if false, wumpa moves down
    private bool goingUp;
    // how far up and down the wumpa will go
    private float bounds = 0.1f;
    // the starting position of the wumpa
    private Vector3 wumpaPos;
    private void Start()
    {
        // save the initial position of the wumpa
        wumpaPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // rotates the coin degreesPerSecond degrees every second
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        upAndDown();
    }

    // moves the wumpa up and down until it hits the upper and lower bounds
    // bounds are determined by the initial height of the wumpa + or - the bounds variable
    private void upAndDown()
    {
        // if true, go up
        if (goingUp)
        {
            // when the wumpa hits the upper bound, go down
            if (transform.position.y >= wumpaPos.y + bounds)
            {
                goingUp = false;
            }
            // until the wumpa hits the upper bounds, keep moving up
            else
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        // if false, go down
        else
        {
            // when the wumpa hits the lower bound, go up
            if (transform.position.y <= wumpaPos.y - bounds)
            {
                goingUp = true;
            }
            // until the wumpa hits the upper bounds, keep moving up
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
        }
    }
}
