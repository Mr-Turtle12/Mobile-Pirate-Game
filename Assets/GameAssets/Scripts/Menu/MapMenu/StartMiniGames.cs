using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMiniGames : MonoBehaviour
{
    public ShipMovement Gets;
    public Button back;
    // Start is called before the first frame update
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
        if (gameIndex.Equals(0))// Cannonball Island
        {
            SceneManager.LoadScene("SeagullScene");

        }
        else if (gameIndex.Equals(1)) //Treasure Island
        {
            SceneManager.LoadScene("CompassGame");
        }
        else if (gameIndex.Equals(2)) //Water Invasion
        {
            SceneManager.LoadScene("RopeScene");
        }
        else if (gameIndex.Equals(3)) //Wood Island
        {
            SceneManager.LoadScene("TreeTap");
        }
    }
}
