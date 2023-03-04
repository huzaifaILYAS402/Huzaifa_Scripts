using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store_Manager : MonoBehaviour
{
    /* (Alhamdullilah)
      This script is attached to store screen game object
      
     */
    public Text Show_Coins;
    public int selectedCar = 0;
    public Text Show_Car_Name;
    public Text Show_Car_Price;

    #region Instance of this class
    public static Store_Manager Instance;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
        Player_Prfs();
    }
    #endregion

  

    public GameObject[] Car;
    public string[] CarName;
    public int[] CarPrice;

    public void Start()
    {
        
        _Game_Button_Clicked(); // button clicked functionality here
        Active_Object(selectedCar); // for active first object
        
    }

    private void Update()
    {
        Show_Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void Player_Prfs()
    {
        if(!PlayerPrefs.HasKey("SelectedCar"))
        {
            PlayerPrefs.SetInt("SelectedCar", 0);
            PlayerPrefs.Save();
        }



        if (!PlayerPrefs.HasKey("SelectedCar"))
        {
            PlayerPrefs.SetInt(CarName[selectedCar] , 0);
            PlayerPrefs.Save();
        }

        if(PlayerPrefs.GetInt(CarName[0]) == 0)
        {
            PlayerPrefs.SetInt(CarName[0], 1);
            Buy.gameObject.SetActive(false);
            Play.gameObject.SetActive(true);
            Debug.Log("here");
           
        }

        else
        {
            BuyItems();
        }
    }

    public void Active_Object(int value)
    {
        foreach (var item in Car)
        {
            item.SetActive(false);

        }

        Car[value].SetActive(true);
        Show_Car_Name.text = CarName[value].ToString();
        Show_Car_Price.text = CarPrice[value].ToString();
    }

    #region Update UI
    public void UpdateUI(int value)
    {
        switch (value)
        {
            case 0: // for next
                if (selectedCar >= Car.Length - 1)
                {
                    selectedCar = 0;

                }

                else
                {
                    selectedCar++;

                }

               
                break;
            case 1: // for previous

                if (selectedCar <= 0)
                {
                    selectedCar = Car.Length - 1;

                }

                else
                {

                    selectedCar--;

                }

                break;
        }

        Active_Object(selectedCar);
        Show_Car_Name.text = CarName[selectedCar].ToString();
        Show_Car_Price.text = CarPrice[selectedCar].ToString();
        BuyItems();
    }

    #endregion
    #region Buy Item
    
    public void BuyItems()
    {
        Debug.Log("Buy items");
        if(PlayerPrefs.GetInt("Coins") >= CarPrice[selectedCar])
        {
            if(PlayerPrefs.GetInt(CarName[selectedCar]) == 1)
            {
                Buy.gameObject.SetActive(false);
                Play.gameObject.SetActive(true);
            }

            else
            {
                Buy.gameObject.SetActive(true);
                Play.gameObject.SetActive(false);
            }
            
        }

        else
        {
            Buy.gameObject.SetActive(false);
            Play.gameObject.SetActive(true);
        }
    }
    #endregion
    #region button Clicked Functions Here

    public Text Item_Name;
    public Button Next, Buy, Previous, Play , Back;
    public void _Game_Button_Clicked()
    {
        Next.onClick.AddListener(delegate
        {

            _Button_Clicked("Next");
        });

        Buy.onClick.AddListener(delegate
        {

            _Button_Clicked("Buy");
        });

        Previous.onClick.AddListener(delegate
        {

            _Button_Clicked("Previous");
        });

        Play.onClick.AddListener(delegate
        {

            _Button_Clicked("Play");
        });

        Back.onClick.AddListener(delegate
        {

            _Button_Clicked("Back");
        });
    }

    public void _Button_Clicked(string  name)
    {
        StartCoroutine(_button_clicked(name));
    }
     IEnumerator _button_clicked(string name)
    {
        yield return new WaitForSeconds (0.1f);
        Debug.Log("Button pressed" + name);
        switch (name)
        {
            case "Next":
                UpdateUI(0);
                break;

            case "Buy":
                PlayerPrefs.SetInt(CarName[selectedCar], 1);
                Buy.gameObject.SetActive(false);
                Play.gameObject.SetActive(true);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - CarPrice[selectedCar]);
         
                break;
            case "Previous":
                UpdateUI(1);
                
                Active_Object(selectedCar);
                break;
            case "Play":
                SceneManager.LoadSceneAsync("GP");
                PlayerPrefs.SetInt("SelectedCar", selectedCar);
                break;
            case "Back":
                this.gameObject.SetActive(false);
                UI_Controller.Instance.MainScreen.SetActive(true);
                //UI_Controller.Instance.StoreScreen.SetActive(false);
                break;
        }
    }
    #endregion
}
