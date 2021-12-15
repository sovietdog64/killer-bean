using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI highScoreUI;
    public static int score;
    public static int highScore;

    private void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = $"Score: {score}";
        highScoreUI.text = $"High Score: {highScore}";

        if (score > highScore)
        {
            highScore = score;
        }
    }
}
