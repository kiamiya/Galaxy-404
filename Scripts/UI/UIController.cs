using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
    
{
    public PlayerData player;
    private Text scoreText, lifeText, ammoText;
    public static bool gameIsPaused = false;
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        lifeText = GameObject.Find("Lifes").GetComponent<Text>();
        ammoText = GameObject.Find("Ammo").GetComponent<Text>();
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.P))
	    {
            gameIsPaused = !gameIsPaused;
            pauseGame();
	    }
       
    }
    public void UpdateScore(int value)
    {
        scoreText.text = "Score : " + value; 
    }
    public void UpdateLife(int value)
    {
        lifeText.text = "Lifes : " + value;
    }
    public void UpdateAmmo(int value)
    {
        ammoText.text = "Ammo : " + value;
    }
    void pauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
   
}
