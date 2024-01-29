using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBarrage : MonoBehaviour
{
    public int maxWaves = 3;
    private int waveCount;
    public float delaysBetweenWaves = 1f;
    private float delayTimer;
    public GameObject charm;
    private PlayerController player;
    private ActiveAbilityInformation abilityInformation;
    private float modifiedDamage;

    void Start()
    {
        delayTimer = 0;
        waveCount = 0;
        player = FindAnyObjectByType<PlayerController>();
        abilityInformation = GetComponent<ActiveAbilityInformation>();
        GameObject tag = Instantiate(charm, player.transform.position, Quaternion.identity);
        tag.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag.GetComponent<ExplosiveTag>().fuseTime = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveCount <= maxWaves)
        {
            modifiedDamage = charm.GetComponent<WeaponInformation>().damage.currentValue * abilityInformation.damageModifier;
            waveCount++;
            FireWave();
            delayTimer = delaysBetweenWaves;
        }
        if (delayTimer > 0)
        {
            delayTimer -= 0.01f;
        }
        if (waveCount >= maxWaves)
        {
            Destroy(gameObject);
        }
    }
    void FireWave()
    {
        GameObject tag1 = Instantiate(charm, player.transform.position + Vector3.right * waveCount * 2, Quaternion.identity);
        tag1.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag1.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag1.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag2 = Instantiate(charm, player.transform.position + Vector3.left * waveCount * 2, Quaternion.identity);
        tag2.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag2.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag2.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag3 = Instantiate(charm, player.transform.position + Vector3.up * waveCount * 2, Quaternion.identity);
        tag3.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag3.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag3.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag4 = Instantiate(charm, player.transform.position + Vector3.down * waveCount * 2, Quaternion.identity);
        tag4.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag4.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag4.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
    }
    public void SetWeaponReference(GameObject newWeapon)
    {
        charm = newWeapon;
    }
}
