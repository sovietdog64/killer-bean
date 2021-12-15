using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Variables
    public static float health = 50f;
    [SerializeField] float deathDelay = 2f;

    public void TakeDamage(float damage) // Makes gameObject lose health
    {
        health -= damage; // Decreases gameObject health by damage variable
        if (health <= 0) // Die() is called when health reaches zero
            Die();
    }

    void Die() // Destroys gameObject
    {
        StartCoroutine(deathCo());
        Destroy(gameObject);
        ScoreTracker.score += 10;
        GameManager.enemyNum--;
        DeathTrackerUI.kills++;
    }

    IEnumerator deathCo()
    {
        yield return new WaitForSeconds(deathDelay);

    }
}
