using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : MonoBehaviour
{
    public GameObject piratePrefab; // Assign your pirate prefab in the Unity Editor
    public Slicer playerSlicer;
    public int score = 0;

    // Adjust these values as needed
    public float spawnInterval = 2f;
    private float nextSpawnTime = 0f;
    public CountdownController Starter;

    void Update()
    {
            // Check if it's time to spawn a new pirate
            if (Time.time >= nextSpawnTime)
            {
                SpawnPirate();
                nextSpawnTime = Time.time + spawnInterval;
            }
    }

    void SpawnPirate()
    {
        if (Starter.start)
        {
            // Instantiate a new pirate from the prefab
            GameObject newPirate = Instantiate(piratePrefab, GetRandomSpawnPosition(), Quaternion.identity);
            
            // Set the GameController as the parent to manage hierarchy
            newPirate.transform.parent = transform;
            
            // Get reference to the pirateScript component of the newly spawned pirate
            pirateScript pirateScriptComponent = newPirate.GetComponent<pirateScript>();
            
            // Set the CountdownController reference in the pirateScript
            pirateScriptComponent.Starter = GetComponent<CountdownController>();
            
            // Subscribe to the pirate's Hit event to update the score
            pirateScriptComponent.OnHit += UpdateScore;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Adjust these values based on your game's needs
        float spawnX = Random.Range(-1f, 2.5f);
        float spawnY = 9f;
        return new Vector3(spawnX, spawnY, 0);
    }
    
    void UpdateScore()
    {
        // Called when a pirate is sliced
        score++;
        Debug.Log("Score: " + score);

        if (score == 3)
        {
            StartCoroutine(EndGameAfterDelay());
        }
    }

    IEnumerator EndGameAfterDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);

        // Call the endGame method after the delay
        Starter.endGame();
    }
}
