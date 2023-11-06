using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool metGoal = false;
    private Gyroscope gyro;
    public GameObject GoalPos;
    public GameObject background;

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
            Vector3 rotation = transform.rotation.eulerAngles;
            float rotatedZ = rotation.z > 220 ? rotation.z - 360 : rotation.z;
            // Map rotation to position
            float t = Mathf.InverseLerp(-200, 220, rotatedZ);
            Debug.Log(rotatedZ);
            Debug.Log(t);
            float targetPosX = Mathf.Lerp(-4.2f, 4.5f, t);

            // Move the background
            Vector3 backgroundPosition = background.transform.position;
            backgroundPosition.x = targetPosX;
            background.transform.position = backgroundPosition;
        }
    }
}
