using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance = null;
    public static bool lvl1Done;
    public static bool lvl2Done;

    string hublevel = "HUB";
    string level1 = "level1";
    string level2 = "Level2";
    string bosslevel = "level4";

    public static int jetPack = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }       
    }
}
