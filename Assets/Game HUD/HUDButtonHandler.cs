using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDButtonHandler : MonoBehaviour {
    
    public Button pauseButton;

    public GameObject pauseMenu;
    public Button resumeButton;
    public Button SFXButton;
    public Image SFXButtonImage;
    public Button musicButton;
    public Image MusicButtonImage;
    public Button quitButton;

    public Sprite musicOffSprite;
    public Sprite musicOnSprite;

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
    }

    public void toggleMusic() {
        SoundManager.instance.playMusic = !SoundManager.instance.playMusic;
        if (SoundManager.instance.playMusic)
            MusicButtonImage.sprite = musicOnSprite;
        else
            MusicButtonImage.sprite = musicOffSprite;
    }

    public void quitGame() {
        SceneManager.instance.endGame(false);
    }
}
