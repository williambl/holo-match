using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

    public List<GameObject> weaponRegistry;
    public List<string> weaponNames;

    public int[] selectedWeapons = new int[3];

    public static WeaponManager weaponManager;

    private List<GameObject> primaryWeapons = new List<GameObject>();
    private List<string> primaryWeaponNames = new List<string>();
    private List<GameObject> secondaryWeapons = new List<GameObject>();
    private List<string> secondaryWeaponNames = new List<string>();
    private List<GameObject> specialWeapons = new List<GameObject>();
    private List<string> specialWeaponNames = new List<string>();

    void Awake () {
        weaponManager = this;

        foreach (GameObject weapon in weaponRegistry) {
            weaponNames.Add(weapon.name);
            Debug.Log(weapon.GetComponent<Weapon>().name);
            Debug.Log(weapon.GetComponent<Weapon>().slot);

            switch (weapon.GetComponent<Weapon>().slot) {
                case EnumSlot.PRIMARY:
                    primaryWeapons.Add(weapon);
                    primaryWeaponNames.Add(weapon.name);
                    break;

                case EnumSlot.SECONDARY:
                    secondaryWeapons.Add(weapon);
                    secondaryWeaponNames.Add(weapon.name);
                    break;

                case EnumSlot.SPECIAL:
                    specialWeapons.Add(weapon);
                    specialWeaponNames.Add(weapon.name);
                    break;
            }
        }
    }

    public List<GameObject> GetWeaponsFromSlot(EnumSlot slot) {
        switch (slot) {
            case EnumSlot.PRIMARY:
                return primaryWeapons;
                break;
            case EnumSlot.SECONDARY:
                return secondaryWeapons;
                break;
            case EnumSlot.SPECIAL:
                return specialWeapons;
                break;
            default:
                return primaryWeapons;
                break;
        }
    }

    public List<string> GetWeaponNamesFromSlot(EnumSlot slot) {
        switch (slot) {
            case EnumSlot.PRIMARY:
                return primaryWeaponNames;
                break;
            case EnumSlot.SECONDARY:
                return secondaryWeaponNames;
                break;
            case EnumSlot.SPECIAL:
                return specialWeaponNames;
                break;
            default:
                return primaryWeaponNames;
                break;
        }
    }

}
