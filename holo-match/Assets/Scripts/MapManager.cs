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

        foreach (Map map in mapRegistry) {
            mapNames.Add(map.name);
        }
    }

    void Update () {
        if (!isServer)
            return;

        if (Input.GetKeyDown("I")) {
            Debug.Log("Switching Map!");
            SwitchMap(mapRegistry[0]);
        }
    }

    private void AddMaps() {
        mapRegistry.Add(new Map("testLevel", "testLevel"));
    }

    public void SwitchMap(Map map) {
        if (!isServer)
            return;

        currentMap = map;
        RpcSwitchToMap(map);
    }

    [ClientRpc]
    public void RpcSwitchToMap(Map map) {
        SceneManager.LoadSceneAsync(map.sceneName);
    }

    public Map GetMapFromRegistry(int index) {
        return mapRegistry[index];
    }

    public Map GetMapFromRegistry(string name) {
        return mapRegistry.Find(x => x.name == name);
    }
}
