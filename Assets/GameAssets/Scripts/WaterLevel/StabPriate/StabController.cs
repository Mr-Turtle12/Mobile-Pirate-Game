using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabController : MonoBehaviour
{
    public float fallThreshold = 0.5f;
    public Animator animator;
    public GameObject PirateList;
    public GameObject Splatter;
    public string NextScene;

    private ThrustController thrushController;
    private GameObject[] Pirates;
    private GameObject CurrentPirates;

    private float total = 0;
    private int health = 2;
    private float miniGameTime;
    private float coolDownTime;
    private bool isRunning = false;
    private bool resetting = false;


    //Interface functions
    public int GetScore()
    {
        return (int)(total * 10);

    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        coolDownTime = 10f / miniGameTime / 2;
        Debug.Log(coolDownTime);
        thrushController.setCoolDownTime(coolDownTime);
        animator.speed = 1 / coolDownTime * 0.217f;
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
        Splatter.SetActive(false);
        Pirates = GetChildren(PirateList);
        SelectNewPirate();

    }


    void Update()
    {
        if (isRunning && !resetting)
        {
            float thrushValue = thrushController.GetThrust(fallThreshold);
            if (thrushValue > 0)
            {
                StartCoroutine(Blood());
                Debug.Log("move");
                total = total + thrushValue;
                animator.SetTrigger("Move");
                health--;
                //blood splatter
            }
            if (health <= 0)
            {
                health = 2;
                StartCoroutine(restartPirate());

            }
        }
    }

    IEnumerator restartPirate()
    {
        resetting = true;
        Animator animator = CurrentPirates.GetComponent<Animator>();
        animator.SetTrigger("Death");
        animator.speed = 1 / coolDownTime * 0.5f;
        yield return new WaitForSeconds(1 / coolDownTime * 0.5f);

        CurrentPirates.SetActive(false);
        SelectNewPirate();
        resetting = false;

    }
    IEnumerator Blood()
    {
        Splatter.SetActive(true);
        yield return new WaitForSeconds(coolDownTime / 2);
        Splatter.SetActive(false);

    }

    private void SelectNewPirate()
    {
        int index = Random.Range(0, Pirates.Length);
        CurrentPirates = Pirates[index];
        CurrentPirates.transform.localPosition = new Vector3(-2.780523f, -1.62f, 0.02240489f);
        CurrentPirates.transform.localRotation = new Quaternion(0, 0, 0, 0);
        CurrentPirates.SetActive(true);
    }

    private GameObject[] GetChildren(GameObject PirateList)
    {
        Transform parentTransform = PirateList.transform;

        GameObject[] tempPirates = new GameObject[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            childTransform.gameObject.SetActive(false);

            tempPirates[i] = childTransform.gameObject;
        }
        return tempPirates;
    }

}

