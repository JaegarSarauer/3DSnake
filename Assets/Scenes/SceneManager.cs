using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public static SceneManager instance;


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
}
