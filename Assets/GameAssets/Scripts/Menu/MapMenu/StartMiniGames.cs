using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartMiniGames : MonoBehaviour
{
    public ShipMovement Gets;
    public Button back;
    public Text texts;
    [SerializeField] private List<string> levels;

    void Start()
    {
        if (back != null)
        {
            back.onClick.AddListener(backButton);
        }
        GetComponent<Button>().onClick.AddListener(startGame);

    }

    // Update is called once per frame
    void Update()
    {
        if (Gets.GetCurrentLocationIndex().Equals(0) && texts.text.Equals("Play"))
        {
            texts.text = "Back";
        }
        else if (!Gets.GetCurrentLocationIndex().Equals(0) && texts.text.Equals("Back"))
        {
            texts.text = "Play";
        }
    }
    void backButton()
    {
        SceneManager.LoadScene("mainMenu");
    }

    void startGame()
    {
        int gameIndex = Gets.GetCurrentLocationIndex();
        //Load Game withg Game index
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
