using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ProjectileManager : NetworkBehaviour {

    private List<GameObject> projectileRegistry = new List<GameObject>();
    private List<string> projectileNames = new List<string>();

    public static ProjectileManager projectileManager;

    void Awake () {
        DontDestroyOnLoad(gameObject);
        projectileManager = this;
    }

    public void AddProjectileToRegistry(GameObject projectile) {
        projectileRegistry.Add(projectile);
        projectileNames.Add(projectile.name);
    }

    public List<GameObject> GetProjectileRegistry() {
        return projectileRegistry;
    }

    public List<string> GetProjectileNames() {
        return projectileNames;
    }

    public GameObject GetProjectileFromRegistry(int index) {
        return projectileRegistry[index];
    }

    public GameObject GetProjectileFromRegistry(string name) {
        return projectileRegistry.Find(x => x.name == name);
    }
}
