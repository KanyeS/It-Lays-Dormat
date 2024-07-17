using UnityEngine;

public class WalkPast : MonoBehaviour
{
    public float speed = 5f; // Speed of the object's movement
    private Vector2 screenBounds;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the screen boundaries
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        // Move the object in the X direction
        Vector3 newPosition = transform.position;
        if (movingRight)
        {
            newPosition.x += speed * Time.deltaTime;
        }
        else
        {
            newPosition.x -= speed * Time.deltaTime;
        }

        // Check if the object hits the screen boundaries
        if (newPosition.x > screenBounds.x || newPosition.x < -screenBounds.x)
        {
            // Reverse the direction
            movingRight = !movingRight;

            // Flip the sprite
            FlipSprite();
        }

        // Apply the new position
        transform.position = newPosition;
    }

    void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            // Flip the sprite by inverting its scale on the X-axis
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
