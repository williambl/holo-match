using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuController : MonoBehaviour {

    public Button host;
    public Button join;
    public Button exit;
    public InputField joinIP;

    void Start () {
        host.onClick.AddListener(Host);
        join.onClick.AddListener(Join);
        exit.onClick.AddListener(Exit);
    }
	
    void Host () {
        NetworkManager.singleton.StartHost();
    }

    void Join () {
        NetworkManager.singleton.networkAddress = joinIP.text == "" ? "localhost" : joinIP.text;
        NetworkManager.singleton.StartClient();
    }

    void Exit () {
        Application.Quit();
    }
}
