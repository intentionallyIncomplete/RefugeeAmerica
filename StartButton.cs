using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour {
    private void Awake()
    {
        GameObject.Destroy(GameObject.FindWithTag("Player"));
    }
    // Use this for initialization
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
