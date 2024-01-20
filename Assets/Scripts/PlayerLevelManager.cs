using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int experience;
    public int level;
    public int levelUpRequirement = 10;
    public float percentage;

    // Start is called before the first frame update
    void Start()
    {
        experience = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (experience / levelUpRequirement == level)
        {
            level++;
        }
        percentage = ((float) experience - (levelUpRequirement * (level - 1))) / (levelUpRequirement * level);
    }
}
