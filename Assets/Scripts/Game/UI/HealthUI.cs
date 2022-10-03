using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Text healthText;

    private DropletHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<DropletHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealth.currentHealth.ToString();
    }
}
