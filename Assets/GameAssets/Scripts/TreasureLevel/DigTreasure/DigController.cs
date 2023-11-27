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

    void Start()
    {
        thrushController = gameObject.AddComponent<ThrustController>();
        countdown = gameObject.GetComponent<CountdownController>();
        thrushController.setCoolDownTime(coolDownTime);
        animator.speed = coolDownTime;
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (countdown.start)
        {
            float thrushValue = thrushController.GetThrush(fallThreshold);
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
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(gameTime);
        Color newColor = spriteRenderer.color;
        Debug.Log(newColor);
        Debug.Log("Coin found:" + newColor.a * 200);
        countdown.endGame();
    }

}

