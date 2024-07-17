using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private HingeJoint2D hingeJoint;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Make the Rigidbody2D kinematic initially
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // Record the offset between the object's position and the mouse position
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;

        // Add HingeJoint2D
        hingeJoint = gameObject.AddComponent<HingeJoint2D>();
        hingeJoint.autoConfigureConnectedAnchor = false;
        hingeJoint.connectedAnchor = GetMouseWorldPos();
        hingeJoint.anchor = transform.InverseTransformPoint(GetMouseWorldPos());

        rb.isKinematic = false; // Enable physics
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Remove HingeJoint2D
        if (hingeJoint != null)
        {
            Destroy(hingeJoint);
            hingeJoint = null;
        }

        rb.velocity = Vector2.zero; // Stop any movement
        rb.isKinematic = true; // Disable physics to make it stationary

        // Reset rotation
        rb.rotation = 0f;
        rb.angularVelocity = 0f;
    }

    void Update()
    {
        if (isDragging)
        {
            // Update the hinge joint's connected anchor to follow the mouse
            hingeJoint.connectedAnchor = GetMouseWorldPos();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in screen space
        Vector3 mousePoint = Input.mousePosition;

        // Convert the screen space mouse position to world space
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
