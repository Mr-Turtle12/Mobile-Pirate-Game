using UnityEngine;

public class pirateController : MonoBehaviour, IMiniGamesController
{
    private int score = 0;
    [SerializeField] private GameObject piratePrefab;
    public string NextScene;
    private float nextSpawnTime = 0f;
    private float spawnInterval = 1.8f;
    private float miniGameTime;
    private bool isRunning = false;

    //Interface functions

    public void StartminiGame()
    {

    }
    public int GetScore()
    {
        return score;

    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        spawnInterval /= (10.0f / miniGameTime);

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
    //Done with Interface Functions


    public void IncreaseScore()
    {
        score += 40;
    }
    public float getGameLength()
    {
        return miniGameTime;
    }

    void Update()
    {
        if (isRunning && Time.time >= nextSpawnTime)
        {
            SpawnPirate();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPirate()
    {
        GameObject newPirate = Instantiate(piratePrefab, GetRandomSpawnPosition(), Quaternion.identity);
        pirateMovement pirateScriptComponent = newPirate.GetComponent<pirateMovement>();
        pirateScriptComponent.SetController(this);
        pirateScriptComponent.OnHit += IncreaseScore;
    }

    Vector3 GetRandomSpawnPosition()
    {
        float spawnX = UnityEngine.Random.Range(-1f, 2.5f);
        float spawnY = 9f;
        return new Vector3(spawnX, spawnY, 0);
    }
}
