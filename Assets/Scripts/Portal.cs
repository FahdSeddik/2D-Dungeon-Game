using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to change scene

public class Portal : Collidable
{
    public string[] sceneNames; //to hold all dungeon names (scene names)
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            //Save state
            GameManager.instance.SaveState();

            //teleport
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; //to choose random scenes
            SceneManager.LoadScene(sceneName); //to load the random scene
            //if we didnt want to type using
            //we could write UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
