using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = $"Health: {PlayerHealth.health}%";
    }
}
