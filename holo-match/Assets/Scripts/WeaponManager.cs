using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponManager : MonoBehaviour {

    private List<GameObject> weaponRegistry = new List<GameObject>();
    private List<string> weaponNames = new List<string>();

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
    }

    public void AddWeaponToRegistry(GameObject weapon) {
        weaponRegistry.Add(weapon);
        weaponNames.Add(weapon.name);

        dynamic weaponComponent = Activator.CreateInstance(weapon.GetComponent<ComponentAdder>().GetWeaponComponentType());

        if (weaponComponent.slot == EnumSlot.PRIMARY) {
            primaryWeapons.Add(weapon);
            primaryWeaponNames.Add(weapon.name);
        }

        if (weaponComponent.slot == EnumSlot.SECONDARY) {
            secondaryWeapons.Add(weapon);
            secondaryWeaponNames.Add(weapon.name);
        }

        if (weaponComponent.slot == EnumSlot.SPECIAL) {
            specialWeapons.Add(weapon);
            specialWeaponNames.Add(weapon.name);
        }

    }

    public List<GameObject> GetWeaponRegistry() {
        return weaponRegistry;
    }

    public GameObject GetWeaponFromRegistry(EnumSlot slot, int index) {
        switch (slot) {
            case EnumSlot.PRIMARY:
                return primaryWeapons[index];
                break;
            case EnumSlot.SECONDARY:
                return secondaryWeapons[index];
                break;
            case EnumSlot.SPECIAL:
                return specialWeapons[index];
                break;
            default:
                return primaryWeapons[index];
                break;
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
