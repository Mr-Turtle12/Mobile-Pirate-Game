using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZPos : MonoBehaviour
{


    void Start()
    {
        ChangePos();
    }

    public void ChangePos()
    {
        float randomZRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomZRotation);
    }
}
