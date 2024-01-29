using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelTextManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Image image;

    private PlayerLevelManager levelManager;
    private int currentLevel;

    private LevelNumberCreator numberSpawner;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindAnyObjectByType<PlayerLevelManager>();
        currentLevel = 0;
        numberSpawner = GetComponent<LevelNumberCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        //levelText.text = "" + levelManager.level;
        
        image.rectTransform.position = new Vector3((levelManager.percentage * 600f) + 510f, image.rectTransform.position.y, image.rectTransform.position.z);
        image.rectTransform.localScale = new Vector3(levelManager.percentage * 12, image.rectTransform.localScale.y, image.rectTransform.localScale.z);

        if (currentLevel != levelManager.level)
        {
            currentLevel = levelManager.level;
            numberSpawner.ClearNumbers();
            numberSpawner.SpawnNumbers(levelManager.level, new Vector3(0,0,0));
        }
    }
}
