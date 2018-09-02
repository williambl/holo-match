using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyMenuController : MonoBehaviour {

    public Button play;
    public Dropdown mapDropdown;

    void Start () {
        play.onClick.AddListener(Play);

        mapDropdown.AddOptions(MapManager.mapManager.mapNames);
    }
	
    void Play () {
        MapManager mapManager = MapManager.mapManager;
        mapManager.SwitchMap(mapManager.mapRegistry[mapDropdown.value]);
    }

}
