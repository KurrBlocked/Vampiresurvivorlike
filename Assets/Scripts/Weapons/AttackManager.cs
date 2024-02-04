using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject cursor;
    private PlayerController player;

    public List<GameObject> obtainedItems;
    public List<GameObject> unobtainedItems;
    private int numOfWeapons;
    private List<float> timers = new List<float>();

    public List<GameObject> obtainedActives;
    public List<GameObject> unobtainedActives;
    private int numOfActives;
    public List<float> activeTimers = new List<float>();

    public List<GameObject> activeSlots;

    // Start is called before the first frame update
    void Start()
    {
        numOfWeapons = 0;
        numOfActives = 0;
        player = FindAnyObjectByType<PlayerController>();
        foreach (GameObject weapon in unobtainedItems)
        { 
            weapon.GetComponent<WeaponInformation>().ResetUpdates();
        }
        obtainedItems[0].GetComponent<WeaponInformation>().ResetUpdates();
    }

    private void Update()
    {
        if (numOfWeapons != obtainedItems.Count)
        {
            obtainedItems[numOfWeapons].GetComponent<WeaponInformation>().timer.SetCurrentValue();
            timers.Add(obtainedItems[numOfWeapons].GetComponent<WeaponInformation>().timer.currentValue);
            numOfWeapons++;
        }
        if (numOfActives != obtainedActives.Count)
        {
            activeTimers.Add(0);
            activeSlots[numOfActives].GetComponent<ActiveSlot>().icon.sprite = obtainedActives[numOfActives].GetComponent<ActiveAbilityInformation>().icon;
            activeSlots[numOfActives].GetComponent<ActiveSlot>().totalCooldown = obtainedActives[numOfActives].GetComponent<ActiveAbilityInformation>().timer;
            activeSlots[numOfActives].GetComponent<ActiveSlot>().isSet = true;
            numOfActives++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i] <= 0)
            {
                if (obtainedItems[i].GetComponent<WeaponInformation>().numProjectiles.currentValue > 1f)
                {
                    FireMultiple(obtainedItems[i], (int)obtainedItems[i].GetComponent<WeaponInformation>().numProjectiles.currentValue);
                }
                else
                {
                    Fire(obtainedItems[i]);
                }
                timers[i] = obtainedItems[i].GetComponent<WeaponInformation>().timer.currentValue;
            }
            else
            {
                timers[i] = timers[i] -= 0.01f;
            }
        }

        for (int i = 0; i < activeTimers.Count; i++)
        {
            if (activeTimers[i] <= 0)
            {
                activeTimers[i] = 0;
            }
            else
            {
                activeTimers[i] = activeTimers[i] -= 0.01f;
            }
            activeSlots[i].GetComponent<ActiveSlot>().currentCooldown = activeTimers[i];
        }
        ManageActiveAbilities();
    }


    void Fire(GameObject attack)
    {
        GameObject bullet;
        if (attack.name == "ChainSicklePivot" || attack.name == "ExplosiveTag")
        {
            bullet = Instantiate(attack, player.transform.position, Quaternion.identity);
        }
        else
        {
            bullet = Instantiate(attack, player.transform.position, FindAnyObjectByType<CursorController>().pointerRotation * Quaternion.Euler(0, 0, -45));
        }
        
        SetDirectionBasedOnDifferentWeapon(bullet);
    }

    void SetDirectionBasedOnDifferentWeapon(GameObject bullet)
    {
        if (bullet.GetComponent<Kunai>() != null)
        {
            bullet.GetComponent<Kunai>().SetTarget(-FindAnyObjectByType<CursorController>().throwingDirection.normalized);
        }
        if (bullet.GetComponent<LargeShuriken>() != null)
        {
            bullet.GetComponent<LargeShuriken>().SetTarget(-FindAnyObjectByType<CursorController>().throwingDirection.normalized);
            bullet.GetComponent<LargeShuriken>().SetScale(bullet.GetComponent<WeaponInformation>().scale.currentValue);
        }
        if (bullet.GetComponent<ExplosiveTag>() != null)
        {
            bullet.GetComponent<ExplosiveTag>().SetScale(bullet.GetComponent<WeaponInformation>().scale.currentValue);
        }
    }
    void ManageActiveAbilities()
    {
        for (int i = 0; i < activeTimers.Count; i++)
        {
            if (activeTimers[i] <= 0)
            {
                if (i == 0)//Seperate dash from other active abilities
                {
                    if (obtainedActives[0].GetComponent<Dash>().dashWasPressed)
                    {
                        activeTimers[i] = obtainedActives[i].GetComponent<ActiveAbilityInformation>().timer;
                    }
                    else
                    {
                        obtainedActives[0].GetComponent<Dash>().canDash = true;
                    }
                    
                }
                else
                {
                    switch (i)
                    {
                        case (1):
                            if (Input.GetKey(KeyCode.Alpha1))
                            {
                                activeTimers[i] = obtainedActives[i].GetComponent<ActiveAbilityInformation>().timer;
                                GameObject active = Instantiate(obtainedActives[i], player.transform.position, Quaternion.identity);
                                ReplaceWithNewWeaponReference(active);
                            }
                            break;
                        case (2):
                            if (Input.GetKey(KeyCode.Alpha2))
                            {
                                activeTimers[i] = obtainedActives[i].GetComponent<ActiveAbilityInformation>().timer;
                                GameObject active = Instantiate(obtainedActives[i], player.transform.position, Quaternion.identity);
                                ReplaceWithNewWeaponReference(active);
                            }
                            break;
                        case (3):
                            if (Input.GetKey(KeyCode.Alpha3))
                            {
                                activeTimers[i] = obtainedActives[i].GetComponent<ActiveAbilityInformation>().timer;
                                GameObject active = Instantiate(obtainedActives[i], player.transform.position, Quaternion.identity);
                                ReplaceWithNewWeaponReference(active);
                            }
                            break;
                        default:
                            
                            Debug.Log("Error: " + i);
                            break;
                    }
                }
            }
        }
    }

    void FireMultiple(GameObject attack, int num)
    {
        Vector3 throwingDirection = -FindAnyObjectByType<CursorController>().throwingDirection.normalized;
        throwingDirection = new Vector3(-throwingDirection.y, throwingDirection.x, 0);
        for (int i = 0; i < num; i++)
        {
            GameObject bullet = Instantiate(attack, player.transform.position + (throwingDirection * i * 0.5f), FindAnyObjectByType<CursorController>().pointerRotation * Quaternion.Euler(0, 0, -45)); ;
            SetDirectionBasedOnDifferentWeapon(bullet);
        }
    }
    public void AddToInventory(GameObject item)
    {
        obtainedItems.Add(item);
        unobtainedItems.Remove(item);
    }
    public void AddToActiveAbilities(GameObject item)
    {
        obtainedActives.Add(item);
        unobtainedActives.Remove(item);
    }

    void ReplaceWithNewWeaponReference(GameObject active)
    {
        GameObject newWeapon = new GameObject();
        if (active.GetComponent<KunaiStorm>() != null)
        {
            foreach (GameObject weapon in obtainedItems)
            {
                if (weapon.GetComponent<Kunai>() != null)
                {
                    newWeapon = weapon;
                }
            }
            active.GetComponent<KunaiStorm>().SetWeaponReference(newWeapon);
        }
        if (active.GetComponent<ChainSickleCyclone>() != null)
        {
            foreach (GameObject weapon in obtainedItems)
            {
                if (weapon.GetComponent<ChainSickle>() != null)
                {
                    newWeapon = weapon;
                }
            }
            active.GetComponent<ChainSickleCyclone>().SetWeaponReference(newWeapon);
        }
        if (active.GetComponent<CharmBarrage>() != null)
        {
            foreach (GameObject weapon in obtainedItems)
            {
                if (weapon.GetComponent<ExplosiveTag>() != null)
                {
                    newWeapon = weapon;
                }
            }
            newWeapon.GetComponent<ExplosiveTag>().SetScale(newWeapon.GetComponent<WeaponInformation>().scale.currentValue);
            active.GetComponent<CharmBarrage>().SetWeaponReference(newWeapon);
        }
    }
}
