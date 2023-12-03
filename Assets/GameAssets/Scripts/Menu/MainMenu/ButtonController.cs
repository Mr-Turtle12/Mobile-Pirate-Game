using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button vs;
    public Button Endless;
    public Button Freeplay;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dataCarrierObj = GameObject.Find("DataCarrier");
        vs.onClick.AddListener(() => { Destroy((Object)dataCarrierObj.GetComponent<IDataCarrierScript>()); dataCarrierObj.AddComponent<VsDataCarrier>(); });
        Endless.onClick.AddListener(() => { Destroy((Object)dataCarrierObj.GetComponent<IDataCarrierScript>()); dataCarrierObj.AddComponent<EndlessDataCarrier>(); });
        Freeplay.onClick.AddListener(() => { Destroy((Object)dataCarrierObj.GetComponent<IDataCarrierScript>()); dataCarrierObj.AddComponent<FreeplayDataCarrier>(); SceneManager.LoadScene("mapMenu"); });

    }
}
