using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Selection : MonoBehaviour
{
    public int LevelNo;
    void Start()
    {
         this.gameObject.GetComponent<Button>().interactiable = false   ;
 
        if(!PlayerPrefs.HasKey("UnlockLevel"))
        {
        
      
            PlayerPrefs.SetInt("UnlockLevel", 0);
            PlayerPrefs.Save();
        }

        if(PlayerPrefs.GetInt("UnlockLevel") >= LevelNo)
        {
            this.gameObject.GetComponent<Button>().interactiable = true;
        }
    }

}
