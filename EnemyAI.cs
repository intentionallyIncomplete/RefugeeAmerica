using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private static GameController control;
    //public GameObject bulletPrefab;
    //public Transform gun;
    /*public float sightDistance = 5f;
    public float rateOfFire = 2f;
    public float nextShotTime = 0f;*/
    public int tickCounter = 0;
    public float speed = .1f;
    public int walkDistance = 1000;
    
    public Transform sp;
    public RaycastHit2D hit;
    private Vector2 v2;
    public GameObject player;
    public SpriteRenderer spr;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        v2 = transform.position;
    }

    void Start()
    {
        hit = GetComponent<RaycastHit2D>();
        sp = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        // Assign value to Camera position
        transform.position = temp;
        v2 = transform.position;

        //RaycastHit hit;
        if (tickCounter % walkDistance < walkDistance / 2)
        {
            hit = Physics2D.Raycast(v2, Vector2.right, 2);
            Debug.DrawRay(v2, Vector2.right);
        }
        else
        {
            hit = Physics2D.Raycast(v2, Vector2.left, 2);
            Debug.DrawRay(v2, Vector2.left);
        }

        //Debug.DrawRay(v2, Vector2.right);

        if (hit.collider.tag == "Player" && player.GetComponent<PlayerMovement>().sneaking == false)
        {
            if (GameController.control.lockPickCount > 0)
            {
                GameController.control.lockPickCount--;
                player.transform.position = player.GetComponent<PlayerMovement>().StartingPosition;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            //SceneManager.LoadScene(0);
        }


    }
    private void FixedUpdate()
    {

        if (tickCounter % walkDistance == walkDistance / 2)
            spr.flipX = true;
        if (tickCounter % walkDistance == 0)
            spr.flipX = false;
        tickCounter++;
        if (tickCounter%walkDistance < walkDistance / 2)
        {
            Vector3 temp = transform.position;
            temp.x = temp.x + speed;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.x = temp.x - speed;
            transform.position = temp;
        }
    }
}

