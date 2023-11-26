using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyAway : MonoBehaviour
{
    public Sprite sprite1; // Assign the first sprite in the Unity Inspector
    public Sprite sprite2; // Assign the second sprite in the Unity Inspector
    public float changeInterval = 1.0f; // Change interval in seconds
    public float flySpeed = 1.0f; // Speed at which the sprite moves upward
    public float maxAngle = 45.0f; // Maximum angle in degrees for the initial flight direction
    public bool flyTime = false;
    private float Ythreshold = 8.0f;
    private SpriteRenderer spriteRenderer;
    private bool isSprite1 = true; // Start with the first sprite
    private float timeSinceLastChange;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the object.");
        }

        timeSinceLastChange = 0f;
        startPosition = transform.position;
    }

    public void OnFly()
    {
        if (!flyTime) 
        {
            flyTime = true;
            InvokeRepeating("ToggleSprite", 0f, changeInterval);
        }
    }

    void ToggleSprite()
    {
        // Toggle between the two sprites
        if (isSprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

        isSprite1 = !isSprite1; // Toggle the boolean

        // Update the start time and target position for smooth upward movement
        startTime = Time.time;
        targetPosition = transform.position + GetRandomUpwardDirection();

        // Move the sprite smoothly upwards
        StartCoroutine(MoveUpSmoothly());
    }

    Vector3 GetRandomUpwardDirection()
    {
        // Generate a random upward direction with a maximum angle
        float angle = Random.Range(0, maxAngle);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation * Vector3.up;
    }

    IEnumerator MoveUpSmoothly()
    {
        float journeyLength = Vector3.Distance(startPosition, targetPosition);

        while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            float distanceCovered = (Time.time - startTime) * flySpeed;
            float journeyFraction = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            yield return null;
        }

        startPosition = transform.position;

        // Check if the seagull is above the destroy threshold and destroy it
        if (transform.position.y > Ythreshold)
        {
            Destroy(gameObject);
        }
    }
}
