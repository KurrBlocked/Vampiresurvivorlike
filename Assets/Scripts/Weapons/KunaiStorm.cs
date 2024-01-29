using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiStorm : MonoBehaviour
{
    public int waves = 3;
    private int wavesRemaining;
    public int kunaisPerWave = 36;
    public float delaysBetweenWaves = 1f;
    private float delayTimer;
    public GameObject kunai;
    private PlayerController player;
    private ActiveAbilityInformation abilityInformation;
    private float modifiedDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        delayTimer = 0;
        wavesRemaining = waves;
        player = FindAnyObjectByType<PlayerController>();
        abilityInformation = GetComponent<ActiveAbilityInformation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wavesRemaining > 0 && delayTimer <= 0)
        {
            modifiedDamage = kunai.GetComponent<WeaponInformation>().damage.currentValue * abilityInformation.damageModifier;
            FireWave();
            wavesRemaining--;
            delayTimer = delaysBetweenWaves;
        }
        if (delayTimer > 0)
        {
            delayTimer -= 0.01f;
        }
        if (wavesRemaining == 0)
        {
            Destroy(gameObject);
        }
    }

    void FireWave()
    {
        float degreesBetweenKunais = 360f / kunaisPerWave;
        for (int i = 0; i < kunaisPerWave; i++)
        { 
            GameObject knive  = Instantiate(kunai, player.transform.position, Quaternion.Euler(0,0,i * degreesBetweenKunais + 225f));
            float angleRadians = i * degreesBetweenKunais * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
            knive.GetComponent<Kunai>().SetTarget(direction);
            knive.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        }
    }
    public void SetWeaponReference(GameObject newWeapon)
    {
        kunai = newWeapon;
    }
}
