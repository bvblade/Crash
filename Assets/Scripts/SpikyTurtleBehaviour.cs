using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Valdez, Logan]
 * Date: [05/09/2023]
 * [Controls the spiky turtle's behaviour]
 */

public class SpikyTurtleBehaviour : MonoBehaviour
{
    //Speed Controller
    public float speed;
    //move to left position
    private Vector3 leftPos;
    //move to right position
    private Vector3 rightPos;
    //left object to get the left position from
    public GameObject leftPoint;
    //right object to get right position from
    public GameObject rightPoint;

    public GameObject rightHead;
    public GameObject leftHead;

    //determines whether the enemy should move left if true or right if false
    private bool moveLeft;

    // Start is called before the first frame update
    void Start()
    {
        //sets the left position to the initial position of the left point object
        leftPos = leftPoint.transform.position;
        //sets the right position to the initial position of the right point object
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if move left is true, check to see if the enemy should still be moving left
        if (moveLeft)
        {
            //if the enemy is too far to the left then set move left to false which will make it move right
            if (transform.position.x <= leftPos.x)
            {
                moveLeft = false;
                rightHead.SetActive(true);
                leftHead.SetActive(false);
            }
            //if the enemy is within bounds to move left, do so
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                rightHead.SetActive(false);
                leftHead.SetActive(true);
            }
        }
        //the same as above but going right instead
        else
        {
            if (transform.position.x >= rightPos.x)
            {
                moveLeft = true;
                rightHead.SetActive(false);
                leftHead.SetActive(true);
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                rightHead.SetActive(true);
                leftHead.SetActive(false);
            }
        }
    }
}
