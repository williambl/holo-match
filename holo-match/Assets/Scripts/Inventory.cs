using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    
    [SyncVar]
    public int equippedWeapon = 0;

    [SyncVar]
    public int weaponCount = 3;

    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;

    void Start () {
        Debug.Log("starting");
        EquipWeapon();
    }

    public void OnStartLocalPlayer () {
        Debug.Log("initialising");
        weapon0.GetComponent<WeaponController>().Init();
        //weapon1.GetComponent<WeaponController>().Init();
        //weapon2.GetComponent<WeaponController>().Init();
    }

    void Update () {
        if (!isLocalPlayer)
            return;

        int previousEquippedWeapon = equippedWeapon;

        //Change weapon by scrolling
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            equippedWeapon++;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            equippedWeapon--;

        //Wraps the equipped weapon around
        equippedWeapon = (equippedWeapon + weaponCount) % weaponCount;

        //If our equipped weapon has changed, update it
        if (previousEquippedWeapon != equippedWeapon)
            EquipWeapon();
    }

    void EquipWeapon () {
        switch (equippedWeapon) {
            case 0:
                weapon0.SetActive(true);
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                break;
            case 1:
                weapon0.SetActive(false);
                weapon1.SetActive(true);
                weapon2.SetActive(false);
                break;
            case 2:
                weapon0.SetActive(false);
                weapon1.SetActive(false);
                weapon2.SetActive(true);
                break;
            default:
                /*
                 * If we ever have an equipped weapon outside of the range
                 * 0..2, then something has gone terribly wrong...
                 */

                Debug.unityLogger.Log(LogType.Error,
                        "You have an equipped weapon outside of the range 0..2! Something has gone terribly wrong...");
                weapon0.SetActive(true);
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                break;
        }
    }
}
