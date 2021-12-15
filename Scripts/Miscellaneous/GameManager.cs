using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string gameMode;
    public GameObject[] enemies;
    public static int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        enemyNum = enemies.Length;

        /* **ADD A DIFFICULTY MODE** */
    }

    // Update is called once per frame
    void Update()
    {
        /* GAME MODES
         * Easy- more amo, enemies deal less damage, less enemeies
         * Normal- normal amo, enemies deal regular damage, normal amout of enemies
         * Hard- less amo, enemies deal more damage, more enemies
         * Custom- player can choose amount for the folloing, amo, enemy damage, num of enemies (Might do this)
         */
        
        /* BONUS
         * If I can, I'll more maps, idk yet...
         */
        if (enemyNum <= 0)
        {
            SceneManager.LoadScene("Congratulations");
        }
    }
}
