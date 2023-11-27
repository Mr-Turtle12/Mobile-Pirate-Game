using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class StartMiniGames : MonoBehaviour
{
    public ShipMovement Gets;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startGame()
    {
        int gameIndex = Gets.GetCurrentLocationIndex();
        //Load Game withg Game index
        Debug.Log("load game with id:" + gameIndex);
        if (gameIndex.Equals(0))// Cannonball Island
        {
            Debug.Log("Load Cannonball island");
        }
        else if (gameIndex.Equals(1)) //Treasure Island
        {
            SceneManager.LoadScene("CompassGame");
        }
        else if (gameIndex.Equals(2)) //Water Invasion
        {
            Debug.Log("Load water invasion");
        }
        else if (gameIndex.Equals(3)) //Wood Island
        {
            SceneManager.LoadScene("LoadLogs");
        }
    }
}
