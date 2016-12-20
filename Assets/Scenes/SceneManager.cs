using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    public static SceneManager instance;

    public enum SceneID { MENU, GAME, UNKNOWN }

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void endGame(bool didWin) {
        
    }


    public SceneID getSceneID() {
        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (scene.Equals("Game"))
            return SceneID.GAME;
        if (scene.Equals("MenuScene"))
            return SceneID.MENU;
        return SceneID.UNKNOWN;
    }
}
