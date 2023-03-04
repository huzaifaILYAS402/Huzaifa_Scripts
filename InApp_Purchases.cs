using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InApp_Purchases : MonoBehaviour {





	[Serializable]
	public class ref_IAP_Meta
	{

		public Text Product_Name;
		public Text Product_Price;
		public Text Product_Quantity;

	}
	public ref_IAP_Meta[] ref_IAP_Data; 


	void SetDefaultValues(){
		for (int i = 0; i < ref_IAP_Data.Length; i++) {

			ref_IAP_Data[i].Product_Name.text = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data[i].Product_Name;
			ref_IAP_Data[i].Product_Price.text = "$"+Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [i].Product_Price.ToString();
			ref_IAP_Data [i].Product_Quantity.text = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [i].Product_Quantity.ToString();

		}
		// tariq you can also get values from google play if required
//		for (int i = 0; i < ref_IAP_Data.Length; i++) {
//			ref_IAP_Data [i].Product_Price = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [i].Product_Price.ToString()+"$";
//			ref_IAP_Data [i].Product_Quantity = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [i].Product_Quantity.ToString();
//		}
	}

	void OnEnable(){
		SetDefaultValues ();
	}

	public void ItemClicked(int Product_Id){
		StartCoroutine(ItemClick(Product_Id));
	}

	IEnumerator ItemClick(int Product_Id){

		Game_Controller.Instance.ref_soundController._BtnSound();
		yield return new WaitForSeconds (0.1f);

//		int currentprice = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [Product_Id].Product_Price;
		int currentQuantity = Game_Controller.Instance.ref_Store_Inventory.ref_IAP_Data [Product_Id].Product_Quantity;
		if (Application.isEditor) {
			Game_Controller.Instance.ref_Store_Inventory.Set_Total_Gold (currentQuantity);
		} else {
			switch (Product_Id) {
			// tariq add SKU's here
			case 0:

				break;
			case 1:

				break;
			case 2:

				break;
			case 3:

				break;
			case 4:

				break;
			case 5:

				break;
			default:
				break;

			}
		}
			

	}
}
