using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -19.64f;
    private bool grounded = false;
    private readonly float checkRadius = .2f;
    private Vector3 velocity;
    private LayerMask groundMask;
    private Transform groundCheck;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        groundMask = LayerMask.GetMask("Ground");

        /* **CLEAN UP THE CODE IN THE SCRIPTS AND ADD COMMENTS!!!** */
    }

    // Update is called once per frame
    void Update()
    {
        // X & Z axis input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical; // Moves the player by transform.position
        
        controller.Move(movement.normalized * speed * Time.deltaTime); // Moves the player

        if (Input.GetButtonDown("Jump") && grounded) // Player jumps when on ground and jump button pressed
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask); // Checks if the player is touching the ground

        velocity.y += gravity * Time.deltaTime; // Applies gravity to the player
        controller.Move(velocity * Time.deltaTime); // Moves the player by current velocity

        if (grounded && velocity.y <= 0) // Resets velocity when the player is on the ground
            velocity.y = -2f;
    }
}
