using UnityEngine;
using UnityEngine.UIElements;

public class DigController : MonoBehaviour
{
    public float fallThreshold = 0.5f;
    public float numOfThrustValue = 6f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject spot;
    public GameObject TreasurePrefab;

    private ThrustController thrushController;
    private CountdownController countdown;
    private float total = 0;
    private int holesDug = 0;

    void Start()
    {
        thrushController = gameObject.AddComponent<ThrustController>();
        countdown = gameObject.GetComponent<CountdownController>();
        float coolDownTime = (10f / countdown.miniGameTime) / 2;
        thrushController.setCoolDownTime(coolDownTime);
        animator.speed = 1 / coolDownTime * 0.138f;
    }

    public int GetScore()
    {
        return (int)(total * 10);
    }

    void Update()
    {
        if (countdown.isRunning())
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
        float animationSpeed = 5 / countdown.miniGameTime;
        Animation.GetComponent<Animator>().speed = animationSpeed;
        Destroy(Animation, animationSpeed);

        //restart hole
        spot.SetActive(true);
        setColour(0);
        holesDug++;
    }

}

