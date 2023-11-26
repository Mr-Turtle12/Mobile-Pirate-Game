using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirateScript : MonoBehaviour
{
    public Sprite winSprite;
    public float moveSpeed = 3.0f;
    public float growthRate = 0.05f;
    private bool canBeDead; // If we can destroy the object
    private bool sliced = false;
    private Vector3 screen; // Position on the screen
    private bool isMoving = false;

    void Update()
    {
        // Set screen position
        screen = Camera.main.WorldToScreenPoint(transform.position);
        
        // If we can die and are not on the screen
        if (canBeDead && screen.y < -20)
        {
            // Destroy
            Destroy(gameObject);
        }
        // If we can't die and are on the screen for the fruit to appear on screen the first time
        else if (!canBeDead && screen.y > -10)
        {
            // We can die
            canBeDead = true;
        }
        // Lower the object slowly
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= -0.5f && screenPoint.y <= 1;

        if (!sliced)
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            if (onScreen)
            {
                Vector3 newScale = transform.localScale + Vector3.one * growthRate * Time.deltaTime;
                transform.localScale = newScale;
            }
        }
    }

    public void Hit()
    {
        // Change Z dimension to 2
        sliced = true;
        Vector3 newPosition = transform.position;
        newPosition.z = 2.0f;
        transform.position = newPosition;

        // Change the Rigidbody2D to dynamic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // Remove collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Destroy(collider);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            sliced = true;
            Debug.Log("Collided");
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && winSprite != null)
            {
                spriteRenderer.sprite = winSprite;
            }
            // Add horizontal movement (to the left)
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 movementDirection = new Vector2(-moveSpeed, -moveSpeed); // Adjust as needed
                rb.velocity = movementDirection;
            }
        }
    }
}
