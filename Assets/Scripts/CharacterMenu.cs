using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text leveltxt, hptxt, coinstxt, upgradetxt, xptxt;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite,weaponSprite;
    public RectTransform xpBar;


    //CharacterSelection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //if we went too far away in array of characters
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            //if we went too far away in array of characters
            if (currentCharacterSelection < 0 )
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }


    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }



    //Update character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradetxt.text = "MAX";
        else
            upgradetxt.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //Data
        hptxt.text = GameManager.instance.player.hitpoint.ToString();
        coinstxt.text = GameManager.instance.coins.ToString();
        leveltxt.text = GameManager.instance.GetCurrentLevel().ToString();

        //xp bar
        int currlevel = GameManager.instance.GetCurrentLevel();
        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xptxt.text = GameManager.instance.experience.ToString() + " Total XP"; //display total xp
        }
        else
        {

            int prevLevelXp = GameManager.instance.GetXpToLevel(currlevel - 1);
            int currlevelXp = GameManager.instance.GetXpToLevel(currlevel);

            int diff = currlevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xptxt.text = currXpIntoLevel.ToString() + " / " + diff;

        }

    }
}
