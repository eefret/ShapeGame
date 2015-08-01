using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class ShowAd : MonoBehaviour {

    InterstitialAd interstitial;
	bool isShown = false;

    public GameObject GameOverPanel;

	// Use this for initialization
    void Start () {

    }

    void OnEnable() {
        interstitial = new InterstitialAd("ca-app-pub-3527160935161318/2903007058");
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);   
        isShown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameOverPanel.activeSelf && !isShown && interstitial.IsLoaded()) {
            interstitial.Show();
			isShown = true;
        }
	}
}
