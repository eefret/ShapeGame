using UnityEngine;
using System.Collections;
using GameStructures;

namespace GameStructures {
	
    // Game dificulty struct
    public struct DataPrefs {
        // Time
        public static string SecondsTime = "SecondsTimePlayedFloat";
        public static string MinutesTime = "MinutesTimePlayedFloat";
        public static string HoursTime = "HoursTimePlayedFloat";
        public static string TimePlayed = "TimePlayedString";
        // Score
        public static string LastScore = "LastScoreInt";
        public static string Highscore = "HighscoreInt";
        // Options
        public static string MusicVolume = "MusicVolume";
        public static string SoundVolume = "SoundVolume";
    }

	// Time struct
	public struct DataTime {
		public void Main() {
			seconds = 0;
			minutes = 0;
			hours = 0;
		}

		public float seconds;
		public float minutes;
		public float hours;
	}

}