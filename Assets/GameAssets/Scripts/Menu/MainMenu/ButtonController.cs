using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button vs;
    public Button Endless;
    public Button Freeplay;
    public DataCarrierScript updateType;
    // Start is called before the first frame update
    void Start()
    {
        vs.onClick.AddListener(() => updateType.SetGameMode(GameMode.Vs));
        Endless.onClick.AddListener(() => updateType.SetGameMode(GameMode.Endless));
        Freeplay.onClick.AddListener(() => updateType.SetGameMode(GameMode.FreePlay));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
