using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public List<GameObject> numbers;
    // Start is called before the first frame update
    void Awake()
    {
        numbers = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            numbers.Add(Resources.Load("Numbers_" + i) as GameObject);
        }

    }
    public void SpawnDamageNumbers(int number, Vector3 position)
    {
        int[] numbersToSpawn = IntToArray(number);
        int index = 0;
        float rand = Random.Range(-0.5f, 0.5f);
        foreach (int n in numbersToSpawn)
        {
            Instantiate(numbers[n], position + new Vector3(index * 0.3f + rand,0,0), Quaternion.identity);
            index++;
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
