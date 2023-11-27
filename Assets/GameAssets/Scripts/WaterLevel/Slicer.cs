using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    private Vector3 pos; // Position

    void Start()
    {
        // Set sleep timeout to never
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        // Y axis = 0.3
        // Red dot 4

        // If the game is running on an iPhone device
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // If we are touching the screen
            if (Input.touchCount == 1)
            {
                // Find screen touch position by transforming position from screen space into game world space.
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                // Set the position of the player object
                transform.position = new Vector3(pos.x, pos.y, 3);
                // Set collider to true
                GetComponent<Collider2D>().enabled = true;
                return;
            }
            // Set collider to false
            GetComponent<Collider2D>().enabled = false;
        }
        // If the game is not running on an iPhone device
        else
        {
            // Find mouse position
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            // Set position
            transform.position = new Vector3(pos.x, pos.y, 3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            pirateScript enemy = other.GetComponent<pirateScript>();
            if (enemy != null)
            {
                enemy.Hit();
            }
        }
    }
}
