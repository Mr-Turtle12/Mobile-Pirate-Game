using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Transform[] objectLocations; // Array of allowed positions
    public GameObject[] GameIntro;
    public int[] indexValues;
    private int currentLocationIndex = 0; // Initial position index
    public float moveSpeed = 5f; // Adjust this value to control the movement speed
    public float wobbleAmount = 0.1f; // Adjust this value to control the wobbling
    private bool isStationary = true;
    private IDataCarrierScript CarrierScript;
    private int maxIndex;

    public int GetCurrentLocationIndex()
    {
        return currentLocationIndex;
    }
    void Start()
    {
        transform.position = new Vector3(objectLocations[currentLocationIndex].position.x, objectLocations[currentLocationIndex].position.y, -1f);
        foreach (var Intro in GameIntro)
        {
            Intro.SetActive(false);
        }
        GameIntro[currentLocationIndex].SetActive(true);
        GameObject DataCarrier = GameObject.Find("DataCarrier");
        CarrierScript = DataCarrier.GetComponent<IDataCarrierScript>();
        switch (CarrierScript.GetGameMode())
        {
            case GameMode.FreePlay:
                maxIndex = indexValues[objectLocations.Length - 1];
                break;
            case GameMode.Campaign when CarrierScript is CampaignDataCarrier campaignCarrier:
                maxIndex = campaignCarrier.getUnlockedMiniGames();
                break;
        }

        for (int i = 0; i < objectLocations.Length; i++)
        {
            if (indexValues[i] > maxIndex)
            {
                objectLocations[i].GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0.5f);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isStationary)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePosition.x > -0.11f && mousePosition.y < -4.16f)
            {
                return;
            }
            // Check if the click is above or below the current position
            if (mousePosition.y > transform.position.y && currentLocationIndex > 0)
            {
                isStationary = false;
                GameIntro[currentLocationIndex].SetActive(false);
                MoveToLocation(currentLocationIndex - 1);
            }
            else if (mousePosition.y < transform.position.y && indexValues[currentLocationIndex] < maxIndex)
            {
                isStationary = false;
                GameIntro[currentLocationIndex].SetActive(false);
                MoveToLocation(currentLocationIndex + 1);
            }
        }
    }

    private void MoveToLocation(int newIndex)
    {
        StopAllCoroutines(); // Stop any ongoing movement coroutine

        StartCoroutine(MoveCoroutine(new Vector3(objectLocations[newIndex].position.x, objectLocations[newIndex].position.y, -1f)));
        currentLocationIndex = newIndex;
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        if (startPosition.x < targetPosition.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;

            // Use Lerp to smoothly interpolate between the current position and the target position
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);

            // Add wobble effect
            float wobble = Mathf.Sin(elapsedTime * Mathf.PI * 2f) * wobbleAmount;
            transform.rotation = Quaternion.Euler(0f, 0f, wobble);

            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;
        isStationary = true;
        GameIntro[currentLocationIndex].SetActive(true);

    }
}

