using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Brough, Heath]
// [5/2/2023]
// controls all the functions of the player

public class PlayerController : MonoBehaviour
{
    // how fast the player goes
    public float moveSpeed;
    // how many lives the player has
    public int lives;
    // how many wumpas the player has
    public int wumpas;
    // how high the player can jump
    public int jumpForce;

    // the rigidbody component
    Rigidbody rigidbody;
    // determines whether or not the player is attacking or not
    private bool isAttacking;
    // if the player is grounded they can jump
    public bool isGrounded = false;
    // bool variable that tells fixed update when to jump
    private bool jump = false;
    // true, the player can attack, false, the player 
    private bool attackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        // fires a ray from the center of the player downwards 1.15 meters
        // returns true if it hits something
        // returns False if it doesnt hit anything
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // if the player presses down the space bar then jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        // rule of thumb - do physics manipulation in fixed update instead of update
        // if jump is true then jump once
        if (jump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // sets jump to false after adding force so that jump does happen constantly
            jump = false;
        }
    }
    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && attackCooldown)
        {
            StartCoroutine(onEPress());
            
        }
    }

    // controls the movement of the player (Left to Right)
    private void movement()
    {
        // Moves the Player to the left if the player presses down the "A" key and the player is to the right of x = -8
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime * -1;
        }

        // Moves the Player to the right if the player presses down the "D" key and the player is to the left of x = 8
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        // Moves the Player to the left if the player presses down the "S" key and the player is to the right of x = -8
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime * -1;
        }

        // Moves the Player to the right if the player presses down the "D" key and the player is to the left of x = 8
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wumpa"))
        {
            wumpas++;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Enemy"))
        {
            lives--;
        }
        if (other.CompareTag("Spike"))
        {
            lives--;
        }
    }

    // when you press E, you attack and turn red for 1 second and the attack has a cooldown of .5 seconds after you are done attacking
    private IEnumerator onEPress()
    {
        // says that the player needs to wait for the cooldown to attack again
        attackCooldown = false;
        // says the player is attacking
        isAttacking = true;
        Debug.Log("Player is attacking");
        // wait for 1 seconds
        yield return new WaitForSeconds(1f);
        // player is no longer attacking
        isAttacking = false;
        Debug.Log("Player is not attacking");
        // wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        // player can attack again
        attackCooldown = true;
    }
}
