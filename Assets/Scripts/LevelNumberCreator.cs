using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumberCreator : MonoBehaviour
{
    public List<GameObject> numbers;
    private List<GameObject> numbersSpawned;
    // Start is called before the first frame update
    void Awake()
    {
        numbers = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            numbers.Add(Resources.Load("Image_" + i) as GameObject);
        }
        numbersSpawned = new List<GameObject>();

    }
    public void SpawnNumbers(int number, Vector3 position)
    {
        int[] numbersToSpawn = IntToArray(number);
        int index = 0;
        foreach (int n in numbersToSpawn)
        {
            GameObject num = Instantiate(numbers[n], position + new Vector3(1760 + index * 62.5f, 1030, 0), Quaternion.identity, transform);
            numbersSpawned.Add(num);
            index++;
        }
    }

    public void ClearNumbers()
    {
        foreach (GameObject digit in numbersSpawned)
        {
            Destroy(digit);
        }
    }
    int[] IntToArray(int number)
    {
        string numberString = number.ToString();
        int[] digitsArray = new int[numberString.Length];
        for (int i = 0; i < numberString.Length; i++)
        {
            digitsArray[i] = int.Parse(numberString[i].ToString());
        }
        return digitsArray;
    }
}
