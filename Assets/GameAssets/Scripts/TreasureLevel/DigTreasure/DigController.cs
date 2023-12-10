using UnityEngine;
using UnityEngine.UIElements;

public class DigController : MonoBehaviour, IMiniGamesController
{
    public float fallThreshold = 0.5f;
    public float numOfThrustValue = 6f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject spot;
    public GameObject TreasurePrefab;
    public string NextScene;

    private ThrustController thrushController;
    private float total = 0;
    private int holesDug = 0;
    private float miniGameTime;
    private bool isRunning = false;

    //Interface functions
    public int GetScore()
    {
        return (int)(total * 10);

    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        float coolDownTime = 10f / miniGameTime / 2;
        thrushController.setCoolDownTime(coolDownTime);
        animator.speed = 1 / coolDownTime * 0.138f;
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
    void Start()
    {
        thrushController = gameObject.AddComponent<ThrustController>();

    }


    void Update()
    {
        if (isRunning)
        {
            float thrushValue = thrushController.GetThrust(fallThreshold);
            if (thrushValue > 0)
            {
                spot.SetActive(false);
                total = total + thrushValue;
                animator.SetTrigger("Move");
                float OnHoleValue = (total - (holesDug * numOfThrustValue)) / numOfThrustValue;
                setColour(OnHoleValue);
            }
            if (spriteRenderer.color.a > 1)
            {
                restartHole();
            }
        }
    }
    void setColour(float value)
    {
        Color newColour = spriteRenderer.color;
        newColour.a = value;
        spriteRenderer.color = newColour;
    }

    void restartHole()
    {
        //Spawn treasure
        GameObject Animation = Instantiate(TreasurePrefab) as GameObject;
        float animationSpeed = 5 / miniGameTime;
        Animation.GetComponent<Animator>().speed = animationSpeed;
        Destroy(Animation, animationSpeed);

        //restart hole
        spot.SetActive(true);
        setColour(0);
        holesDug++;
    }

}

