using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public GameObject Log;
    public int maxcount = 2;
    private GameObject CurrentLog;
    private GyroController phoneController;
    private bool CreateNewCrete;
    private int count;
    public CountdownController Starter;
    private int LandedSafely = 0;

    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        phoneController.EnableGyro();
        count = 0;
        CreateNewCrete = true;
    }

    public void increaseLandedSafely()
    {
        LandedSafely++;
    }
    private void CreateCrate()
    {
        float[] numbers = { 1.56f, 0.02f, -1.62f };
        float[] angles = { -68f, -33f, -21f, 19f, 50f, 62f };
        int randomIndex = Random.Range(0, numbers.Length);
        CurrentLog = Instantiate(Log, new Vector3(numbers[randomIndex], 5.87f, 0f), Quaternion.Euler(0, 0, Random.Range(-90f, 90f))) as GameObject;
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Starter.start)
        {
            if (CreateNewCrete)
            {
                CreateCrate();
                CreateNewCrete = false;
            }

            if (CurrentLog.GetComponent<boxCollides>().landed)
            {
                if (count <= maxcount)
                {
                    CreateNewCrete = true;
                }
                else
                {
                    Debug.Log(LandedSafely);
                    Starter.endGame();
                }
            }

            CurrentLog.transform.eulerAngles = phoneController.AddGyro(CurrentLog.transform.eulerAngles, false, false, true);
        }


    }

}
