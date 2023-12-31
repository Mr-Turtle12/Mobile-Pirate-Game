using System.Collections;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
    bool coolDownBool = false;
    private float cooldownTime = 1f;

    public float GetThrust(float Threshold)
    {
        // Get the accelerometer data
        Vector3 acceleration = Input.acceleration;

        // Check if the device is falling based on the y-axis acceleration
        if (acceleration.y < -Threshold && !coolDownBool)
        {
            StartCoroutine(CoolDown());
            return -acceleration.y;
        }
        return 0f;
    }
    public bool IsThrust(float Threshold)
    {
        if (GetThrust(Threshold) > 0)
        {
            return true;
        }
        return false;
    }
    IEnumerator CoolDown()
    {
        coolDownBool = true;
        yield return new WaitForSeconds(cooldownTime);
        Debug.Log("Can swing again");

        coolDownBool = false;
    }
    public void setCoolDownTime(float newTime)
    {
        cooldownTime = newTime;
    }
}
