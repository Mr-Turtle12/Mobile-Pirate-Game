using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AddName : MonoBehaviour
{
    public Button plus;
    public Button Play;
    public DataCarrierScript dataCarrier;
    public GameObject[] players;
    public TextMeshProUGUI playerNameTextField;
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
        dataCarrier.SetPlayerList(playersName);
        GameMode gameMode = dataCarrier.GetGameMode();
        if (gameMode.Equals(GameMode.FreePlay))
        {
            //Load Main map
            Debug.Log("Load up main map");
        }
        else if (gameMode.Equals(GameMode.Endless))
        {
            //Load up a random mini game
            Debug.Log("Load up a random miniGame");
        }
        else if (gameMode.Equals(GameMode.Vs))
        {
            //Load first game
            Debug.Log("Load first miniGame");
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

        }

    }
}
