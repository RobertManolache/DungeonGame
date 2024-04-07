using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    GameSesion gameSesion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int CurrentScenIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = CurrentScenIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
                FindObjectOfType<GameSesion>().ResetGameSession();


            }
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
