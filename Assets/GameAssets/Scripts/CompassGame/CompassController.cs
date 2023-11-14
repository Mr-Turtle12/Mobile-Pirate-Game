using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class CompassController : MonoBehaviour
{

    public Transform Goal_hand;
    public Transform Compass_hand;
    public GameObject background;
    private int Count;
    private GyroController phoneController;
    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();

        Count = 0;
        phoneController.EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 previousEulerAngles = Compass_hand.eulerAngles;

        if (Mathf.Abs(previousEulerAngles.z - Goal_hand.rotation.eulerAngles.z) <= 2)
        {
            Goal_hand.GetComponent<RandomZPos>().ChangePos();
            Count++;
        }
        if (Count <= 3)
        {
            Compass_hand.eulerAngles = phoneController.AddGyro(previousEulerAngles, false, false, true);

            Vector3 rotation = Compass_hand.rotation.eulerAngles;
            float rotatedZ = rotation.z > 220 ? rotation.z - 360 : rotation.z;

            // Map rotation to position
            float t = Mathf.InverseLerp(-200, 220, rotatedZ);
            float targetPosX = Mathf.Lerp(-4.2f, 4.5f, t);

            // Move the background
            Vector3 backgroundPosition = background.transform.position;
            backgroundPosition.x = targetPosX;
            background.transform.position = backgroundPosition;
        }

    }
}
