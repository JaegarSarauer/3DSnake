using UnityEngine;
using System.Collections;

public class KongregateManager : MonoBehaviour {
    public static KongregateManager instance;
    public bool scoreSubmitted = false;
    private bool connected = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }
    }

    public void Start() {
        tryConnection();
    }

    private void tryConnection() {
        Application.ExternalEval(
          @"if(typeof(kongregateUnitySupport) != 'undefined'){
        kongregateUnitySupport.initAPI('KongregateManager', 'OnKongregateAPILoaded');
      };"
        );
    }

    public void OnKongregateAPILoaded(string userInfoString) {
        connected = true;
        OnKongregateUserInfo(userInfoString);
    }

    public void OnKongregateUserInfo(string userInfoString) {
        var info = userInfoString.Split('|');
        var userId = System.Convert.ToInt32(info[0]);
        var username = info[1];
        var gameAuthToken = info[2];
        Application.ExternalEval(
          @"Console.log(Kongregate User Info: " + username + ", userId: " + userId + ");"
        );
        
    }

    public void submitHighscore(int val) {
        /*bool isGuest = Application.ExternalCall("kongregate.services.isGuest");
        Application.Ext*/
        if (scoreSubmitted)
            return;
        if (!connected) {
            tryConnection();
            return;
        }
        scoreSubmitted = true;
        Application.ExternalCall("kongregate.stats.submit", "Highscore", val);
        Application.ExternalCall("kongregate.stats.submit", "Total Score", val);
    }
}
