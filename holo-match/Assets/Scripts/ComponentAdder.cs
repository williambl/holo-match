using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class ComponentAdder : MonoBehaviour {

    public string[] componentNames;
    public string modName;

    void Start () {
        Assembly assembly = ModManager.manager.GetAssemblyFromModName(modName);
        foreach (string componentName in componentNames) {
            gameObject.AddComponent(assembly.GetType(componentName));
        }
    }
    
    public Type GetWeaponComponentType() {
        Assembly assembly = ModManager.manager.GetAssemblyFromModName(modName);
        return assembly.GetType(componentNames[0]);
    }

}
