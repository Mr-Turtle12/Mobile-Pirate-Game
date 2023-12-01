using UnityEngine;

public class treeMovement : MonoBehaviour
{
    [SerializeField] private GameObject trunkPrefab; 
    [SerializeField] private GameObject topPrefab;  
    [SerializeField] public cameraController controller;
    private Vector3 originalTrunkPosition; 
    private float trunkHeightOffset; 

    void Start()
    {
        originalTrunkPosition = transform.position;
        SpriteRenderer fullTreeRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer trunkRenderer = trunkPrefab.GetComponent<SpriteRenderer>();
        trunkHeightOffset = (fullTreeRenderer.bounds.size.y - trunkRenderer.bounds.size.y) / 2f;
    }

    public void SetController(cameraController _controller)
    {
        controller = _controller;
    }

    public void Topple()
    {
        GameObject trunk = Instantiate(trunkPrefab, originalTrunkPosition - new Vector3(0f, trunkHeightOffset, 0f), Quaternion.identity);
        GameObject top = Instantiate(topPrefab, transform.position, Quaternion.identity);
        ApplyFallingAnimation(top);
        Destroy(gameObject);
    }
    
    void ApplyFallingAnimation(GameObject treeTop)
    {
        Rigidbody2D rb = treeTop.GetComponent<Rigidbody2D>();
        rb.AddTorque(Random.Range(-4f, 4f), ForceMode2D.Impulse);
        Destroy(treeTop, 1f);
    }
}

