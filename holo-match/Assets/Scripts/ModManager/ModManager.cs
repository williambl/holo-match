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
    }
}
