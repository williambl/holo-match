using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MapManager : NetworkBehaviour {

    private List<Map> mapRegistry = new List<Map>();
    private List<string> mapNames = new List<string>();

    [SyncVar]
    public Map currentMap;

    public static MapManager mapManager;

    void Awake () {
        DontDestroyOnLoad(gameObject);
        mapManager = this;

        currentMap = mapRegistry[0];
    }

    public void AddMapToRegistry(Map map) {
        mapRegistry.Add(map);
        mapNames.Add(map.name);
    }

    public List<Map> GetMapRegistry() {
        return mapRegistry;
    }

    public List<string> GetMapNames() {
        return mapNames;
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
