using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeWindow;
    private CanvasGroup upgradeScreen;

    private AttackManager attackManager;
    private PlayerLevelManager playerLevel;

    public GameObject upgradeSlot;
    private List<GameObject> allPossibleUpgrades;
    private List<GameObject> listedUpgrades;
    public int maxNumberOfUpgradesDisplayed = 4;
    // Start is called before the first frame update
    void Start()
    {
        upgradeScreen = upgradeWindow.GetComponent<CanvasGroup>();
        upgradeScreen.alpha = 0;
        upgradeScreen.interactable = false;
        upgradeScreen.blocksRaycasts = false;
        attackManager = FindAnyObjectByType<AttackManager>();
        playerLevel = FindAnyObjectByType<PlayerLevelManager>();
        allPossibleUpgrades = new List<GameObject>();
        listedUpgrades = new List<GameObject>();
    }
    public void OpenUpgradeMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        upgradeScreen.alpha = 1;
        upgradeScreen.interactable = true;
        upgradeScreen.blocksRaycasts = true;
        CreateUpgradeTable();
        RandomizeAndDisplayUpgrades();
    }
    public void ResumePlaying()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        upgradeScreen.alpha = 0;
        upgradeScreen.interactable = false;
        upgradeScreen.blocksRaycasts = false;

        foreach (GameObject upgrade in listedUpgrades)
        {
            Destroy(upgrade);
        }
        foreach (GameObject upgrade in allPossibleUpgrades)
        {
            Destroy(upgrade);
        }
        allPossibleUpgrades.Clear();
        listedUpgrades.Clear();
    }
    private void RandomizeAndDisplayUpgrades()
    {
        if (allPossibleUpgrades.Count < maxNumberOfUpgradesDisplayed)
        {
            listedUpgrades = new List<GameObject>(allPossibleUpgrades);
        }
        else
        {
            List<int> chosen = new List<int>();
            for (int i = 0; i < maxNumberOfUpgradesDisplayed; i++)
            {
                int rand = Random.Range(0, allPossibleUpgrades.Count);
                listedUpgrades.Add(allPossibleUpgrades[rand]);
                allPossibleUpgrades.RemoveAt(rand);
            }
        }
        for (int i = 0; i < listedUpgrades.Count; i++)
        {
            listedUpgrades[i].transform.position = new Vector3(960, 700 - (120 * i), 0);
        }
        if (listedUpgrades.Count == 0)
        {
            ResumePlaying();
        }
    }
   
    private void CreateUpgradeTable()
    {
        
        List<GameObject> items = attackManager.obtainedItems;
        List<GameObject> unlockables = attackManager.unobtainedItems;
        List<GameObject> unlockableAbilities = attackManager.unobtainedActives;


        foreach (GameObject i in items)
        {
            CheckPossibleUpgrades(i);
        }
        
        foreach (GameObject i in unlockables)
        {
            CheckPossibleUnlockables(i);
        }
        foreach (GameObject i in unlockableAbilities)
        {
            CheckPossibleActives(i, items);
        }

    }

    private void CheckPossibleUpgrades(GameObject weapon)
    {
        WeaponInformation weaponInfo = weapon.GetComponent<WeaponInformation>();
        WeaponStat tmr = weaponInfo.timer;
        WeaponStat dmg = weaponInfo.damage;
        WeaponStat nProj = weaponInfo.numProjectiles;
        WeaponStat size = weaponInfo.scale;
        
        if (tmr.currentTier < (tmr.values.Length - 1))
        {
            if (tmr.levelRequirement[tmr.currentTier + 1] <= playerLevel.level)
            {
                GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
                upgrade.GetComponentInChildren<Text>().text = tmr.upgradeDescription;
                upgrade.transform.Find("Icon").GetComponent<Image>().sprite = weaponInfo.icon;
                upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
                upgrade.GetComponent<Button>().onClick.AddListener(() => weapon.GetComponent<WeaponInformation>().timer.UpgradeTier());       
                allPossibleUpgrades.Add(upgrade);
            }
        }
        if (dmg.currentTier < (dmg.values.Length - 1))
        {
            if (dmg.levelRequirement[dmg.currentTier + 1] <= playerLevel.level)
            {
                GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
                upgrade.GetComponentInChildren<Text>().text = dmg.upgradeDescription;
                upgrade.transform.Find("Icon").GetComponent<Image>().sprite = weaponInfo.icon;
                upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
                upgrade.GetComponent<Button>().onClick.AddListener(() => weapon.GetComponent<WeaponInformation>().damage.UpgradeTier()); 
                allPossibleUpgrades.Add(upgrade);
            }
        }
        if (nProj.currentTier < (nProj.values.Length - 1))
        {
            if (nProj.levelRequirement[nProj.currentTier + 1] <= playerLevel.level)
            {
                GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
                upgrade.GetComponentInChildren<Text>().text = nProj.upgradeDescription;
                upgrade.transform.Find("Icon").GetComponent<Image>().sprite = weaponInfo.icon;
                upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
                upgrade.GetComponent<Button>().onClick.AddListener(() => weapon.GetComponent<WeaponInformation>().numProjectiles.UpgradeTier());
                allPossibleUpgrades.Add(upgrade);
            }
        }
        if (size.currentTier < (size.values.Length - 1))
        {
            if (size.levelRequirement[size.currentTier + 1] <= playerLevel.level)
            {
                GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
                upgrade.GetComponentInChildren<Text>().text = size.upgradeDescription;
                upgrade.transform.Find("Icon").GetComponent<Image>().sprite = weaponInfo.icon;
                upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
                upgrade.GetComponent<Button>().onClick.AddListener(() => weapon.GetComponent<WeaponInformation>().scale.UpgradeTier());
                allPossibleUpgrades.Add(upgrade);
            }
        }
    }

    private void CheckPossibleUnlockables(GameObject weapon)
    {
        WeaponInformation weaponInfo = weapon.GetComponent<WeaponInformation>();
        if (weaponInfo.levelRequirement <= playerLevel.level)
        {
            GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
            upgrade.GetComponentInChildren<Text>().text = weaponInfo.weaponDescription;
            upgrade.transform.Find("Icon").GetComponent<Image>().sprite = weaponInfo.icon;
            upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
            upgrade.GetComponent<Button>().onClick.AddListener(() => attackManager.AddToInventory(weapon));
            allPossibleUpgrades.Add(upgrade);
        }
    }

    private void CheckPossibleActives(GameObject activeAbility, List<GameObject> items)
    {
        ActiveAbilityInformation abilityInfo = activeAbility.GetComponent<ActiveAbilityInformation>();
        int index = 0;
        switch (activeAbility.name)
        {
            case "KunaiStorm":
                index = FindIndexOfItemByName(items, "Kunai");
                break;
            case "ChainSickleCyclone":
                index = FindIndexOfItemByName(items, "ChainSicklePivot");
                break;
            case "CharmBarrage":
                index = FindIndexOfItemByName(items, "ExplosiveTag");
                break;
            default:
                Debug.Log(activeAbility.name);
                break;
        }
        if (index != -1)
        {
            if (abilityInfo.requiredUpgrades < items[index].GetComponent<WeaponInformation>().CalculateTotalUpgradeScore())
            {
                GameObject upgrade = Instantiate(upgradeSlot, new Vector3(3000, 3000, 0), Quaternion.identity, upgradeWindow.transform);
                upgrade.GetComponentInChildren<Text>().text = abilityInfo.description;
                upgrade.transform.Find("Icon").GetComponent<Image>().sprite = abilityInfo.icon;
                upgrade.transform.Find("IconFrame").GetComponent<Image>().color = Color.white;
                upgrade.GetComponent<Button>().onClick.AddListener(() => ResumePlaying());
                upgrade.GetComponent<Button>().onClick.AddListener(() => attackManager.AddToActiveAbilities(activeAbility));
                allPossibleUpgrades.Add(upgrade);
            }
        }
    }
    private int FindIndexOfItemByName(List<GameObject> items, string name)
    {
        int index = -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                index = i;
            }
        }
        return index;
    }
}
