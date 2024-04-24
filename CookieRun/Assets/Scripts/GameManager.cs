using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private bool GameOver = false;
    private int Stage = 1;
    public bool jump = true;
    public bool pyjump = false;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return _instance;
        }
    }
    
    public bool gameOver
    {
        get { return GameOver; }
        set { GameOver = value; }
    }

    public int stage
    {
        get { return Stage; }
        set { Stage = value; }
    }
}
