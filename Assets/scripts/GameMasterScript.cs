﻿using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    private static int TriangleCount = 0;
    private static int SquareCount = 0;
    private static int maxShapes = 3;
    private static int shapeCount = 0;
    public GameObject Triangle;
    public GameObject Square;
	public GameObject Pentagon;
    public GUIText scoreText;
    private int score;

    public void Start() {
        score = 0;

    }

    public void addScore(int ammount) {
        score += ammount;
        updateScore(score);
    }

    private void updateScore(int score){
        scoreText.text = "Score: " + score;
    }

    

	// Use this for initialization
	void Awake() {
        Instantiate(Triangle, new Vector3(-2, 10, -1), Quaternion.identity);
	}

    void ShapeDestroyed(string shape) {
        switch (shape) {
            case "TRIANGLE":              
            case "SQUARE":
			case "PENTAGON":
                if (shapeCount < maxShapes) {
					int selected = Random.Range(1, 2);
					RandInstantiate(selected);
                }
                break;
        }
    }

    void GameOver() {

        Application.LoadLevel(2);
    }

    void RandInstantiate(int count) {
        bool made = false;
        for (int i = 0; i < count; i++) {
            int range = Random.Range(0, 3);
            if (!made) {
                range = Random.Range(1, 3);
            }
            switch (range) {
                case 0:
                break;
                case 1:
                made = true;
                if (shapeCount < maxShapes) {
                    Instantiate(Triangle, new Vector3(-2, 10, 0), Quaternion.identity);
                }
                break;
                case 2:
                made = true;
                if (shapeCount < maxShapes) {
                    Instantiate(Square, new Vector3(-2, 10, 0), Quaternion.identity);
                }
                break;
				case 3:
				made = true;
				if (shapeCount < maxShapes){
					Instantiate(Pentagon, new Vector3(-2, 10, 0), Quaternion.identity); 
				}
				break;
            }   
        }
    }
}
