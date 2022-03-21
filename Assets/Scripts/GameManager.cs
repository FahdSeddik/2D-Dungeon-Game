using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //to access gamemanger anywhere
    private void Awake() //to make sure everyone accesses the same instance
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        


        instance = this;
        //will look for all functions in load state and call them once event is fired
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject); 
    }


    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;



    //Refernces to diffrent things such as 
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coins;
    public int experience;


    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);

    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon max level
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false; 

        if(coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }



    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //Max level need to get out of loop
                return r;
        }
        return r; 
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currlevel = GetCurrentLevel();
        experience += xp;
        if(currlevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
    }


    //to save a state
    /*
     INT preferedSkin
     INT coins
     INT experience
     INT weaponLevel
     */
    public void SaveState()
    {
        string st = "";

        //Appedning to string values for each characteristic in state separate by '|'
        st += "0" + "|"; //for preferedSkin
        st += coins.ToString() + "|"; //for coins
        st += experience.ToString() + "|"; //for experience
        st += weapon.weaponLevel.ToString(); //for weapon level
        //Add more in future updates

        PlayerPrefs.SetString("SaveState", st); //takes a key name and a string key here is SaveState
    }

    //to load a state
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        
        if (!PlayerPrefs.HasKey("SaveState"))//then you didnt save before then return
            return;

        /*
         * data array is a string array that will carry information about the player
         * data[0] --> skin number
         * data[1] --> coins
         * data[2] --> experience
         * data[3] --> weapon level
         * to add more in future updates
         */
        string[] data = PlayerPrefs.GetString("SaveState").Split('|'); //needs key 'SaveState' and split it with .Split('|')

        //Change Player skin
        // NOT IMPLEMENTED
        
        //Set coins
        coins = int.Parse(data[1]); //have to use int.Parse() for type conversion form string to int

        //Set experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() !=1)
            player.SetLevel(GetCurrentLevel());

        //Cange weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
