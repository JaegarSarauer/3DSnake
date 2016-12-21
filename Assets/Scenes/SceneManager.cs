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

    public void endGame(bool didWin) {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        SoundManager.instance.playGameMusic(SoundManager.MusicID.MENU);
    }

    public void restartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        SoundManager.instance.playGameMusic(SoundManager.MusicID.GAME);
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
