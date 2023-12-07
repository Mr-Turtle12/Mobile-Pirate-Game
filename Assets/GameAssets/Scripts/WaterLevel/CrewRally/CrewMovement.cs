using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMovement : MonoBehaviour
{
    private bool hasMoved = false;
    private Vector3 initialPosition;
    public Vector3 FinalPosition;
    public Transform FinalGroupPos;
    [SerializeField] private CrewController controller;

    public void Start()
    {
        initialPosition = transform.position;
    }
    public void WalkIn(float moveSpeed)
    {
        if (!hasMoved)
        {
            StartCoroutine(MoveIntoPostion(moveSpeed));
        }
    }

    private IEnumerator MoveIntoPostion(float moveSpeed)
    {
        hasMoved = true;
        controller.IncreaseScore();
        transform.position = initialPosition;
        Debug.Log(FinalPosition);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(initialPosition, FinalPosition, elapsedTime);
            elapsedTime += Time.deltaTime * (moveSpeed / 5);
            yield return null;
        }
        transform.position = FinalPosition;
        yield return new WaitForSeconds(moveSpeed / 10);

        StartCoroutine(SpawnInCoroutine(moveSpeed));
    }

    private IEnumerator SpawnInCoroutine(float moveSpeed)
    {
        controller.startedPirateRallying();
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(FinalPosition, FinalGroupPos.localPosition, elapsedTime);
            elapsedTime += Time.deltaTime * (moveSpeed / 5);
            yield return null;
        }
        transform.position = initialPosition;
        hasMoved = false;
        controller.pirateRallied();
    }
}
