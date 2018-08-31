using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

    public List<Map> mapRegistry;
    public List<string> mapNames;

    public Map currentMap;

    public static MapManager mapManager;

    void Awake () {
        mapManager = this;

        foreach (Map map in mapRegistry) {
            mapNames.Add(map.name);
        }
    }

    public void SwitchToMap(Map map) {
        SceneManager.LoadSceneAsync(map.sceneName);
    }

    public Map GetMapFromRegistry(int index) {
        return mapRegistry[index];
    }

    public Map GetMapFromRegistry(string name) {
        return mapRegistry.Find(x => x.name == name);
    }
}
