using UnityEngine;

public class CompassController : MonoBehaviour, IMiniGamesController
{

    public Transform Goal_hand;
    public Transform Compass_hand;
    public GameObject newCompassAnimationPrefab;
    public GameObject background;
    public string NextScene;

    private GyroController phoneController;

    private int score;
    private float miniGameTime;
    private bool isRunning;


    //Interface functions
    public int GetScore()
    {
        return score * 40;
    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
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
    public void StartminiGame()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        score = 0;
        phoneController.EnableGyro();
    }
    //Done with Interface Functiom
    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            Vector3 previousEulerAngles = Compass_hand.eulerAngles;

            if (Mathf.Abs(previousEulerAngles.z - Goal_hand.rotation.eulerAngles.z) <= 2)
            {
                GameObject Animation = Instantiate(newCompassAnimationPrefab) as GameObject;
                float animationSpeed = (10f / miniGameTime) * 0.25f;
                Animation.GetComponent<Animator>().speed = 1 / animationSpeed * 1.25f;
                Destroy(Animation, animationSpeed);
                Goal_hand.GetComponent<RandomZPos>().ChangePos();
                score++;
            }
            Compass_hand.eulerAngles = phoneController.AddGyro(previousEulerAngles, false, false, true);

            UpdateBackground();
        }
    }

    void UpdateBackground()
    {
        Vector3 rotation = Compass_hand.rotation.eulerAngles;
        float rotatedZ = rotation.z > 220 ? rotation.z - 360 : rotation.z;

        // Map rotation to position
        float t = Mathf.InverseLerp(-200, 220, rotatedZ);
        float targetPosX = Mathf.Lerp(-4.2f, 4.5f, t);

        // Move the background
        Vector3 backgroundPosition = background.transform.position;
        backgroundPosition.x = targetPosX;
        background.transform.position = backgroundPosition;

    }

}
