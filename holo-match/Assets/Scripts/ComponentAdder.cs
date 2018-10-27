using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ComponentAdder : MonoBehaviour {

    string[] componentNames;
    string modName;

    void Start () {
        Assembly assembly = ModManager.GetAssemblyFromModName(modName);
        foreach (string componentName in componentNames) {
            gameObject.AddComponent(assembly.GetType(component.componentName));
        }
    }

}
