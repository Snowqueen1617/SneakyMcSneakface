using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
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
    public void StartGame()
    {
        UnityEngine.Debug.Log("Game has started!");
    }
}
