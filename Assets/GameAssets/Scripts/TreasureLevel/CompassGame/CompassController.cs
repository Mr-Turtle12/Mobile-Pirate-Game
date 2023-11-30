using UnityEngine;

public class CompassController : MonoBehaviour
{

    public Transform Goal_hand;
    public Transform Compass_hand;
    public GameObject newCompassAnimationPrefab;
    public GameObject background;

    private GyroController phoneController;

    private CountdownController Starter;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        phoneController = gameObject.AddComponent<GyroController>();
        Starter = GetComponent<CountdownController>();
        score = 0;
        phoneController.EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        if (Starter.isRunning())
        {
            Vector3 previousEulerAngles = Compass_hand.eulerAngles;

            if (Mathf.Abs(previousEulerAngles.z - Goal_hand.rotation.eulerAngles.z) <= 2)
            {
                GameObject Animation = Instantiate(newCompassAnimationPrefab) as GameObject;
                float animationSpeed = (10f / Starter.miniGameTime) * 0.25f;
                Animation.GetComponent<Animator>().speed = 1 / animationSpeed * 1.25f;
                Destroy(Animation, animationSpeed);
                Goal_hand.GetComponent<RandomZPos>().ChangePos();
                score++;
            }
            Compass_hand.eulerAngles = phoneController.AddGyro(previousEulerAngles, false, false, true);

            UpdateBackground();
        }
    }

    public int GetScore()
    {
        return score * 40;
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
