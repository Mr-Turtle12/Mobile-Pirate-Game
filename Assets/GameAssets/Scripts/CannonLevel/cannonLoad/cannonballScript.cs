using UnityEngine;

public class cannonballScript : MonoBehaviour
{
    public cannonballController controller;

    void Start()
    {
        if (controller == null)
        {
            controller = FindObjectOfType<cannonballController>();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            controller.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
