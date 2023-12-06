using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddName : MonoBehaviour
{
    public Button plus;
    public Button Play;
    private IDataCarrierScript dataCarrier;
    public GameObject[] players;
    public TMP_InputField playerNameTextField;
    public List<string> playersName;
    // Start is called before the first frame update
    void Start()
    {
        plus.onClick.AddListener(AddPlayer);
        Play.onClick.AddListener(playGame);
        playersName = new List<string>();
        foreach (GameObject objPlayers in players)
        {
            objPlayers.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    void playGame()
    {
        GameObject dataCarrierObj = GameObject.Find("DataCarrier");
        dataCarrier = dataCarrierObj.GetComponent<IDataCarrierScript>();
        string nextScene = "";
        switch (dataCarrier.GetGameMode())
        {
            case GameMode.Endless:
                (dataCarrier as EndlessDataCarrier).AddPlayer(playersName);
                nextScene = (dataCarrier as EndlessDataCarrier).StartEndlessGame();
                SceneManager.LoadScene(nextScene);
                break;
            case GameMode.Vs:
                (dataCarrier as VsDataCarrier).AddPlayer(playersName);
                nextScene = (dataCarrier as VsDataCarrier).StartVsMode();
                SceneManager.LoadScene(nextScene);
                break;
        }
    }
    void AddPlayer()
    {
        int CurrentPlayerCount = playersName.Count;
        if (CurrentPlayerCount <= 7)
        {

            players[CurrentPlayerCount].SetActive(true);
            string name = playerNameTextField.text;
            players[CurrentPlayerCount].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            playersName.Add(name);
            playerNameTextField.text = "";
        }

    }
}
