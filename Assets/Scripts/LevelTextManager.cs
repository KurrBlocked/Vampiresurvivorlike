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
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindAnyObjectByType<PlayerLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "" + levelManager.level;
        
        image.rectTransform.position = new Vector3((levelManager.percentage * 600f) + 510f, image.rectTransform.position.y, image.rectTransform.position.z);
        image.rectTransform.localScale = new Vector3(levelManager.percentage * 12, image.rectTransform.localScale.y, image.rectTransform.localScale.z);
    }
}
