using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScene : MonoBehaviour
{
    private VsDataCarrier dataCarrierScript;
    public Text PlayerDisplay;
    public Button back;
    // Start is called before the first frame update
    void Start()
    {
        GameObject DataCarrier = GameObject.Find("DataCarrier");
        dataCarrierScript = DataCarrier.GetComponent<VsDataCarrier>();
        PlayerDisplay.text = dataCarrierScript.getPlayerOrder();
        back.onClick.AddListener(() => SceneManager.LoadScene("mainMenu"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
