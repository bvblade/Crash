using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// [Brough, Heath]
// [5/2/2023]
// controls all the functions of the player
// updated [5/9/2023]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text wumpasText;
    public Text gameOver;
    public Text winText;
    private GameObject Canvas;

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
    public bool isAttacking;
    // if the player is grounded they can jump
    public bool isGrounded = false;
    // bool variable that tells fixed update when to jump
    private bool jump = false;
    // true, the player can attack, false, the player 
    private bool attackCooldown = true;

    private Vector3 spawn;

    public Material attackingMaterial;
    private Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        defaultMaterial = GetComponent<Renderer>().material;
        // set the spawn point
        spawn = transform.GetChild(1).transform.position;
        // access the canvas
        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        livesText = Canvas.transform.GetChild(0).GetComponent<Text>();
        wumpasText = Canvas.transform.GetChild(1).GetComponent<Text>();


    }

    private void OnLevelWasLoaded(int leve2)
    {
        spawn = transform.GetChild(1).transform.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        gameOver.enabled = false;
        winText.enabled = false;

        movement();
        attack();
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
    // calls the coroutine to attack
    private void attack()
    {
        // if the player presses E and the attack cooldown has ended then attack
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
        if (other.CompareTag("Win"))
        {
            this.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            winText.enabled = true;
        }
        // if the player touches a Wumpa, add 1 wumpa
        if (other.CompareTag("Wumpa"))
        {
            wumpas++;
            // destroy the wumpas when you collect them
            Destroy(other.gameObject);
            wumpasText.text = "Wumpas: " + wumpas.ToString();
        }
        // if the player touches an enemy and they are not attacking, respawn them
        if (other.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                Debug.Log("Killed an enemy");
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Hit an enemy");
                respawn();
            }
            
        }
        //If player is attacking crate while touching it, then it disappears
        if (other.CompareTag("Crate") && isAttacking)
        {
            other.gameObject.SetActive(false);
        }

        // if the player touches a spike, respawn them
        if (other.CompareTag("Spike"))
        {
            Debug.Log("Hit a spike");
            respawn();
        }
        // if the player touches a pit, respawn them
        if (other.CompareTag("Pit"))
        {
            Debug.Log("Hit the endless pit");
            respawn();
        }
        // if the player hits the top of a crate, bounce them
        if (other.CompareTag("crateTop"))
        {
            // bounce the player 
            rigidbody.AddForce(Vector3.up * jumpForce * 1.7f, ForceMode.Impulse);
            // set the parent of the crateTop (the crate) inactive
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
        // if the player hits the top of an enemy, bounce them
        if (other.CompareTag("stompBox"))
        {
            // bounce the player 
            rigidbody.AddForce(Vector3.up * jumpForce * 1.7f, ForceMode.Impulse);
            // set the parent of the stompBox (the enemy) inactive
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // removes one life and returns the player to the spawnpoint set at the start of the level
    private void respawn()
    {
        lives--;
        transform.position = spawn;
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            //deactivates this script component
            this.enabled = false;
            //disables the objects renderer
            GetComponent<MeshRenderer>().enabled = false;
            gameOver.enabled = true;
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
        // change the player material to the attacking material
        transform.GetComponent<Renderer>().material = attackingMaterial;
        // wait for 1 seconds
        yield return new WaitForSeconds(1f);
        // player is no longer attacking
        isAttacking = false;
        // change the material back to the default material
        transform.GetComponent<Renderer>().material = defaultMaterial;
        Debug.Log("Player is not attacking");
        // wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Player can attack again");
        // player can attack again
        attackCooldown = true;
    }
}
