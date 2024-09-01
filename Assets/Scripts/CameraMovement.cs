using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector2 distance = new Vector2(6, 3);
    public Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    public float mouseSensitivity = 2f;
    public float raycastDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        angle.x -= mouseX * Mathf.Deg2Rad;

        // Limit rotation to 360 degrees (optional)
        // angle.x = Mathf.Clamp(angle.x, -Mathf.PI, Mathf.PI);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Calculate the camera's desired position
        Vector3 orbit = new Vector3(Mathf.Cos(angle.x), 0, Mathf.Sin(angle.x));
        Vector3 desiredPosition = target.position - orbit * distance.x + Vector3.up * distance.y;

        // Create a raycast from the camera's position towards the desired position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, desiredPosition - transform.position, out hit, raycastDistance))
        {
            // If the raycast hits something, adjust the camera's position to avoid collision
            desiredPosition = hit.point + hit.normal * 0.1f;
        }

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);

        // Look at the target
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}