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
    public GameObject[] players;
    public TextMeshProUGUI PlayerNameTextField;
    public List<string> PlayersName;
    // Start is called before the first frame update
    void Start()
    {
        plus.onClick.AddListener(AddPlayer);
        PlayersName = new List<string>();
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    void AddPlayer()
    {
        int CurrentPlayerCount = PlayersName.Count;
        if (CurrentPlayerCount <= 7)
        {

            players[CurrentPlayerCount].SetActive(true);
            string name = PlayerNameTextField.text;
            players[CurrentPlayerCount].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            PlayersName.Add(name);

        }

    }
}
