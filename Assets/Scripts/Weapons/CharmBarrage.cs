using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBarrage : MonoBehaviour
{
    public int maxWaves = 3;
    private int waveCount;
    public GameObject charm;
    private PlayerController player;
    private ActiveAbilityInformation abilityInformation;
    private float modifiedDamage;

    void Start()
    {
        waveCount = 0;
        player = FindAnyObjectByType<PlayerController>();
        abilityInformation = GetComponent<ActiveAbilityInformation>();
        GameObject tag = Instantiate(charm, player.transform.position, Quaternion.identity);
        modifiedDamage = charm.GetComponent<WeaponInformation>().damage.currentValue * abilityInformation.damageModifier;
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
        }
        if (waveCount >= maxWaves)
        {
            Destroy(gameObject);
        }
    }
    void FireWave()
    {
        GameObject tag1 = Instantiate(charm, player.transform.position + Vector3.right * waveCount * 3, Quaternion.identity);
        tag1.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag1.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag1.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag2 = Instantiate(charm, player.transform.position + Vector3.left * waveCount * 3, Quaternion.identity);
        tag2.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag2.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag2.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag3 = Instantiate(charm, player.transform.position + Vector3.up * waveCount * 3, Quaternion.identity);
        tag3.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag3.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag3.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag4 = Instantiate(charm, player.transform.position + Vector3.down * waveCount * 3, Quaternion.identity);
        tag4.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag4.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag4.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount/10f;
        GameObject tag5 = Instantiate(charm, player.transform.position + (Vector3.right + Vector3.up)  * waveCount * 3, Quaternion.identity);
        tag5.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag5.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag5.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount / 10f;
        GameObject tag6 = Instantiate(charm, player.transform.position + (Vector3.left + Vector3.down) * waveCount * 3, Quaternion.identity);
        tag6.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag6.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag6.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount / 10f;
        GameObject tag7 = Instantiate(charm, player.transform.position + (Vector3.up + Vector3.left) * waveCount * 3, Quaternion.identity);
        tag7.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag7.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag7.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount / 10f;
        GameObject tag8 = Instantiate(charm, player.transform.position + (Vector3.down + Vector3.right) * waveCount * 3, Quaternion.identity);
        tag8.GetComponent<WeaponInformation>().damage.currentValue = modifiedDamage;
        tag8.GetComponent<ExplosiveTag>().isTriggerable = false;
        tag8.GetComponent<ExplosiveTag>().fuseTime = 1 + waveCount / 10f;
    }
    public void SetWeaponReference(GameObject newWeapon)
    {
        charm = newWeapon;
    }
}
