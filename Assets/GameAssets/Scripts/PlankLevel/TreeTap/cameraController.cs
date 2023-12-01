using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private GameObject treePrefab; 
    private Vector3 initialCameraPosition;
    private Vector3 initialAxePosition;
    private float timeRatio;
    private float moveSpeed = 6.0f; 
    private float rotationTime = 0.2f; 
    private int clickCount = 0;
    private int counter = 1;
    private int switcher = 1;
    private GameObject background;
    private GameObject newBackground;
    private GameObject mainCamera;
    private Coroutine movementCoroutine;
    public CountdownController Starter;
    public treeMovement Tree;
    public Axe axe;

    void Start()
    {
        mainCamera = Camera.main.gameObject;
        timeRatio = 10.0f/Starter.miniGameTime;
        moveSpeed = Math.Max(timeRatio*moveSpeed, moveSpeed);
        rotationTime = Math.Min(rotationTime/timeRatio, rotationTime);
        initialCameraPosition = mainCamera.transform.position;
        initialAxePosition = axe.transform.position;
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score += 8;
    }
    void Update()
    {
        if (Starter.isRunning() && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider.gameObject.CompareTag("Tree"))
            {
                axe.RotateAxe(rotationTime);
                clickCount++;
                IncreaseScore();
                if (clickCount >= 3)
                {
                    TreeFell();
                    clickCount = 0;
                }
            }
        }
    }
    public void TreeFell()
    {
        Tree.Topple();
        SpawnTree();
        movementCoroutine = StartCoroutine(moveCoroutine(moveSpeed));
    }

    void SpawnTree()
    {
        GameObject newTree = Instantiate(treePrefab, GetSpawnPosition(), Quaternion.identity);
        treeMovement treeScript = newTree.GetComponent<treeMovement>();
        treeScript.SetController(this);
        Tree = treeScript;
    }

    void MoveBackground()
    {
        counter += 1;
        if (counter % 2 == 0)
        {
            switcher += 1;

            if (switcher % 2 == 0)
            {
                GameObject background = GameObject.FindWithTag("Background1");
                GameObject newBackground = GameObject.FindWithTag("Background2");
                Vector3 backgroundPosition = background.transform.position;
                newBackground.transform.position = new Vector3(backgroundPosition.x + 10.4f, backgroundPosition.y, backgroundPosition.z);
            }
            else
            {
                GameObject background = GameObject.FindWithTag("Background2");
                GameObject newBackground = GameObject.FindWithTag("Background1");
                Vector3 backgroundPosition = background.transform.position;
                newBackground.transform.position = new Vector3(backgroundPosition.x + 10.4f, backgroundPosition.y, backgroundPosition.z);
            }   
        }
    }
        
    private IEnumerator moveCoroutine(float moveSpeed)
    {
        float elapsedTime = 0.0f;
     
        while (elapsedTime < 1f)
        {
            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, initialCameraPosition + Vector3.right * 5.0f, elapsedTime);
            axe.transform.position = Vector3.Lerp(initialAxePosition, initialAxePosition + Vector3.right * 5.0f, elapsedTime);
            elapsedTime += Time.deltaTime * (moveSpeed/5);
            yield return null;
        }
        
        initialCameraPosition = mainCamera.transform.position;
        initialAxePosition = axe.transform.position;

        MoveBackground();

        GameObject trunk = GameObject.FindWithTag("Trunk");
        Destroy(trunk);
    }

    Vector3 GetSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        float screenRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane)).x;
        Vector3 spawnPosition = new Vector3(screenRightEdge + 2.5f, transform.position.y - 1.4f, transform.position.z + 10.0f);

        return spawnPosition;
    }
}


