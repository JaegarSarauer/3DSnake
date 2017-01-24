using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDButtonHandler : MonoBehaviour {
    public static HUDButtonHandler instance;

    public Text scoreText;
    
    public Button pauseButton;

    public GameObject pauseMenu;
    public Button resumeButton;
    public Button SFXButton;
    public Image SFXButtonImage;
    public Button musicButton;
    public Image MusicButtonImage;
    public Button quitButton;

    public GameObject gameOverMenu;
    public Button restartButton;
    public Text endGameScoreText;

    public Sprite musicOffSprite;
    public Sprite musicOnSprite;


    void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Destroy(this);
    }

    void Start() {
        //load SFX setting
        if (SoundManager.instance.playSFX = (DataHandler.instance.loadData(DataHandler.DataID.SFX) == 1) ? true : false)
            SFXButtonImage.sprite = musicOnSprite;
        else
            SFXButtonImage.sprite = musicOffSprite;

        //load Music setting
        if (SoundManager.instance.playMusic = (DataHandler.instance.loadData(DataHandler.DataID.MUSIC) == 1) ? true : false)
            MusicButtonImage.sprite = musicOnSprite;
        else
            MusicButtonImage.sprite = musicOffSprite;

        //initialize the button click sounds
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (var b in buttons) {
            b.onClick.AddListener(() => {
                SoundManager.instance.UIClick();
            });
        }
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (var child in children) {
            Button[] childButtons = child.gameObject.GetComponentsInChildren<Button>(true);
            foreach (var b in childButtons) {
                b.onClick.AddListener(() => {
                    SoundManager.instance.UIClick();
                });
            }
        }
    }

    public void showGameOver() {
        //submit scores here TODO
        scoreText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        endGameScoreText.text = "SCORE: " + GameManager.instance.score;
        gameOverMenu.SetActive(true);
    }

    public void restartGame() {
        SceneManager.instance.restartGame();
    }

    public void togglePause() {
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        if (GameManager.instance.isPaused) {
            pauseButton.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
        } else {
            pauseButton.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }
    
    public void toggleSFX() {
        SoundManager.instance.playSFX = !SoundManager.instance.playSFX;
        if (SoundManager.instance.playSFX)
            SFXButtonImage.sprite = musicOnSprite;
        else
            SFXButtonImage.sprite = musicOffSprite;
        DataHandler.instance.saveData(DataHandler.DataID.SFX, (SoundManager.instance.playSFX) ? 1 : 0);
    }

    public void toggleMusic() {
        SoundManager.instance.playMusic = !SoundManager.instance.playMusic;
        if (SoundManager.instance.playMusic)
            MusicButtonImage.sprite = musicOnSprite;
        else
            MusicButtonImage.sprite = musicOffSprite;
        DataHandler.instance.saveData(DataHandler.DataID.MUSIC, (SoundManager.instance.playMusic) ? 1 : 0);
    }

    public void quitGame() {
        SceneManager.instance.endGame(false);
    }
}
