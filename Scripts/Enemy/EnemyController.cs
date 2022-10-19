using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float minSpeed = .1f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float zoneRadius = 55f;
    [SerializeField] float damage = 10f;
    [SerializeField] float changeTime = 2f;
    [SerializeField] float gravity = -19.64f; // 
    [SerializeField] AudioClip death;
    private bool move = true; // true when player starts moving
    private bool playerInZone = false; // checks whether player is in enemy chase zone
    private float vertical; // horizontal movement
    private float horizontal; // vertical movement
    private float timer;
    private bool grounded; // true when player touches ground
    private float radius = .2f; // radius of ground trigger
    private Vector3 velocity = Vector3.zero; // 
    private LayerMask groundMask;
    private CharacterController controller;
    private AudioSource audioSource;
    private Material material;
    private Transform groundCheck;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        material = GetComponent<Renderer>().material;
        target = GameObject.Find("Player").GetComponent<Transform>();

        timer = changeTime;
        vertical = Random.Range(-1, 2);
        horizontal = Random.Range(-1, 2);
        groundMask = LayerMask.GetMask("Ground");

        // Calls method
        GenerateRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy switch direction every couple of seconds

        if (move)
        {
            Vector3 movement = new Vector3(horizontal, velocity.y, vertical);
            float speed = 10f;

            controller.Move(movement * speed * Time.deltaTime);

            timer -= Time.deltaTime;

            SwitchDirection();
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < zoneRadius)
        {
            ChasePlayer();
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform tr in transform)
            groundCheck = tr;

        grounded = Physics.CheckSphere(groundCheck.position, radius, groundMask); // checks whether the player is touching the ground

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (grounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
        
    }

    void SwitchDirection()
    {
        if (timer <= 0 && !playerInZone)
        {
            // Change's enemy's direction

            vertical = Random.Range(-1, 2);
            horizontal = Random.Range(-1, 2);

            timer = changeTime;
        }
    }

    void ChasePlayer()
    {
        move = false;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 
            Random.Range(minSpeed, maxSpeed));
    }

    void GenerateRandomColor()
    {
        Color randomColor = new Color(Random.Range(0, 20), Random.Range(0, 20), Random.Range(0, 20)); // Generates random color
        material.color = randomColor; // Sets enemy's material to randomColor

    }

    private void OnTriggerEnter(Collider other) // If the player collides with an enemy, the player will lose health
    {
        if (other.gameObject.CompareTag("Player"))
            PlayerHealth.health -= damage;
    }
}
