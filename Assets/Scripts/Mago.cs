using UnityEngine;

public class Mago : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public float mouseSensitivity = 2f;
    public CharacterController controller;
    public float initialHeightOffset = 0.5f;
    public float groundCheckDistance = 0.5f;
    public float raycastHeightOffset = 0.1f;  // Raycast start offset above ground
    public float footstepDelay = 0.5f;        // Time between footstep sounds

    public AudioSource audioSource;
    public AudioClip piedraFootstep;  // Footstep sound for "Piedra"
    public AudioClip alfombraFootstep;  // Footstep sound for "Alfombra"

    private Vector3 moveDirection;
    private float rotationY = 0f;
    private bool isGrounded = false;
    private bool wasGrounded = false;
    private float lastFootstepTime = 0f;  // Tracks the last time a footstep sound was played

    void Start()
    {
        // Adjust character's initial position to prevent being under the ground
        Vector3 newPosition = transform.position;
        newPosition.y += initialHeightOffset;
        transform.position = newPosition;
    }

    void Update()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // Apply movement using Character Controller
        controller.Move(moveDirection * Time.deltaTime);

        // Handle rotation
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        transform.eulerAngles = new Vector3(0, rotationY, 0);

        // Cast ray from slightly above the character to detect the ground
        Vector3 raycastOrigin = transform.position + Vector3.up * raycastHeightOffset;
        RaycastHit hit;

        Debug.DrawRay(raycastOrigin, -Vector3.up * groundCheckDistance, Color.red);  // Visualize raycast

        // Check for ground collision using raycast
        if (Physics.Raycast(raycastOrigin, -Vector3.up, out hit, groundCheckDistance))
        {
            if (hit.collider.tag == "Piedra" || hit.collider.tag == "Alfombra")
            {
                isGrounded = true;

                // Play footstep sound if the character is grounded and moving
                if (isGrounded && (horizontal != 0 || vertical != 0) && Time.time - lastFootstepTime >= footstepDelay)
                {
                    PlayFootstepSound(hit.collider.tag);
                    lastFootstepTime = Time.time;  // Update the time of the last footstep sound
                }
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        // Update animator parameters
        animator.SetFloat("xSpeed", horizontal);
        animator.SetFloat("ySpeed", vertical);

        // Track grounded state change for sound triggering
        wasGrounded = isGrounded;
    }

    // Play footstep sound based on the surface tag
    void PlayFootstepSound(string surfaceTag)
    {
        if (audioSource != null)
        {
            if (surfaceTag == "Piedra" && piedraFootstep != null)
            {
                audioSource.clip = piedraFootstep;
                audioSource.Play();
            }
            else if (surfaceTag == "Alfombra" && alfombraFootstep != null)
            {
                audioSource.clip = alfombraFootstep;
                audioSource.Play();
            }
        }
    }
}
