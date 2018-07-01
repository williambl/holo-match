using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

    public List<GameObject> weaponRegistry;

    public static WeaponManager weaponManager;

    void Start () {
        weaponManager = this;
    }

}
