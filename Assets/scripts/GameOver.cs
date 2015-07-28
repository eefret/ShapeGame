using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOver : MonoBehaviour {

    InterstitialAd interstitial;
	bool isShown = false;

	// Use this for initialization
	void Start () {
        /*
        interstitial = new InterstitialAd("ca-app-pub-3527160935161318/2903007058");
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        */
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (interstitial.IsLoaded() && !isShown) {
            interstitial.Show();
			isShown = true;
        }
        */
	}
}
