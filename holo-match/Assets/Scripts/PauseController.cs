using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PauseController : NetworkBehaviour {

    private Canvas pauseCanvas;
    private Button resume;
    private Button exit;

    public bool isPaused = false;

    void Awake () {
        pauseCanvas = transform.Find("PauseCanvas").GetComponent<Canvas>();
        resume = pauseCanvas.transform.Find("Resume").GetComponent<Button>();
        exit = pauseCanvas.transform.Find("Exit").GetComponent<Button>();
        pauseCanvas.enabled = false;
    }

    public override void OnStartLocalPlayer () {
        resume.onClick.AddListener(Resume);
        exit.onClick.AddListener(Exit);
    }

    void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Cancel")) {
            isPaused = !isPaused;
            pauseCanvas.enabled = isPaused;
        }
    }

    void Resume () {
        isPaused = false;
        pauseCanvas.enabled = false;
    }

    void Exit () {
        if (isServer)
            NetworkManager.singleton.StopHost();
        else
            NetworkManager.singleton.StopClient();
    }
}
