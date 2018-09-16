using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;

public class ModManager : MonoBehaviour {

    private List<Type> modRegistry = new List<Type>();
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

                foreach (Type type in asm.GetTypes())
                {
                    if (type.BaseType == typeof(HoloMod))
                        modRegistry.Add(type);
                }

                foreach (string assetBundlePath in Directory.EnumerateFiles(modDir, "*.assetbundle")) {
                    assetBundleRegistry.Add(AssetBundle.LoadFromFile(assetBundlePath));
                }
            }
        }
    }

    private void RegisterMaps (List<Type> registry) {
        foreach (var mod in registry) {
            dynamic modInstance = Activator.CreateInstance(mod); 
            modInstance.RegisterMaps(MapManager.mapManager);
        } 
    }

    private void RegisterWeapons (List<Type> registry) {
        foreach (var mod in registry) {
            dynamic modInstance = Activator.CreateInstance(mod); 
            modInstance.RegisterWeapons(WeaponManager.weaponManager);
        }
    }
}
