using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public AddName PlayerInfo;
    public DataCarrier SaveInfo;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(OnPlayGame);


    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnPlayGame()
    {
        SaveInfo.PlayerNames = PlayerInfo.PlayersName;
        //Load Next scene

    }
}

