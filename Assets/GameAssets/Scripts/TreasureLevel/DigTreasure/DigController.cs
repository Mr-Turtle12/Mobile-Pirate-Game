using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigController : MonoBehaviour
{
    public float fallThreshold = 0.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject spot;
    public float gameTime = 5f;
    public float coolDownTime = 0.5f;

    private ThrustController thrushController;
    private CountdownController countdown;
    private float total = 0;
    private bool stopped = true;

    void Start()
    {
        thrushController = gameObject.AddComponent<ThrustController>();
        countdown = gameObject.GetComponent<CountdownController>();
        thrushController.setCoolDownTime(coolDownTime);
        animator.speed = coolDownTime;
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
                Debug.Log(total);


                Color newColor = spriteRenderer.color;
                newColor.a = total / 10;
                spriteRenderer.color = newColor;
            }
        }
        else if (stopped)
        {
            stopped = true;
            Color newColor = spriteRenderer.color;
            Debug.Log(newColor);
            Debug.Log("Coin found:" + newColor.a * 200);
        }
    }

}

