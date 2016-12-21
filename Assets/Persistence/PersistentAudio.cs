using UnityEngine;
using System.Collections;

public class PersistentAudio : MonoBehaviour {
    public static PersistentAudio instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else {
            DestroyObject(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
