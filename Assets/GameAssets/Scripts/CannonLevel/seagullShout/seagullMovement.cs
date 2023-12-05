using System.Collections;
using UnityEngine;

public class seagullMovement : MonoBehaviour
{
    private bool hasFlown = false;
    private Vector3 initialPosition;
    private Coroutine flyingCoroutine;
    private Coroutine spawnCoroutine;
    [SerializeField] private seagullController controller;

    public void FlyAway(float flySpeed)
    {
        if (!hasFlown)
        {
            flyingCoroutine = StartCoroutine(FlyUpCoroutine(flySpeed));
        }
    }

    private IEnumerator FlyUpCoroutine(float flySpeed)
    {
        hasFlown = true;
        controller.IncreaseScore();
        initialPosition = transform.position;
        while (transform.position.y < 7.0f)
        {
            Vector3 targetLocation = Quaternion.Euler(0, 0, Random.Range(-5.0f, 55.0f)) * Vector3.up;
            transform.Translate(targetLocation * flySpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector3(initialPosition.x + 5.0f, initialPosition.y + 2.0f, initialPosition.z);
        spawnCoroutine = StartCoroutine(SpawnInCoroutine(initialPosition, flySpeed));
    }

    private IEnumerator SpawnInCoroutine(Vector3 targetPosition, float flySpeed)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * (flySpeed/5);
            yield return null;
        }
        transform.position = targetPosition;
        hasFlown = false;
    }
}
