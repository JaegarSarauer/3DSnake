using UnityEngine;
using System.Collections;

public class DataHandler : MonoBehaviour {
    public static DataHandler instance;

    public enum DataID { SFX, MUSIC, HIGHSCORE, SPEED }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Destroy(this);
    }

    public void saveData() {
        PlayerPrefs.Save();
    }

    public void saveData(DataID data, int value) {
        switch (data) {
            case DataID.SFX:
                PlayerPrefs.SetInt("SFX", value);
                break;
            case DataID.MUSIC:
                PlayerPrefs.SetInt("MUSIC", value);
                break;
            case DataID.HIGHSCORE:
                PlayerPrefs.SetInt("HIGHSCORE", value);
                break;
            case DataID.SPEED:
                PlayerPrefs.SetInt("SPEED", value);
                break;
            default:
                break;
        }
        saveData();
    }

    public int loadData(DataID data) {
        switch(data) {
            case DataID.SFX:
                return PlayerPrefs.GetInt("SFX");
            case DataID.MUSIC:
                return PlayerPrefs.GetInt("MUSIC");
            case DataID.HIGHSCORE:
                return PlayerPrefs.GetInt("HIGHSCORE");
            case DataID.SPEED:
                return PlayerPrefs.GetInt("SPEED");
            default:
                return 0;
        }
    }


}
