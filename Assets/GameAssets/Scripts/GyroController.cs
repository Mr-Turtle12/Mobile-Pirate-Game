using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool metGoal = false;
    private Gyroscope gyro;
    public GameObject GoalPos;

    private void Start()
    {
        EnableGyro();
    }

    private void EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }

    }

    private void Update()
    {
        Vector3 previousEulerAngles = transform.eulerAngles;
        if (Mathf.Abs(previousEulerAngles.z - GoalPos.transform.rotation.eulerAngles.z) <= 10)
        {
            metGoal = true;
        }
        if (!metGoal)
        {
            Vector3 gyroInput = Input.gyro.rotationRateUnbiased;

            Vector3 targetEulerAngles = previousEulerAngles + gyroInput * Time.deltaTime * Mathf.Rad2Deg;
            targetEulerAngles.y = 0.0f; // set y to 0 to ensure it only rotates around the y-axis
            targetEulerAngles.x = 0.0f; // set z to 0 to ensure it only rotates around the y-axis

            transform.eulerAngles = targetEulerAngles;
        }
    }
}
