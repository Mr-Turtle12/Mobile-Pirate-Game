using UnityEngine;

public class ClickCount : MonoBehaviour
{
    public GameObject trunkPrefab; // Drag and drop the prefab for the trunk in the Inspector
    public GameObject topPrefab;   // Drag and drop the prefab for the top of the tree in the Inspector

    private int clickCount;
    private Vector2 originalTrunkPosition; // Store the original trunk position
    private float trunkHeightOffset; // Store the height offset for trunk spawning
    public Axe axe;
    public CameraMovement cameraMovement;
    public CountdownController Starter;

    void Start()
    {
        clickCount = 0;

        // Record the original trunk position at the start
        originalTrunkPosition = transform.position;

        // Calculate the height offset for trunk spawning
        SpriteRenderer fullTreeRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer trunkRenderer = trunkPrefab.GetComponent<SpriteRenderer>();

        if (fullTreeRenderer != null && trunkRenderer != null)
        {
            trunkHeightOffset = (fullTreeRenderer.bounds.size.y - trunkRenderer.bounds.size.y) / 2f;
        }
    }

    void Update()
    {
        // Check for mouse click in 2D
        if (Starter.start && Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world point
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast to detect if the mouse click hits this object in 2D
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Object is clicked
                RotateAxe();
                clickCount++;

                // Check if the click count is 3, and fell the tree
                if (clickCount >= 3)
                {
                    FellTree();
                }
            }
        }
    }

    void RotateAxe()
    {
        if (axe != null)
        {
            axe.RotateAxe();
        }
    }

    void FellTree()
    {
        // Instantiate the trunk and top of the tree
        GameObject trunk = Instantiate(trunkPrefab, originalTrunkPosition - new Vector2(0f, trunkHeightOffset), Quaternion.identity);
        GameObject top = Instantiate(topPrefab, transform.position, Quaternion.identity);

        // Apply falling animation to the top of the tree
        ApplyFallingAnimation(top);

        // Destroy the original tree
        Destroy(gameObject);

        // Call TreeCutDown method to trigger camera and axe movement
        if (cameraMovement != null)
        {
            cameraMovement.TreeCutDown();
        }
    }

    void ApplyFallingAnimation(GameObject treeTop)
    {
        // Ensure the object has a Rigidbody2D component
        Rigidbody2D rb = treeTop.GetComponent<Rigidbody2D>();

        // Apply rotation as the top of the tree rotates about its base
        rb.AddTorque(Random.Range(-4f, 4f), ForceMode2D.Impulse);

        // Set a callback to destroy the object after 1 second
        Destroy(treeTop, 1f);
    }
}

