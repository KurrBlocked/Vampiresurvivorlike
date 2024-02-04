using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInformation : MonoBehaviour
{
    public string weaponDescription;
    public WeaponStat timer;
    public WeaponStat damage;
    public WeaponStat numProjectiles;
    public WeaponStat scale;
    public int levelRequirement;
    public Sprite icon;

    public void ResetUpdates()
    {
        timer.currentTier = 0;
        damage.currentTier = 0;
        numProjectiles.currentTier = 0;
        scale.currentTier = 0;
        UpdateCurrentValues();
    }
    public void UpdateCurrentValues()
    {
        timer.SetCurrentValue();
        damage.SetCurrentValue();
        numProjectiles.SetCurrentValue();
        scale.SetCurrentValue();
    }
    public void CloneStats(WeaponInformation weapon)
    {
        timer = weapon.timer;
        damage = weapon.damage;
        numProjectiles = weapon.numProjectiles;
        scale = weapon.scale;
    }

    public int CalculateTotalUpgradeScore()
    {
        return timer.currentTier + damage.currentTier + numProjectiles.currentTier + scale.currentTier;
    }
    public void PrintInfo()
    {
        Debug.Log("Weapon Timer:" + timer.currentValue + " || Weapon Damage:" + damage.currentValue + " || Weapon Number of Projectiles:" + numProjectiles.currentValue + "|| Weapon Scale:" + scale.currentValue);
    }
}
