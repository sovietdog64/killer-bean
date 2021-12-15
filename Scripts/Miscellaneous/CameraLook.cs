using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // Variables
    [SerializeField] private float mouseSensitivity = 150f; // How fast the camera rotates when mouse moves
    private Transform playerBody; // Player's transform
    private float xRotation = 0f; // Rotate x-axis

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("Player").GetComponent<Transform>(); // Gets player's transform component

        Cursor.lockState = CursorLockMode.Locked; // Locks cursor
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaMouse = mouseSensitivity * Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Mouse input

        xRotation -= deltaMouse.y; // Moves the player's x-axis rotation
        xRotation = Mathf.Clamp(xRotation, -90, 90); // Limits the player's vertical view

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Implements xRotation on gameObject

        playerBody.Rotate(Vector3.up, deltaMouse.x); // Implements horizontal rotation on the player's transform

        if (PlayerHealth.health <= 0 || GameManager.enemyNum <= 0)
            Cursor.lockState = CursorLockMode.None;
    }
}
