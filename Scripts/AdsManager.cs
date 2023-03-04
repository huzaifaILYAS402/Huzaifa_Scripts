using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class IronSource : MonoBehaviour {
	public static IronSourceIronSource instance;
	public string AdsKey;
	//Place ironsource app key here
	private static string appKey;// = "85460dcd" ; 

	private bool _isRewardedAvailable = false;
	private bool _isBannerAvailable = false;
	// Use this for initialization
	void Awake ()
	{
		appKey = AdsKey;
		//Makes the object not be destroyed automatically when loading a new scene.
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}	
	}
	void Start () {
		Debug.Log("ads --> Start() init with key " + appKey);

		IronSource.Agent.init (appKey,IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.REWARDED_VIDEO);
		// Only for testing
		//IronSource.Agent.validateIntegration();
		
		//Banner Events
		IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
		IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;        
		IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
		IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
		IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
		IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

		//Interstitials Events
		IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

		//Rewarded Events
		IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
    	IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
    	IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
    	IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
    	IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
    	IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
    	IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

		//Load formats
		//Load smart banner (adjust mobile and tablet) in bottom position
	//IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
		IronSource.Agent.loadInterstitial();
		//There is no loadRewardedVideo in ironSource
	}
	//IronSource mandatory
	void OnApplicationPause(bool isPaused) {                 
  		IronSource.Agent.onApplicationPause(isPaused);
	}

	public void showBannerAd(){
        
            // if banner is available, show it, otherwise we try to load for the next call to this method
            if (_isBannerAvailable)
            {
                IronSource.Agent.displayBanner();
            }
            else
            {
                IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            }
        
    }
	public void hideBannerAd(){
		IronSource.Agent.hideBanner();
	}
	public void destroyBannerAd(){
		IronSource.Agent.destroyBanner();//showRewardedVideoAd
		_isBannerAvailable = false;
	}
	public void showInterstitialAd(){
		
			//If interstitial is ready show it, otherwise load again
			if (IronSource.Agent.isInterstitialReady())
			{
				IronSource.Agent.showInterstitial();
			}
			else
			{
				IronSource.Agent.loadInterstitial();
			}
		
	}

	public void ShowIronSourceRewarded(){
		
			if (IronSource.Agent.isRewardedVideoAvailable())
			{
				IronSource.Agent.showRewardedVideo();
			}
			else
			{
				//Tell the user there is no rewarded video available at the moment

			}
		
	}
		
	#region  Banner Events Callbacks
		//Invoked once the banner has loaded
		private void BannerAdLoadedEvent() {
			_isBannerAvailable = true;
			Debug.Log("ads --> Callback: BannerAdLoadedEvent()");
			// TODO
		}
		//Invoked when the banner loading process has failed.
		//@param description - string - contains information about the failure.
		private void BannerAdLoadFailedEvent (IronSourceError error) {
			Debug.Log("ads --> Callback: BannerAdLoadFailedEvent() error: " + error);
			// TODO
		}
		// Invoked when end user clicks on the banner ad
		private void BannerAdClickedEvent () {
			Debug.Log("ads --> Callback: BannerAdClickedEvent()");
			// TODO
		}
		//Notifies the presentation of a full screen content following user click
		private void BannerAdScreenPresentedEvent () {
			Debug.Log("ads --> Callback: BannerAdScreenPresentedEvent()");
			// TODO
		}
		//Notifies the presented screen has been dismissed
		private void BannerAdScreenDismissedEvent() {
			
			// TODO
		}
		//Invoked when the user leaves the app
		private void BannerAdLeftApplicationEvent() {
		
			// TODO
		}

	#endregion

	#region Interstitials Events Callbacks
	//Invoked when the initialization process has failed.
	//@param description - string - contains information about the failure.
	private void InterstitialAdLoadFailedEvent (IronSourceError error) {
		
		//TODO
	}
	//Invoked right before the Interstitial screen is about to open.
	private void InterstitialAdShowSucceededEvent() {
		
		//TODO
	}
	//Invoked when the ad fails to show.
	//@param description - string - contains information about the failure.
	private void InterstitialAdShowFailedEvent(IronSourceError error) {
		
		//TODO
	}
	// Invoked when end user clicked on the interstitial ad
	private void InterstitialAdClickedEvent () {

		//TODO

 	}
	//Invoked when the interstitial ad closed and the user goes back to the application screen.
	private void InterstitialAdClosedEvent () {
	
		// We call load interstitial when the previous one is closed.
		IronSource.Agent.loadInterstitial();
		//TODO

	}
	//Invoked when the Interstitial is Ready to shown after load function is called
	private void InterstitialAdReadyEvent() {

		//TODO

	}
	//Invoked when the Interstitial Ad Unit has opened
	private void InterstitialAdOpenedEvent() {
	
		//TODO

 	}
	#endregion
	
	#region Rewarded Events Callbacks
		//Invoked when the RewardedVideo ad view has opened.
		//Your Activity will lose focus. Please aprivate void performing heavy 
		//tasks till the video ad will be closed.
		private void RewardedVideoAdOpenedEvent() {
		
			//TODO
		}  
		//Invoked when the RewardedVideo ad view is about to be closed.
		//Your activity will now regain its focus.
		private void RewardedVideoAdClosedEvent() {
		
			//TODO		
		}
		//Invoked when there is a change in the ad availability status.
		//@param - available - value will change to true when rewarded videos are available. 
		//You can then show the video by calling showRewardedVideo().
		//Value will change to false when no videos are available.
		private void RewardedVideoAvailabilityChangedEvent(bool available) {
			//Change the in-app 'Traffic Driver' state according to availability.
			_isRewardedAvailable = available;
		
			//TODO	
		}
		//  Note: the events below are not available for all supported rewarded video 
		//   ad networks. Check which events are available per ad network you choose 
		//   to include in your build.
		//   We recommend only using events which register to ALL ad networks you 
		//   include in your build.
		//Invoked when the video ad starts playing.
		private void RewardedVideoAdStartedEvent() {
	
			//TODO	
		}
		//Invoked when the video ad finishes playing.
		private void RewardedVideoAdEndedEvent() {

			//TODO	
		}
	//Invoked when the user completed the video and should be rewarded. 
	//If using server-to-server callbacks you may ignore this events and wait for the callback from the  ironSource server.
	//
	//@param - placement - placement object which contains the reward data
	//
	private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
	{
        if (PlayerPrefs.GetString("reward") == "single")
        {

			WheelManager.instance.Spin();

        }
		if (PlayerPrefs.GetString("reward") == "Double")
		{

			Game_Controller.Instance.ref_dialogue_Handler.wait_Time();

		}
		//canvasmanager.Instance.AddCash();

	}

		//Invoked when the Rewarded Video failed to show
		//@param description - string - contains information about the failure.
		private void RewardedVideoAdShowFailedEvent (IronSourceError error){

		//TODO	
	}

	// for intertial
	// AdsManager.instance.showInterstitialAd();
	// for Rewarded Ads
	/*
	if (IronSource.Agent.isRewardedVideoAvailable())
        {
            AdsManager.instance.ShowIronSourceRewarded();
            PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1);
        } */
	#endregion
}
