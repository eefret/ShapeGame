using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RefreshGameStat : MonoBehaviour {
    
    public enum RefreshData {
        CurrentScore,
        Lastscore,
        Highscore,
    };
    
    private Text guiTextUI;

    [Multiline(2)]
    public string preText = "";
    public RefreshData refreshData;
    [Multiline(2)]
    public string posText = "";
    
    // Use this for initialization
    void Start () {
        guiTextUI = GetComponent<Text> ();
    }
    
    // Update is called once per frame
    void Update () {
        guiTextUI.text = preText;
        
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
        
        guiTextUI.text += posText;
    }
}