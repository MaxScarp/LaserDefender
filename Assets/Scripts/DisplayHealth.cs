using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player player;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(player.GetHealth() <= 0)
        {
            healthText.text = "0";
        }
        else
        {
            healthText.text = player.GetHealth().ToString();
        }
    }
}
