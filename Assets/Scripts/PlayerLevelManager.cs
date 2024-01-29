using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int experience;
    public int level;
    public int levelUpRequirement = 10;
    public float percentage;
    public float increaseLevelUpRequirementIncrement = 2.1f;
    private int previousRequirement;

    public UpgradeManager upgrader;
    // Start is called before the first frame update
    void Start()
    {
        experience = 0;
        level = 1;
        upgrader = FindAnyObjectByType<UpgradeManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (percentage >= 1f)
        {
            level++;
            previousRequirement = levelUpRequirement;
            levelUpRequirement = (int)((float)levelUpRequirement * increaseLevelUpRequirementIncrement);
            upgrader.OpenUpgradeMenu();
        }
        percentage = (float)(experience - previousRequirement) / levelUpRequirement;
    }
}
