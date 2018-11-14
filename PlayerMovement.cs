using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public static GameController control;
    public Rigidbody2D rb2;
    public SpriteRenderer spr;
    public bool isGrounded = true;
    public bool sneaking = false;
    public float speed = 3f;
    public float jumpForce = 1000;
    public int jumpCount = 0;
    public bool bioTriggerTrue = false;
    bool menu = false;
    public Vector3 StartingPosition;

    public int MinimumBread = 50;

    public AudioClip footstep;
    public AudioClip jump;

    [SerializeField]
    public AudioSource srce;
    public AudioSource jsrce;

    // Use this for initialization
    void Start () {
        rb2 = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        StartingPosition = transform.position;
        footstep = GetComponent<AudioClip>();
        jump = GetComponent<AudioClip>();
        srce = GetComponent<AudioSource>();
        jsrce = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        var input = Input.GetAxis("Horizontal"); // This will give us left and right movement (from -1 to 1). 
        var movement = input * speed;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            spr.flipX = true;
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            spr.flipX = false;
        if (Input.GetKeyDown(KeyCode.Q))
            menu = true;
        if (sneaking == true)
        {
            movement /= 2;
        }
        if (isGrounded == false)
            sneaking = false;
        //isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        rb2.velocity = new Vector3(movement, rb2.velocity.y, 0);
        if ((Input.GetKeyDown(KeyCode.Space) == true || (Input.GetKeyDown(KeyCode.UpArrow))) && isGrounded == true)
        {
            rb2.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
            jsrce.Play(10);
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) == true) || Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            sneaking = true;
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift) == true) || Input.GetKeyUp(KeyCode.DownArrow) == true)
        {
            sneaking = false;
        }

        if ((input != 0 && isGrounded == true) && srce.isPlaying == false)
            srce.Play(500);
    }
    private void OnTriggerExit2D(Collider2D collision) // prevents jumping from midair
    {
        if(collision.gameObject.tag == "Walkable")
        {
            isGrounded = false;
        }
        else if(collision.gameObject.tag == "BioInforTrig")
        {
            bioTriggerTrue = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col) // col is the trigger object we collided with
    {

        if(col.gameObject.tag == "Walkable" || col.gameObject.tag == "Movable") //if it is on a "walkable" surface //
        {
            isGrounded = true; //then "is grounded" = true //
            
        }
        else if(col.tag == "Health")
        {
            Destroy(col.gameObject);
            GameController.control.Health++;
        }
        else if(col.tag == "Lockpick")
        {
            Destroy(col.gameObject);
            GameController.control.lockPickCount++;
        }
        else if (col.tag == "Breadcrumb")
        {
            //coins++;
            Destroy(col.gameObject); // remove the coin
            GameController.control.BreadCount++;
            if(GameController.control.BreadCount > MinimumBread)
            {
                GameController.control.Health++;
                GameController.control.BreadCount = 0;
            }
        }
        else if (col.tag == "Water")
        {
            // Death? Reload Scene? Teleport to start:
            //transform.position = startingPosition;
        }
        else if(col.gameObject.tag == "Enemy")
        {
            if(GameController.control.lockPickCount > 0)
            {
                //SceneManager.LoadScene(0); // Edit to match escape sequence, currently set to return to menu
                GameController.control.lockPickCount--;
            }
        }
        else if (col.gameObject.tag == "DeathCol")
        {
            // Death? Reload Scene? Teleport to start:
            //transform.position = startingPosition;
            bioTriggerTrue = false;
            if(GameController.control.Health > 0)
            {
                GameController.control.Health--;
                transform.position = StartingPosition;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(GameController.control.Health == 0)
            {
                GameController.control.Health = 1000;
                SceneManager.LoadScene(0);
            }
        }
        else if (col.tag == "End")
        {
            // Load next level? Heres how you get this level's scene number, add 1 to it and load that scene:
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(0);
        }
        else if(col.tag == "BioInforTrig")
        {
            bioTriggerTrue = true;
        }
    }
    private void FixedUpdate()
    {

    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Health: " + GameController.control.Health);
        GUI.Label(new Rect(10, 40, 100, 30), "Lock Picks: " + GameController.control.lockPickCount);
        GUI.Label(new Rect(10, 70, 100, 30), "Bread: " + GameController.control.BreadCount);
        if (menu == true)
        {
            if(GUI.Button(new Rect(100, 100, 100, 30), "Resume"))
            {
                menu = false;
            }
            if(GUI.Button(new Rect(100, 130, 100, 30), "Settings"))
            {
                SceneManager.LoadScene(0);
            }
            if(GUI.Button(new Rect(100, 160, 100, 30), "Quit"))
            {
                SceneManager.LoadScene(0);
            }

        }
        if(bioTriggerTrue == true)
        {
            GUI.Label(new Rect(100, 100, 200, 300), "Avoid Chemical Weapons like these that Assad keeps in Syria. Chemical weapons not only provide the ability for Syria to attack it's neighbors, but chemical weapons are used in civilian attacks and are stolen by terrorist groups.");
        }
    }
}

