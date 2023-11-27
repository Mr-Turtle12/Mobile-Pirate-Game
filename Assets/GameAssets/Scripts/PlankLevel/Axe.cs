using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Quaternion originalRotation;
    private bool isRotating = false;

    void Start()
    {
        // Record the original rotation at the start
        originalRotation = transform.rotation;
    }

    public void RotateAxe()
    {
        if (!isRotating)
        {
            // Rotate the axe quickly in one direction
            StartCoroutine(RotateAxeCoroutine());
        }
    }

    System.Collections.IEnumerator RotateAxeCoroutine()
    {
        isRotating = true;

        // Rotate quickly in one direction
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, 50f);
        float rotationTime = 0.2f; // Adjust the time as needed

        float elapsedRotationTime = 0f;
        while (elapsedRotationTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedRotationTime / rotationTime);
            elapsedRotationTime += Time.deltaTime;
            yield return null;
        }

        // Reset the axe quickly to its starting position
        float resetTime = 0.05f; // Adjust the time as needed
        float elapsedResetTime = 0f;

        while (elapsedResetTime < resetTime)
        {
            transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, elapsedResetTime / resetTime);
            elapsedResetTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation;
        isRotating = false;
    }
}

