using UnityEngine;
using System.Collections;
using GameStructures;

namespace GameStructures {
	
    // Game dificulty struct
    public struct DataPrefs {
        public static string SecondsTime = "SecondsTimePlayedFloat";
        public static string MinutesTime = "MinutesTimePlayedFloat";
        public static string HoursTime = "HoursTimePlayedFloat";
        public static string TimePlayed = "TimePlayedString";
        
        public static string LastScore = "LastScoreInt";
        public static string Highscore = "HighscoreInt";
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