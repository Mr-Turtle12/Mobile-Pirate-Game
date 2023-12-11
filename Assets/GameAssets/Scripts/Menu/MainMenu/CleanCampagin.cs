using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanCampagin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
