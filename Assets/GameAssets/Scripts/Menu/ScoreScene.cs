using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScene : MonoBehaviour
{
    private IDataCarrierScript dataCarrierScript;
    public Text PlayerDisplay;
    public Button back;
    // Start is called before the first frame update
    void Start()
    {
        GameObject DataCarrier = GameObject.Find("DataCarrier");
        dataCarrierScript = DataCarrier.GetComponent<IDataCarrierScript>();
        switch (dataCarrierScript.GetGameMode())
        {
            case GameMode.Endless when dataCarrierScript is EndlessDataCarrier endlessCarrier:
                PlayerDisplay.text = "Total score of: " + endlessCarrier.OverallScore;
                break;
            case GameMode.Vs when dataCarrierScript is VsDataCarrier vsCarrier:
                PlayerDisplay.text = vsCarrier.getPlayerOrder();
                break;
        }
        back.onClick.AddListener(() => SceneManager.LoadScene("mainMenu"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
