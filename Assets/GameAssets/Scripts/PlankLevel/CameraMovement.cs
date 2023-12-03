using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed as needed
    public float movementDuration = 2f; // Adjust the duration of movement
    public float cooldownDuration = 2f; // Adjust the cooldown duration

    private bool treeCut = false;
    private GameObject mainCamera;
    private GameObject axe;

    void Start()
    {
        mainCamera = Camera.main.gameObject;
        axe = GameObject.FindWithTag("Axe"); // Assuming the Axe object has a tag "Axe"
    }

    void Update()
    {
        if (treeCut)
        {
            MoveObjectsToRight();
        }
    }

    public void TreeCutDown()
    {
        if (!treeCut)
        {
            treeCut = true;
            Invoke("ResetTreeCutFlag", movementDuration + cooldownDuration);
            Invoke("StopMovement", movementDuration);

        }
    }

    void MoveObjectsToRight()
    {
        // Move only in the horizontal direction
        mainCamera.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        axe.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }

    void ResetTreeCutFlag()
    {
        treeCut = false;
    }

    void StopMovement()
    {
        mainCamera.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        axe.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}


