using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {
    public Button playButton;

    public Button speedButton;
    public enum Speed { SLOW, MEDIUM, FAST }
    public Speed speedMultiplier = Speed.SLOW;
    public Text speedMultiplierText;
    public float realSpeed = .6f;

    public Button SFXButton;
    public Image SFXButtonImage;

    public Button MusicButton;
    public Image MusicButtonImage;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

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

        //load speed multiplier
        speedMultiplier = (Speed)DataHandler.instance.loadData(DataHandler.DataID.SPEED);
        speedMultiplierText.text = "x" + (1 + (int)speedMultiplier);
    }

    public void changeSpeedMultiplier() {
        switch (speedMultiplier) {
            case Speed.SLOW:
                speedMultiplier = Speed.MEDIUM;
                realSpeed = .6f;
                break;
            case Speed.MEDIUM:
                speedMultiplier = Speed.FAST;
                realSpeed = .3f;
                break;
            case Speed.FAST:
                speedMultiplier = Speed.SLOW;
                realSpeed = .9f;
                break;
        }
        speedMultiplierText.text = "x" + (1 + (int)speedMultiplier);
        DataHandler.instance.saveData(DataHandler.DataID.SPEED, (int)speedMultiplier);
    }

    public void playGame() {
        SceneManager.instance.restartGame();
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

}
