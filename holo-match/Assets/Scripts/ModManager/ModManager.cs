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
        RegisterWeapons(modRegistry);
    }

    private void LoadAllMods () {
        IEnumerable<String> modDirs = Directory.EnumerateDirectories(Path.Combine(Application.dataPath, "Mods"));
        foreach (String modDir in modDirs) {
            String modName = Path.GetDirectoryName(modDir);

            if (File.Exists(Path.Combine(modDir, modName+".dll")))
            {
                Assembly asm = Assembly.Load(File.ReadAllBytes(Path.Combine(modDir, modName+".dll")));

                HoloMod modInstance = null;

                foreach (Type type in asm.GetTypes())
                {
                    if (type.BaseType == typeof(HoloMod)) {
                        modInstance = (HoloMod)Activator.CreateInstance(type);
                        modRegistry.Add(type, modInstance);
                    }
                }

                foreach (string assetBundlePath in Directory.EnumerateFiles(modDir, "*.assetbundle")) {
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
}
