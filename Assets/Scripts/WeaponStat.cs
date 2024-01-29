using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStat
{
    public string upgradeDescription;
    public int currentTier;
    public float[] values;
    public float[] levelRequirement;
    public float currentValue;

    public void SetCurrentValue()
    {
        currentValue = values[currentTier];
    }
    public void UpgradeTier()
    {
        currentTier++;
        SetCurrentValue();
    }
}
