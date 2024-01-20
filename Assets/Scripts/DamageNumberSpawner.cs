using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public List<GameObject> numbers;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            numbers.Add(Resources.Load("Numbers_" + i) as GameObject);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SpawnDamageNumbers(int number, Vector3 position)
    {
        int[] numbersToSpawn = IntToArray(number);
        int index = 0;
        foreach (int n in numbersToSpawn)
        {
            Instantiate(numbers[n], position + new Vector3(index * 0.3f,0,0), Quaternion.identity);
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
