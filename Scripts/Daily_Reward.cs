using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class Daily_Reward : MonoBehaviour
{



	public AudioSource ards;
	public AudioClip click;
	public AudioClip Notifictiaon;

	public GameObject Daily_Reward_Button;

	public GameObject Daily_Bonus_Dialouge;
	public GameObject Reward_Dialouge;

	public Text Dialouge_Text_Heading;
	public Text Dialouge_Text_Description;


	public Image Reward_Image;

	
	public Text claim_reward_text;
	public Text timer_text;

	
	// current date 
	DateTime FutureDate;
	DateTime CurrentDate;

	// all the value which you want to set
	//but we are setting these values in one of public method 
	int i_Years , i_Months , i_Days , i_Hours , i_Minutes , i_Seconds ;
	string s_Years , s_Months , s_Days , s_Hours , s_Minutes , s_Seconds ;


	// type of reward in enum
	// make reference where you want to choose;
	public enum item_Type {None,Coins,Cash, Gems, Medikit, Armors,Missiles,AirStrikes};


	bool canClaimReward = false;
	bool isrewardClaimed = false;
	bool isRewardedDialougeActive;



	int button_Clicked_Id;

	[Serializable]
	public class Get_Daily_Bonus{
		
		public item_Type selected_Item;
		public int bonus_Quantity;
		public Button bonus_Button;
		public string bonus_day_string;
		public Text bonus_day_Text;
		public Sprite noraml_Sprite;
		public Sprite claim_Sprite;
		public Sprite reward_Sprite;


	}
	public Get_Daily_Bonus[] ref_Get_Daily_Bonus;

	[Space]
	public GameObject ref_Dialogue;


	string stringFutureDate;
	DateTime TempFutureDate;


	void OnEnable(){


	
		//		Debug.Log ("on enabled called");
		//Cristmiss_Special.SetActive (true);


	}


	public void ref_OpenPoPUP(){
//		Cristmiss_Special.SetActive (true);
	}

	void Start()
	{

		//Invoke(nameof(wait), 0.8f);
		button_Clicked_Id = 0;
	
		isRewardedDialougeActive = true;

//		Daily_Bonus_Dialouge.SetActive (false);
		Reward_Dialouge.SetActive (false);
		Reward_Image.enabled = false;




		stringFutureDate = PlayerPrefs.GetString ("FutureDate", "");
		DateTime.TryParse (stringFutureDate, out TempFutureDate);
		CurrentDate = System.DateTime.Now;

		FutureDate = TempFutureDate;

		CheckDailyBonus ();



	}

	public void wait()
    {
		ards.PlayOneShot(Notifictiaon);
		Daily_Bonus_Dialouge.SetActive(true);
	}
	void Update(){

		CurrentDate = System.DateTime.Now;


		//Debug.Log ("current time "+CurrentDate+" future time "+FutureDate);
		// to get the time difference between two DateTime varialbes

		TimeSpan travelTime = FutureDate - CurrentDate; 

		if (CurrentDate > FutureDate ) {
			
			canClaimReward = true;
			claim_reward_text.enabled = true;
			timer_text.enabled = false;
//			Debug.Log ("time is up");

			//Daily_Reward_Button.gameObject.SetActive (true);
			//Daily_Reward_Button.gameObject.GetComponent<Button> ().interactable = true;
			//Daily_Reward_Button.gameObject.GetComponent<Animator> ().enabled = true;

//			claim_reward_text.text = "You Can Claim Reward";
		} else {

			if (isRewardedDialougeActive) {


				claim_reward_text.enabled = false;
				timer_text.enabled = true;
				timer_text.text = "Remaining Time :   " + travelTime.Hours + ":" + travelTime.Minutes + ":" + travelTime.Seconds;
				canClaimReward = false;

			
			}
			//Daily_Reward_Button.gameObject.SetActive (false);

			Debug.Log ("time is running");
		}

 


	} 

	// call this method first then call the startcounter method on the one button 
	// this will get the button id and then we proceed accordingly
	public void ButtonId(int iD){
		
		button_Clicked_Id = iD;
		Debug.Log ("The button id = " + button_Clicked_Id);
	}





	public void StartCounter(int hours){

		//converting hours into seconds
		int seconds = hours* 3600;

		//Game_Controller.Instance.ref_soundController._BtnSound(); // huzaifa you comment this


		// all converstion is due to the formate of set DateTime


		if (canClaimReward) {


			PlayerPrefs.SetString ("FutureDate", "");

			FutureDate = System.DateTime.Now;

//			Debug.Log ("Future date before the calculating the values is = " + FutureDate);

			i_Years = 0;
			i_Months = 0;
			i_Days = 0;
			i_Hours = 0;
			i_Minutes = 0;
			i_Seconds = seconds;
			//first is adding the date in the futuredate
			FutureDate = FutureDate.AddYears (i_Years);
			FutureDate = FutureDate.AddMonths (i_Months);
			FutureDate = FutureDate.AddDays (i_Days);
			FutureDate = FutureDate.AddHours (i_Hours);
			FutureDate = FutureDate.AddMinutes (i_Minutes);
			FutureDate = FutureDate.AddSeconds (i_Seconds);

			//converting current date into string 

			s_Years = FutureDate.Year.ToString ();
			s_Months = FutureDate.Month.ToString ();
			s_Days = FutureDate.Day.ToString ();
			s_Hours = FutureDate.Hour.ToString ();
			s_Minutes = FutureDate.Minute.ToString ();
			s_Seconds = FutureDate.Second.ToString ();
//			Debug.Log ("String values" + s_Years + " " + s_Months + " " + s_Days + " " + s_Hours + " " + s_Minutes + " " + s_Seconds);

			//now converting string int to int

			i_Years = int.Parse (s_Years);
			i_Months = int.Parse (s_Months);
			i_Days = int.Parse (s_Days);
			i_Hours = int.Parse (s_Hours);
			i_Minutes = int.Parse (s_Minutes);
			i_Seconds = int.Parse (s_Seconds);

			//calling method to save the future_date in playerprefes
			SetDate ();
			//calling the method to handle the dailybonus sprites
			HandlingTheBonuses ();
			// handling the sprite and texts according to the playerprefes
			CheckDailyBonus ();

			// add to the invetory to call these two lines
			string ItemName = ref_Get_Daily_Bonus[button_Clicked_Id].selected_Item.ToString();
			ards.PlayOneShot(click);
			//HUZAIFA CALL ADS
			
            addInventory (ItemName);

			// calling unity ads here

			//			Advertisement.Show(null, new ShowOptions {
			//				resultCallback = result => {
			//					Debug.Log(result.ToString());
			//				}
			//			});

			/*GameObject.Find ("Admob").GetComponent<GoogleMobileAdsDemoScript> ().show_rewarded_Video_Admob_Unity ();*/


			//GameObject.Find("Ads_Controller").gameObject.GetComponent<Ads_Call_Manager>().Show_Rewarded_Unity_Google();

		} else {
		
			isrewardClaimed = false;
			RewardHandler ();
		}
	}


	void SetDate(){
		
	


		stringFutureDate = PlayerPrefs.GetString ("FutureDate", "");

		// setting time and save into playerprefes

		if (stringFutureDate == "") {
			
			//setting the future time for once 
			FutureDate = new DateTime (i_Years, i_Months, i_Days, i_Hours, i_Minutes, i_Seconds);

//			Debug.Log ("Future date first Time is = " + FutureDate);

			//setting player prefes to store the date 
			PlayerPrefs.SetString ("FutureDate", FutureDate.ToString ());
			stringFutureDate = PlayerPrefs.GetString ("FutureDate", "");
			DateTime.TryParse (stringFutureDate, out TempFutureDate);

	

		} else {
		
			stringFutureDate = PlayerPrefs.GetString ("FutureDate", "");
			DateTime.TryParse (stringFutureDate, out TempFutureDate);



		}
		 
		FutureDate = TempFutureDate;

//		ref_no 1-4129815904 5:00PM Wajahad 12-9-2016
//		ref_no no.. Rimsha  10:00AM 12-13-2016
	}




	public void ButtonClicked(string name){

//		Debug.Log ("name is " + name);
		StartCoroutine(butttonClick(name));

	}
	IEnumerator butttonClick(string name) {

        /*Game_Controller.Instance.ref_soundController._BtnSound(); */// huzaifa you comment this

        yield return new WaitForSeconds(0.1f);

		switch (name) {


		case "Back":
				ards.PlayOneShot(click);
				//ref_Dialogue.gameObject.GetComponent<Animator>().enabled = true;
				//ref_Dialogue.gameObject.GetComponent<Animator>().Play("Backward");
				//Invoke(nameof(_Disable), 0.4f);


				Daily_Bonus_Dialouge.SetActive (false);
//			Cristmiss_Special.SetActive (false);
			break;
		case "OK":
				ards.PlayOneShot(click);

				Reward_Dialouge.SetActive (false);
			isRewardedDialougeActive = true;
			break;
		
		

		}
	}



	void HandlingTheBonuses(){
	
	
		// changing playerprefes to active next dailybonus and do interectable false for remaining 
		if (Get_Reward_Item() == ref_Get_Daily_Bonus.Length - 1) {

			//because its adding in the value in the saved playerprefs 
			// therefore we are deducting the total value
			// we are not setting as Set_Reward_Item (0);
			Set_Reward_Item (-(ref_Get_Daily_Bonus.Length - 1));

		} else {

			//every time add one to the current reward item
			Set_Reward_Item (1);
		}

		// which type of reward you want to add
		// write your code here 
		isrewardClaimed = true;
		RewardHandler ();

	
	}



	//a popup come to show that time is remaining 
	void RewardHandler(){
	
		if (isrewardClaimed) {


			Reward_Dialouge.SetActive (true);
			Reward_Image.enabled = true;
//			Dialouge_Text_Heading.text = "Congratulations";
//			Dialouge_Text_Description.text = "You have awarded with "+ ref_Get_Daily_Bonus [button_Clicked_Id].bonus_Quantity+" coins."+"Please Come Back Soon. Thanks!";
			Dialouge_Text_Heading.text = "CONGRATULATIONS!";
			Dialouge_Text_Description.text = "You got successfully reward,\n Come back Next Day.";

			

			isRewardedDialougeActive = false;
		} else {
			
			Reward_Dialouge.SetActive (true);
			Reward_Image.enabled = false;

			Dialouge_Text_Heading.text = "Attention!";
			Dialouge_Text_Description.text = "You Can Not Collect Reward .\n Please Try Again Soon.";

		}
	

	}


	void addInventory(string ItemName){
	
		int tempQuantity = ref_Get_Daily_Bonus [button_Clicked_Id].bonus_Quantity;
//		Debug.Log ("the quantity is " + tempQuantity);
		// add your all inventroy here
		switch (ItemName) {

		case "Coins":
				//			Debug.Log (" coins added successfully :) ");

				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + tempQuantity);
				break;
		case "Cash":
			/*Game_Controller.Instance.ref_Store_Inventory.Set_Total_Gems(tempQuantity);*/ // huzaifa you comment this
//			Debug.Log (" Cash added successfully :) ");
			break;
		case "Medikit":
			/*Game_Controller.Instance.ref_Store_Inventory.Set_Madikit(tempQuantity); */ // huzaifa you comment this
//			Debug.Log (" Medikit added successfully :) ");
			break;
		case "Armors":
//			Debug.Log (" Armors added successfully :) ");
			break;
		case "AirStrikes":
//			Debug.Log (" AirStrikes added successfully :) ");
			break;
		case "Missiles":
//			Debug.Log (" Missiles added successfully :) ");
			break;

		default:
			break;
		}
	
	}



	void CheckDailyBonus(){


		int j = Get_Reward_Item();


		// setting interactables only for buttons
		for (int i=0; i<ref_Get_Daily_Bonus.Length; i++){

			if (i == j) {
				
				ref_Get_Daily_Bonus [i].bonus_Button.GetComponent<Button> ().interactable = true;


			} else {

				ref_Get_Daily_Bonus [i].bonus_Button.GetComponent<Button> ().interactable = false;
			}


			ref_Get_Daily_Bonus [i].bonus_day_Text.text = ref_Get_Daily_Bonus [i].bonus_day_string;


		}

		changeSprite ();

	}


	void changeSprite(){

		int j = Get_Reward_Item();

		// chaining the sprites only
		for (int i=0; i<ref_Get_Daily_Bonus.Length; i++){

			if (i <= j-1) {

				ref_Get_Daily_Bonus [i].bonus_Button.GetComponent<Image> ().sprite = ref_Get_Daily_Bonus [i].claim_Sprite;
				Reward_Image.sprite = ref_Get_Daily_Bonus [i].reward_Sprite;
//				Debug.Log("selected item is type of this "+ref_Get_Daily_Bonus[i].selected_Item);


			}
			else {

				ref_Get_Daily_Bonus [i].bonus_Button.GetComponent<Image> ().sprite = ref_Get_Daily_Bonus [i].noraml_Sprite;
			}


		}

	}


	// getter setter for daily bonus 
	public void Set_Reward_Item(int value){
	
		PlayerPrefs.SetInt ("Coins", Get_Reward_Item()+value);
	
	}

	public int Get_Reward_Item(){
	
		return PlayerPrefs.GetInt ("Coins", 0);
	}
	private void _Disable()
	{
		ref_Dialogue.gameObject.GetComponent<Animator>().enabled = false;
		Daily_Bonus_Dialouge.gameObject.SetActive(false);

	}

}