using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    private string coins300 = "com.gamescreator.inapp.300coins";
    private string removeads = "com.gamescreator.innapp.removeads";

    public GameObject restoreButton; // restore button for iphone onlye


    public void Awake()
    {
        // restore button only i phone ma show hoog ga
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
           

        }
    }



    public void Start()
    {
        if(PlayerPrefs.HasKey("Coins"))
        {
            Debug.Log("Coins key exist");
        }
        else
        {
            Debug.Log("Coins key does not exist");
        }
    }





    // for purchasing successfull
    public void OnPurchasesComplete(Product product) 
    {
        if(product.definition.id == coins300) // for purchase coin
        {
            // agr purchsing successfull hoo jati hai ye code chley
            // yha pey panel wagera  bhe active karwa sktey han or coins store karwa sktey han
            Debug.Log("you have gained 300 coins");



        }
        if(product.definition.id == removeads) // for remove add
        {
            Debug.Log("All ads removed");
        }
    }


 






    // agr purchasing failed ho jati hai too
    public void OnPurchasesFailed(Product product , PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " Failed because " + failureReason);
    }

}
