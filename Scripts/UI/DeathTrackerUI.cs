using UnityEngine;
using TMPro;

public class DeathTrackerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deathUI;
    public static int kills;

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deathUI.text = $"Kills: {kills}/30";
    }
}
