using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSickleCyclone : MonoBehaviour
{
    public int sicklesToSpawn = 10;
    private int sicklessRemaining;
    public float delaysBetweenSpawn = 1f;
    private float delayTimer;
    public GameObject chainSickle;
    private PlayerController player;
    private ActiveAbilityInformation abilityInformation;
    private float modifiedDamage;
    // Start is called before the first frame update
    void Start()
    {
        delayTimer = 0;
        sicklessRemaining = sicklesToSpawn;
        player = FindAnyObjectByType<PlayerController>();
        abilityInformation = GetComponent<ActiveAbilityInformation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sicklessRemaining > 0 && delayTimer <= 0)
        {
            modifiedDamage = chainSickle.GetComponent<WeaponInformation>().damage.currentValue * abilityInformation.damageModifier;
            sicklessRemaining--;
            delayTimer = delaysBetweenSpawn;
            GameObject sickle = Instantiate(chainSickle, player.transform.position, Quaternion.identity);
            sickle.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        }
        if (delayTimer > 0)
        {
            delayTimer -= 0.01f;
        }
        if (sicklessRemaining == 0)
        {
            Destroy(gameObject,2);
        }
    }

    public void SetWeaponReference(GameObject newWeapon)
    {
        chainSickle = newWeapon;
    }
}
