using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNumberSpawner : MonoBehaviour
{
    private LevelNumberCreator numberSpawner;
    public float timer;

    public int minutes;

    // Start is called before the first frame update
    void Start()
    {
        numberSpawner = GetComponent<LevelNumberCreator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        int seconds = (int)timer % 60;
        minutes = (int)timer / 60;
        numberSpawner.ClearNumbers();
        if (seconds < 10)
        {
            numberSpawner.SpawnNumbers(0, new Vector3(40, -980, 0));
            numberSpawner.SpawnNumbers(seconds, new Vector3(100, -980, 0));
        }
        else
        {
            numberSpawner.SpawnNumbers(seconds, new Vector3(40, -980, 0));

        }
        if (minutes < 10)
        {
            numberSpawner.SpawnNumbers(0, new Vector3(-130, -980, 0));
            numberSpawner.SpawnNumbers(minutes, new Vector3(-70, -980, 0));
        }
        else
        {
            numberSpawner.SpawnNumbers(minutes, new Vector3(-130, -980, 0));
        }
    }
}
