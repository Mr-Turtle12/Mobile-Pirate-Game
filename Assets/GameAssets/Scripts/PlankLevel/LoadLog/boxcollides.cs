using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCollides : MonoBehaviour
{
    public bool landed = false;
    private LogController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<LogController>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crate")
        {
            landed = true;
            //stick if angle is smaller then 15 or bigger then 75
            if ((gameObject.transform.eulerAngles.z % 90) <= 5 || (gameObject.transform.eulerAngles.z % 90) >= 85)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                controller.increaseLandedSafely();

            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 300f));
            }
            landed = true;


        }
    }
}
