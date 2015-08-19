using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour {

    private const string GOOGLEPLAY_SHORTURL = "https://goo.gl/m7c5Rw";
    private const string GOOGLEPLAY_URL = "https://play.google.com/store/apps/details?id=com.firstcrow.convergeapp";

    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";

    private const string FACEBOOK_ADDRESS = "https://www.facebook.com/dialog/feed?app_id=1522024844684912";

    //=http://www.mysexyurl.com&p[summary]=mysexysummaryhere&p[images][0]=http://www.urltoyoursexyimage.com
        
        // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FacebookShareScore() {
        #if UNITY_ANDROID
        Application.OpenURL(FACEBOOK_ADDRESS +
                            "&display=popup&caption=" + WWW.EscapeURL("I got " + DataManager.instance.score + " points in Converge") +
                            "&link=" + GOOGLEPLAY_URL + 
                            "&redirect_uri=" + GOOGLEPLAY_URL);
        #endif
    }

    public void TwitterShareScore() {
        #if UNITY_ANDROID
        Application.OpenURL(TWITTER_ADDRESS +
                            "?text=" + WWW.EscapeURL("I got " + DataManager.instance.score + " points in Converge ; " + GOOGLEPLAY_SHORTURL) +
                            "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE) + ";hashtags=ConvergeApp");
        #endif
    }
}
