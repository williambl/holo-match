using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

    public List<GameObject> weaponRegistry;
    public List<string> weaponNames;

    public static WeaponManager weaponManager;

    void Awake () {
        weaponManager = this;

        foreach (GameObject weapon in weaponRegistry) {
            weaponNames.Add(weapon.name);
        }
    }

}
