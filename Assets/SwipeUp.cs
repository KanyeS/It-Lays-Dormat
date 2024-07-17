using UnityEngine;

public class SwipeUp : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float minSwipeDistance = 50f; // Minimum distance for a swipe to be registered
    public float pullBackDistance = 1f; // Distance to move the object downwards when swiped

    void Update()
    {
        DetectSwipe();
    }

    void DetectSwipe()
    {
        // Detect touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    HandleSwipe();
                    break;
            }
        }

        // Detect mouse input (for testing in the editor)
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = (Vector2)Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = (Vector2)Input.mousePosition;
            HandleSwipe();
        }
    }

    void HandleSwipe()
    {
        float swipeDistance = Vector2.Distance(startTouchPosition, endTouchPosition);
        if (swipeDistance >= minSwipeDistance)
        {
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
            {
                if (swipeDirection.y > 0)
                {
                    // Swipe is upwards
                    PullBackObject();
                }
            }
        }
    }

    void PullBackObject()
    {
        // Move the object downwards
        transform.position = new Vector3(transform.position.x, transform.position.y - pullBackDistance, transform.position.z);

        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
