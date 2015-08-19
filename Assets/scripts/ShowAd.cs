using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class ShowAd : MonoBehaviour {

    private InterstitialAd interstitial;
	private AdRequest request;
	
	private bool isShown = true;

    public GameObject GameOverPanel;

	// Use this for initialization
	void Start () {
    }

	void OnEnable() {
		interstitial = new InterstitialAd("ca-app-pub-3527160935161318/2903007058");
		request = new AdRequest.Builder().Build();
		interstitial.LoadAd(request);
		isShown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isShown && interstitial.IsLoaded()) {
            interstitial.Show();
			isShown = true;
        }
	}
}
