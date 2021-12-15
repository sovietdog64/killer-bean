using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Variables
    private const float maxHealth = 100;
    public static float health;
    [SerializeField] float deathDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Assigns health to have the same value of maxHealth
    }

    // Update is called once per frame
    void Update()
    {
        Die(); // Invokes Die() method
    }

    void Die() // If the player's health reaches 0 or below, the player will die
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject, deathDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
