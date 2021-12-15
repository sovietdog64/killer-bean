using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Variables
    [SerializeField] float damage = 10f;
    [SerializeField] float impactForce = 35f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15f;
    float nextTimeToFire = 0f;
    private Animator anim;
    private AudioSource audioSource;
    private ParticleSystem shooting;
    private Transform fpsCam;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float destroyTime = .75f;
    [SerializeField] float audioVolume = 1f;
    [SerializeField] AudioClip shootingEffect;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        shooting = GameObject.Find("ShootingParticle").GetComponent<ParticleSystem>();
        fpsCam = Camera.main.GetComponent<Transform>();

        /* **ADD AMO AND RELOADING MECHANICS** */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) // If the fire button is down, the player will
        {                                                            // Shoot() every 15 seconds
            nextTimeToFire = Time.time + 1 / fireRate; // Sets the amount of time the player will shoot again
            Shoot(); // Calls Shoot() method
            anim.SetBool("Shoot", true); // Plays shooting animation
            if (!shooting.isPlaying) // If shooting particle is off, shooting particle will play
            {
                shooting.Play();
                audioSource.PlayOneShot(shootingEffect, audioVolume);
            }
        }
        else if (Input.GetButtonUp("Fire1")) // If Fire is no longer being pressed,
        {                                    // the animation & particle will no longer play
            anim.SetBool("Shoot", false);
            shooting.Stop();
            audioSource.Stop();
        }
        
    }

    void Shoot()
    {
        RaycastHit hit; // RaycastHit variable declared

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) // Creates raycast to detect
        {                                                                                         // collision
            Target target = hit.transform.GetComponent<Target>(); // Returns target scripts from the object it hit

            if (target != null) // If the bullet hits a gameObject with a target script, the gameObject will take damage
                target.TakeDamage(damage);

            if (hit.rigidbody != null) // If the bullet hits a gameObject with a rigidbody, the gameObject will be pushed back
                hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiates bullet
        Destroy(impact, destroyTime); // destroys bullet                                               // on impact
    }
}
