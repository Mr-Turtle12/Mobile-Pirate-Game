using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Quaternion originalRotation;
    private bool isRotating = false;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    public void RotateAxe(float rotationTime)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateAxeCoroutine(rotationTime));
        }
    }

    private IEnumerator RotateAxeCoroutine(float rotationTime)
    {
        isRotating = true;

        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, 50f);

        float elapsedRotationTime = 0f;
        while (elapsedRotationTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedRotationTime / rotationTime);
            elapsedRotationTime += Time.deltaTime;
            yield return null;
        }

        float resetTime = rotationTime / 4.0f;
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

