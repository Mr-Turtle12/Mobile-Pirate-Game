using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartMiniGames : MonoBehaviour
{
    public ShipMovement Gets;
    public Button back;
    [SerializeField] private List<string> levels;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(startGame);
        back.onClick.AddListener(backButton);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void backButton()
    {
        SceneManager.LoadScene("mainMenu");
    }

    void startGame()
    {
        int gameIndex = Gets.GetCurrentLocationIndex();
        //Load Game withg Game index
        Debug.Log("load game with id:" + gameIndex);
        if (gameIndex.Equals(0))
        {
            SceneManager.LoadScene(levels[0]);
        }
        else if (gameIndex.Equals(1)) 
        {
            SceneManager.LoadScene(levels[1]);
        }
        else if (gameIndex.Equals(2)) 
        {
            SceneManager.LoadScene(levels[2]);
        }
        else if (gameIndex.Equals(3)) 
        {
            SceneManager.LoadScene(levels[3]);
        }
    }
}
