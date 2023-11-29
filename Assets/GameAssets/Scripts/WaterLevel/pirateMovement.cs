using System.Collections;
using System;
using UnityEngine;

public class pirateMovement : MonoBehaviour
{
    [SerializeField] private Sprite pirateWin;
    [SerializeField] private Sprite pirateLose;
    private float timeRatio;
    private float moveSpeed = 3.0f;
    private float growthRate = 0.05f;
    private bool sliced = false;
    public event Action OnHit;
    public pirateController controller;

    public void Start()
    {
        timeRatio = 10.0f/controller.Starter.miniGameTime;
        moveSpeed = timeRatio*moveSpeed;
        growthRate = timeRatio*growthRate;
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (!sliced)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    public void SetController(pirateController _controller)
    {
        controller = _controller;
    }

    public void Hit()
    {
        OnHit?.Invoke();
        sliced = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pirateLose;
        Vector3 newPosition = transform.position;
        newPosition.z = 3.0f;
        transform.position = newPosition;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        Collider2D collider = GetComponent<Collider2D>();
        Destroy(collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            sliced = true;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = pirateWin;
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(collider);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 movementDirection = new Vector2(-moveSpeed, -moveSpeed); // Adjust as needed
            rb.velocity = movementDirection;
        }
    }
}
