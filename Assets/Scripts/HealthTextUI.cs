using UnityEngine;
using TMPro;

public class HealthTextUI : MonoBehaviour
{
    public Movement[] players;             
    public TextMeshProUGUI[] hpTexts;       

    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null && hpTexts[i] != null)
            {
                hpTexts[i].text = "HP: " + players[i].GetCurrentHealth().ToString();
            }
        }
    }
}
