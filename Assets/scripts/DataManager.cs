using UnityEngine;
using System.Collections;
using GameStructures;

public class DataManager: MonoBehaviour {

	// Public variables// exists
	public bool updateData;
    
    public int lastscore;
    public int highscore;
	public int score;

	public DataTime timePlayed;

	// Singleton reference
	private static DataManager _instance;

	// Get singleton
	public static DataManager instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<DataManager>();

				DontDestroyOnLoad(_instance.gameObject);
			}

			return _instance;
		}
	}

	// On level load
	void OnLevelWasLoaded () {
		Reset ();
	}

	// Called on game start
	void Awake () {
		// Check if another gameobject exists
		if (_instance == null) {
			_instance = this;

			DontDestroyOnLoad (this);
		} else {
			// Destroys himself if another singleton exists
			if(this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		// Never shutdown screen during game
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		// Variable initialitation
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		// Update values
		if(updateData) {


			// Gametime
			// Count gameplay time
			timePlayed.seconds += Time.deltaTime;
			if(timePlayed.seconds > 60) { timePlayed.seconds -= 60; timePlayed.minutes++; }
			if(timePlayed.minutes > 60) { timePlayed.minutes -= 60; timePlayed.hours++; }
		}
	}

	public void Reset() {
		// Init vars
        score = 0;
        lastscore = PlayerPrefs.GetInt(DataPrefs.LastScore);
        highscore = PlayerPrefs.GetInt(DataPrefs.Highscore);
		timePlayed = new DataTime();

		// Enable data update and set gameoverpanel deactivate
		updateData = true;
	}

	public void AddScore(int Value) {
		if (updateData) {
			score += Mathf.RoundToInt (Value);
		}
	}

	// Save game data when current game ends
	public void SaveData() {
        //Save last score
        lastscore = score;
        PlayerPrefs.SetInt(DataPrefs.LastScore, score);

        // Save highscore
        if(highscore < score) {
            highscore = score;
            PlayerPrefs.SetInt(DataPrefs.Highscore, score);
        }

		// Adds game time to total time
		// Adds seconds with carry then saves the seconds
		if(PlayerPrefs.GetFloat(DataPrefs.SecondsTime) + timePlayed.seconds >= 60) {
			timePlayed.seconds -= 60;
			timePlayed.minutes++;
		}
        PlayerPrefs.SetFloat(DataPrefs.SecondsTime, PlayerPrefs.GetFloat(DataPrefs.SecondsTime) + timePlayed.seconds);
		// Adds minutes with carry then saves de minutes
		if(PlayerPrefs.GetFloat(DataPrefs.MinutesTime) + timePlayed.minutes >= 60) {
			timePlayed.minutes -= 60;
			timePlayed.hours++;
		}
        PlayerPrefs.SetFloat(DataPrefs.MinutesTime, PlayerPrefs.GetFloat(DataPrefs.MinutesTime) + timePlayed.minutes);
		// Save the hours
        PlayerPrefs.SetFloat(DataPrefs.HoursTime, PlayerPrefs.GetFloat(DataPrefs.HoursTime) + timePlayed.hours);

		// Saves the time played as string
        PlayerPrefs.SetString(DataPrefs.TimePlayed, PlayerPrefs.GetFloat(DataPrefs.HoursTime).ToString("F0") + " : "
                              + PlayerPrefs.GetFloat(DataPrefs.MinutesTime).ToString("F0") + " : "
                              + PlayerPrefs.GetFloat(DataPrefs.SecondsTime).ToString("F2")
		);
	}
	
}
