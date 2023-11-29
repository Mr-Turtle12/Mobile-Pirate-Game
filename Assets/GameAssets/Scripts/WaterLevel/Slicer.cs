using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    private Vector3 pos; // Position

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 1)
            {
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                transform.position = new Vector3(pos.x, pos.y, 3);
                GetComponent<Collider2D>().enabled = true;
                return;
            }
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.position = new Vector3(pos.x, pos.y, 3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            pirateMovement enemy = other.GetComponent<pirateMovement>();
            if (enemy != null)
            {
                enemy.Hit();
            }
        }
    }
}
