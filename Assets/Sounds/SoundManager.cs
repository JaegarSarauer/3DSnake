using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    public bool playSFX = true;
    private bool _playMusic = true;
    public bool playMusic {
        get {
            return _playMusic;
        }
        set {
            _playMusic = value;
            if (!value) {
                stopAllMusic();
            } else {
                playGameMusic(getSceneMusic());
            }
        }
    }

    public enum SFXID { EAT, WORLD_SHIFT, CLICK }
    public AudioSource worldShift;
    public AudioSource eat;
    public AudioSource click;

    public enum MusicID { MENU, GAME, NONE }

    public AudioSource menuMusic;
    public AudioSource gameMusic;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Destroy(this);
    }

    public void Start() {
      /*  DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(worldShift);
        DontDestroyOnLoad(eat);
        DontDestroyOnLoad(menuMusic);
        DontDestroyOnLoad(gameMusic);*/
    }

    public void UIClick() {
        playSound(SFXID.CLICK);
    }

    public void playSound(SFXID sound) {
        if (!playSFX)
            return;
        switch (sound) {
            case SFXID.EAT:
                if (!eat.isPlaying)
                eat.Play();
                break;
            case SFXID.WORLD_SHIFT:
                if (!worldShift.isPlaying)
                    worldShift.Play();
                break;
            case SFXID.CLICK:
                if (!click.isPlaying)
                    click.Play();
                break;
        }
    }

    public void playGameMusic(MusicID sound) {
        if (!playMusic) {
            stopAllMusic();
            return;
        }
        switch (sound) {
            case MusicID.MENU:
                if (!menuMusic.isPlaying)
                    stopAllMusic();
                    menuMusic.loop = true;
                    menuMusic.Play();
                break;
            case MusicID.GAME:
                if (!gameMusic.isPlaying)
                    stopAllMusic();
                    gameMusic.loop = true;
                    gameMusic.Play();
                break;
            case MusicID.NONE:
                break;
        }
    }

    public MusicID getSceneMusic() {
        switch (SceneManager.instance.getSceneID()) {
            case SceneManager.SceneID.GAME:
                return MusicID.GAME;
            case SceneManager.SceneID.MENU:
                return MusicID.MENU;
            case SceneManager.SceneID.UNKNOWN:
            default:
                return MusicID.NONE;
        }
    }

    public void stopAllMusic() {
        if (gameMusic.isPlaying)
            gameMusic.Pause();
        if (menuMusic.isPlaying)
            menuMusic.Pause();
    }
}
