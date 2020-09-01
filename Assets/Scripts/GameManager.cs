using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string gameState = "Start Screen";

    private static GameManager instance;
    public GameObject titleScreen;
    public GameObject gameUI;
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;
    public int lives = 3;
    public GameObject playerDeathScreen;
    public GameObject gameOverScreen;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Attempted to create a second game manager.");
            Destroy(this.gameObject);
        }
    }

    public void ChangeState(string newState)
    {
        gameState = newState;
    }
    public void StartGame()
    {
        UnityEngine.Debug.Log("Game has started!");
        ChangeState("Initialize Game");
    }

    private void Update()
    {
        if(gameState == "Start Screen")
        {
            //Do the state behavior
            StartScreen();
            //Check for transitions
            //This transitions on a button click
        }
        else if(gameState == "Initialize Game")
        {
            //Do the state behavior
            InitializeGame();
            //Check for transitions
            ChangeState("Spawn Player");
        }
        else if(gameState == "Spawn Player")
        {
            //Do the state behavior
            SpawnPlayer();
            //Check for transitions
            ChangeState("Gameplay");
        }
        else if(gameState == "Gameplay")
        {
            //Do the state behavior
            Gameplay();
            //Check for transitions
            if (player == null && lives > 0)
            {
                ChangeState("Player Death");
            }
            else if (player == null && lives <= 0)
            {
                ChangeState("Game Over");
            }
        }
        else if(gameState == "Player Death")
        {
            //Do the state behavior
            PlayerDeath();
            //Check for transitions
            if(Input.anyKeyDown)
            {
                ChangeState("Spawn Player");
            }
        }
        else if(gameState == "Game Over")
        {
            //Do the state behavior
            GameOver();
            //Check for transitions
            if(Input.anyKeyDown)
            {
                ChangeState("Start Screen");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Game Manager tried to change to non-existant state");
        }
    }

    public void StartScreen()
    {
        //Show the menu
        if(!titleScreen.activeSelf)
        {
            titleScreen.SetActive(true);
        }
 
    }

    public void InitializeGame()
    {
        //Reset all variables
        //TODO: Reset variables in initialization
        //Turn off menu
        titleScreen.SetActive(false);
        //Turn on game ui
        gameUI.SetActive(true);
    }

    public void SpawnPlayer()
    {
        //Add the player to the world
        player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
    }

    public void Gameplay()
    {
       
    }

    public void PlayerDeath()
    {

        if(!playerDeathScreen.activeSelf)
        {
            playerDeathScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        if(gameUI.activeSelf)
        {
            gameUI.SetActive(false);
        }
        if(!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
