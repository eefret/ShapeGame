using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour {

    public delegate void SubmitCallback(bool Success);

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate();
		
        Social.localUser.Authenticate(ProcessAuthentication);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowLeaderboard() {
        if(Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI1MaelJUCEAIQCQ");
        } else {
            Social.localUser.Authenticate(ProcessAuthentication);
        }
    }

    public void AddScoreLeaderboard(int Score, SubmitCallback Callback) {
        if(Social.localUser.authenticated) {
            Social.ReportScore(Score, "CgkI1MaelJUCEAIQCQ", (bool success) => {
                Callback(success);
            });
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI1MaelJUCEAIQCQ");
        } else {
            Social.localUser.Authenticate(ProcessAuthentication);
            Callback(false);
        }
    }
    
    void ProcessAuthentication(bool success) {
        if(success) {
            Debug.Log("Authentication done");
        } else {
            Debug.Log("Authentication failed!");
        }
    }
}
