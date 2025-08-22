using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < -0.01f)
        {
            spriteRenderer.flipX=true;
        }
    }
}
