using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;

public class ModManager : MonoBehaviour {

    private Dictionary<Type, HoloMod> modRegistry = new Dictionary<Type, HoloMod>();
    private List<AssetBundle> assetBundleRegistry = new List<AssetBundle>();

    void Awake () {
        LoadAllMods();

        RegisterMaps(modRegistry);
        RegisterProjectiles(modRegistry);
        RegisterWeapons(modRegistry);
    }

    private void LoadAllMods () {
        IEnumerable<String> modDirs = Directory.EnumerateDirectories(Path.Combine(Application.dataPath, "Mods"));
        foreach (String modDir in modDirs) {
            String modName = new DirectoryInfo(modDir).Name;
            Debug.Log("Looking in " + modDir + ", mod name " + modName);

            if (File.Exists(Path.Combine(modDir, modName+".dll")))
            {
                Debug.Log("(" + modName + ") dll found.");

                Assembly asm = Assembly.Load(File.ReadAllBytes(Path.Combine(modDir, modName+".dll")));

                Debug.Log("(" + modName + ") dll assembly loaded.");

                HoloMod modInstance = null;

                foreach (Type type in asm.GetTypes())
                {
                    Debug.Log("(" + modName + ") found type: " + type.Name);
                    if (type.BaseType == typeof(HoloMod)) {
                        Debug.Log("(" + modName + ") found mod type: " + type.Name + ", registering");
                        modInstance = (HoloMod)Activator.CreateInstance(type);
                        modRegistry.Add(type, modInstance);
                    }
                }

                foreach (string assetBundlePath in Directory.EnumerateFiles(modDir, "*.assetbundle")) {
                    Debug.Log("(" + modName + ") found assetbundle: " + Path.GetFileName(assetBundlePath));
                    AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
                    modInstance.assetBundles.Add(assetBundle);
                    assetBundleRegistry.Add(assetBundle);
                }
            }
        }
    }

    private void RegisterMaps (Dictionary<Type, HoloMod> registry) {
        foreach (var mod in registry) {
            mod.Value.RegisterMaps(MapManager.mapManager);
        } 
    }

    private void RegisterWeapons (Dictionary<Type, HoloMod> registry) {
        foreach (var mod in registry) {
            mod.Value.RegisterWeapons(WeaponManager.weaponManager);
        }
    }

    private void RegisterProjectiles (Dictionary<Type, HoloMod> registry) {
        foreach (var mod in registry) {
            mod.Value.RegisterProjectiles(ProjectileManager.projectileManager);
        }
    }

    public Assembly GetAssemblyFromModName (string modName) {
        foreach (Type type in modRegistry.Keys) {
            if (type.Name == modName)
                return Assembly.GetAssembly(type);
        }
        return null;
    }

}
