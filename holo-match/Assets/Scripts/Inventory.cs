using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    
    [SyncVar]
    public int equippedWeapon = 0;

    [SyncVar]
    public int weaponCount = 3;

    void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            equippedWeapon++;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            equippedWeapon--;

        //Wraps the equipped weapon around
        equippedWeapon = (equippedWeapon + weaponCount) % weaponCount; 
    }
}
