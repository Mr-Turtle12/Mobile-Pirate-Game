using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class cannonballController : MonoBehaviour , IMiniGamesController
{      
    private int score = 0;
    public string NextScene;
    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private Transform background;
    private float timeRatio;
    private float dragSpeed = 5f;
    private bool isDragging = false;
    private bool isRunning = false;
    private float miniGameTime;
    private GameObject currentCannonball;

    public int GetScore()
    {
        return score;
    }

    public void SetDuration(float time)
    {
        miniGameTime = time;
        timeRatio = 10.0f / miniGameTime;
        dragSpeed = Math.Max(timeRatio * dragSpeed, dragSpeed);
    }

    public void IsRunning(bool running)
    {
        isRunning = running;
    }

    public bool GameRunning()
    {
        return isRunning;
    }

    public string getNextScene()
    {
        return NextScene;
    }
    public void StartminiGame()
    {

    }
    //Done with Interface Functions
    public void IncreaseScore()
    {
        score += 20;
    }

    void Update()
    {
        if (isRunning && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegin(touch.position);
                    break;

                case TouchPhase.Moved:
                    HandleTouchMove(touch.position);
                    break;

                case TouchPhase.Ended:
                    HandleTouchEnd();
                    break;
            }
        }
    }

    void HandleTouchBegin(Vector3 touchPosition)
    {
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Cannons"))
        {
            currentCannonball = Instantiate(cannonballPrefab, touchWorldPosition, Quaternion.identity);
            isDragging = true;
        }
    }

    void HandleTouchMove(Vector3 touchPosition)
    {
        float desiredZPosition = 0f;
        if (isDragging && currentCannonball != null)
        {
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            Vector3 cannonballPosition = new Vector3(touchWorldPosition.x, touchWorldPosition.y, desiredZPosition);
            currentCannonball.transform.position = cannonballPosition;
        }
    }

    void HandleTouchEnd()
    {
        isDragging = false;

        if (currentCannonball != null)
        {
            Destroy(currentCannonball);
        }
    }
}
