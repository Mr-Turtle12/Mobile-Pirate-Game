using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public GameObject Log;
    public int maxcount = 2;
    private GameObject CurrentLog;
    private GyroController phoneController;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        phoneController.EnableGyro();
        count = 0;

        CreateCrate();
    }

    private void CreateCrate()
    {
        float[] numbers = { 1.56f, 0.02f, -1.62f };
        float[] angles = { -68f, -33f, -21f, 19f, 50f, 62f };
        int randomAngle = Random.Range(0, angles.Length);
        int randomIndex = Random.Range(0, numbers.Length);
        Debug.Log(angles[randomAngle]);
        CurrentLog = Instantiate(Log, new Vector3(numbers[randomIndex], 5.87f, 0f), Quaternion.Euler(0, 0, Random.Range(-90f, 90f))) as GameObject;
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLog.GetComponent<boxCollides>().landed)
        {
            if (count <= maxcount)
            {
                CreateCrate();
            }
            else
            {
                CurrentLog = Instantiate(Log, new Vector3(-1000f, -1000f, 0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

        CurrentLog.transform.eulerAngles = phoneController.AddGyro(CurrentLog.transform.eulerAngles, false, false, true);


    }

}
