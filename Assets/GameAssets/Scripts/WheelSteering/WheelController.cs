using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject Wheel;
    public GameObject Wind;
    private GyroController phoneController;
    private bool applyWind;

    private IEnumerator ApplyWindForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            applyWind = true;

            yield return new WaitForSeconds(0.5f);

            applyWind = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        phoneController.EnableGyro();
        applyWind = false;
        StartCoroutine(ApplyWindForce());
    }

    void ChangeCourse()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, Random.Range(-90f, 90f));

        // Gradually change the wheel angle over half a second.
        Quaternion startRotation = Wheel.transform.rotation;
        float startTime = Time.time;

        while (Time.time - startTime < 0.5f)
        {
            float t = (Time.time - startTime) / 0.5f;
            Wheel.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
        }

        // Ensure the final rotation is the target rotation.
        Wheel.transform.rotation = targetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (applyWind)
        {
            GameObject windObject;
            if (Random.Range(0, 100) >= 50)
            {
                windObject = Instantiate(Wind, new Vector3(0, 0, 0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            else
            {
                windObject = Instantiate(Wind, new Vector3(0, 0, 0f), Quaternion.Euler(0, 0, 180f));
            }
            ChangeCourse();
            Debug.Log("hello");
            Destroy(windObject);
        }
        else
        {
            Wheel.transform.eulerAngles = phoneController.AddGyro(Wheel.transform.eulerAngles, false, false, true);
        }
    }

}
