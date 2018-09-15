using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class ModManager : MonoBehaviour {

    private List<Type> modRegistry = new List<Type>();

    void Awake () {

        foreach(var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in asm.GetTypes())
            {
                if (type.BaseType == typeof(HoloMod)) {
                    Debug.Log(type.Name);
                    modRegistry.Add(type);
                }
            }
        }

        RegisterMaps(modRegistry);
        RegisterWeapons(modRegistry);
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
