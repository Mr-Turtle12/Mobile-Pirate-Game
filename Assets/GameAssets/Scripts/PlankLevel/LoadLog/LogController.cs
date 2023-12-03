using UnityEngine;

public class LogController : MonoBehaviour, IMiniGamesController
{
    public GameObject Log;
    public float gravityScale = 1f;
    public string NextScene;

    private GameObject CurrentLog;
    private GyroController phoneController;
    private bool CreateNewCrete;
    private int LandedSafely = 0;
    private float miniGameTime;
    private bool isRunning = false;

    //Interface functions
    public int GetScore()
    {
        return LandedSafely * 24;

    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        gravityScale = 10f / miniGameTime * gravityScale;

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

    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        phoneController.EnableGyro();
        CreateNewCrete = true;

    }

    public void SafelyLandedCrate()
    {
        LandedSafely++;
        CreateNewCrete = true;

    }
    public void NotSafelyLandedCrate()
    {
        CreateNewCrete = true;

    }
    private void CreateCrate()
    {
        float[] numbers = { 1.56f, 0.02f, -1.62f };
        int randomIndex = Random.Range(0, numbers.Length);
        CurrentLog = Instantiate(Log, new Vector3(numbers[randomIndex], 5.87f, 0f), Quaternion.Euler(0, 0, Random.Range(-90f, 90f))) as GameObject;
        CurrentLog.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (CreateNewCrete)
            {
                CreateCrate();
                CreateNewCrete = false;
            }
            CurrentLog.transform.eulerAngles = phoneController.AddGyro(CurrentLog.transform.eulerAngles, false, false, true);
        }


    }

}
