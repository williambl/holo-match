using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    
    private PauseController pauseController;

    [SyncVar]
    public int equippedWeapon = 0;

    [SyncVar]
    public int weaponCount = 3;

    public GameObject[] weapons = new GameObject[3];

    void Start () {
        EquipWeapon();
        pauseController = GetComponent<PauseController>();

        foreach (GameObject weapon in weapons) {
            MoveWeapon(weapon, gameObject);
        }
    }

    void MoveWeapon(GameObject original, GameObject target) {
        var origWeapon = original.GetComponentInChildren<Weapon>();
        var newWeapon = CopyComponent<Weapon>(origWeapon, target);
        newWeapon.weaponGObject = original;
        Destroy(origWeapon);
    }

    //From http://answers.unity.com/answers/589400/view.html
    T CopyComponent<T>(T original, GameObject destination) where T : Component {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }

    void Update () {
        if (!isLocalPlayer || pauseController.isPaused)
            return;

        int previousEquippedWeapon = equippedWeapon;

        //Change weapon by scrolling
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            equippedWeapon++;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            equippedWeapon--;

        if (Input.GetButtonDown("Weapon 0"))
            equippedWeapon = 0;
        if (Input.GetButtonDown("Weapon 1"))
            equippedWeapon = 1;
        if (Input.GetButtonDown("Weapon 2"))
            equippedWeapon = 2;

        //Wraps the equipped weapon around
        equippedWeapon = (equippedWeapon + weaponCount) % weaponCount;

        //If our equipped weapon has changed, update it
        if (previousEquippedWeapon != equippedWeapon)
            EquipWeapon();
    }

    void EquipWeapon () {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(i == equippedWeapon);
        }
    }
}
