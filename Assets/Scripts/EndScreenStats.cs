using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EndScreenStats : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI upgradesText;
    public TextMeshProUGUI killCountText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Player Level: " + GameStateManager.playerLevel;
        TimeText.text = "Minutes Survived: " + GameStateManager.minutesLasted;
        expText.text = "Exp Obtained: " + GameStateManager.expGained;
        upgradesText.text = "Upgrades Obtained: " + GameStateManager.upgradesObtained + " / 50";
        killCountText.text = "Kill Count: " + GameStateManager.killCount;
    }
}
