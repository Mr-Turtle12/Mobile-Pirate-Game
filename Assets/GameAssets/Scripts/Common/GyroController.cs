using UnityEngine;

public class GyroController : MonoBehaviour
{
    private Gyroscope gyro;

    public void EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }

    }
    public Vector3 Getgyro()
    {
        Vector3 gyroInput = Input.gyro.rotationRateUnbiased;
        return gyroInput;
    }
    //Add Gyro rotation to an Vector 3  only applying if x,y,z vars are true
    public Vector3 AddGyro(Vector3 previousAngle, bool x, bool y, bool z)
    {
        Vector3 targetEulerAngles = previousAngle + Getgyro() * Time.deltaTime * Mathf.Rad2Deg;
        if (!x)
        {
            targetEulerAngles.x = 0.0f;
        }
        if (!y)
        {
            targetEulerAngles.y = 0.0f;
        }
        if (!z)
        {
            targetEulerAngles.z = 0.0f;
        }
        return targetEulerAngles;
    }
}

