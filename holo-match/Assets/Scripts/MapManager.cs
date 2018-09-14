using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MapManager : NetworkBehaviour {

    public List<Map> mapRegistry = new List<Map>();
    public List<string> mapNames;

    [SyncVar]
    public Map currentMap;

    public static MapManager mapManager;

    void Awake () {
        DontDestroyOnLoad(gameObject);
        mapManager = this;

        AddMaps();
        currentMap = mapRegistry[0];

        foreach (Map map in mapRegistry) {
            mapNames.Add(map.name);
            Debug.Log(map.name);
        }
    }

    private void AddMaps() {
        mapRegistry.Add(new Map("testLevel", "testLevel"));
        mapRegistry.Add(new Map("testlevel1", "testlevel1"));
    }

    public void SwitchMap(Map map) {
        if (!isServer)
            return;

        currentMap = map;
        NetworkManager.singleton.ServerChangeScene(currentMap.sceneName);
    }

    public Map GetMapFromRegistry(int index) {
        return mapRegistry[index];
    }

    public Map GetMapFromRegistry(string name) {
        return mapRegistry.Find(x => x.name == name);
    }
}
