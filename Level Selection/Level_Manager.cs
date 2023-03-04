using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level_Manager : MonoBehaviour
{



    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            
            PlayerPrefs.SetInt("Level" , 0);
            PlayerPrefs.Save();
        }
        }
    public void Level_Load(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadSceneAsync("GP");
    }


    #region Next Button code
    public void Next_Clicked()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel") + 1);
        SceneManager.LoadSceneAsync("GP");
        if (PlayerPrefs.GetInt("Level") >= 8)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }

    #endregion
}
