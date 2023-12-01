using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public static int coins = 0;

    public static int trayBonus = 0;

    public static int magicWeightWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PauseGame(){
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    public static void UnpauseGame(){
        gameIsPaused = false;
        Time.timeScale = 1;
    }
}
