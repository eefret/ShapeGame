using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RefreshGameStat : MonoBehaviour {

    // Enum
    public enum RefreshData {
        CurrentScore,
        Lastscore,
        Highscore,
    };

    // Public
    [Multiline(2)] // 2 lines on inspector
    public string preText = "";
    [Multiline(2)] // 2 lines on inspector
    public string posText = "";

    public RefreshData refreshData;

    // Private
    private Text guiTextUI;

    // Use this for initialization
    void Start () {
        guiTextUI = GetComponent<Text> ();
    }
    
    // Update is called once per frame
    void Update () {
        // Setup preText
        guiTextUI.text = preText;

        // Add stat
        switch (refreshData) {
            case RefreshData.CurrentScore:
                guiTextUI.text += DataManager.instance.score.ToString();
                break;
            case RefreshData.Highscore:
                guiTextUI.text += DataManager.instance.highscore.ToString();
                break;
            case RefreshData.Lastscore:
                guiTextUI.text += DataManager.instance.lastscore.ToString();
                break;
            default:
                guiTextUI.text += "";
                break;
        }

        // Add posText
        guiTextUI.text += posText;
    }
}