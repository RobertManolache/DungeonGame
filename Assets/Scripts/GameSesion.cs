using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSesion : MonoBehaviour
{
     [SerializeField] public int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
      public int score = 0;
    

    void Awake()
    {
        int numGameSesion = FindObjectsOfType<GameSesion>().Length;
        if(numGameSesion > 1)
        {
            Destroy(gameObject);
        } 
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void PlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    } 
   void TakeLife()
    {
        
        playerLives--;
        int CurrentScenIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScenIndex);
        livesText.text = "Lives: "+ playerLives.ToString();
 
    }
    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();

    }
   

}
