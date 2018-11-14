using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Vector3 startingPosition;
    public GameObject player;
    private void Awake()
    {
         player = GameObject.FindWithTag("Player");
    }
    // Use this for initialization
    void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = player.transform.position;
        temp.z = temp.z - 10;

        // Assign value to Camera position
        transform.position = temp;
    }
}
