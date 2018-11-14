using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController control;
    public int BreadCount = 0;
    public int lockPickCount = 0;
    public int Health = 10000;

    // Use this for initialization
    void Awake () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Destroy(gameObject);
        if(control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }

	}


    // Update is called once per frame

}
