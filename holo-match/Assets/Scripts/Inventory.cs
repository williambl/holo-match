using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    
    public Weapon weapon0;
    public Weapon weapon1;

    [SyncVar(hook="OnEquippedChange")]
    public int equippedWeapon = 0;

    public Dictionary<EnumAmmoType, int> ammo;

    public Weapon GetEquipped (bool swap = false) {
        switch (equippedWeapon) {
            case 0:
                return swap ? weapon1 : weapon0;
            case 1:
                return swap ? weapon0 : weapon1;
            default:
                return null;
        }
    }

    public void SwapEquipped () {
        equippedWeapon = (equippedWeapon == 0) ? 1 : 0;
    }

    public void UpdateEquipped (Weapon weaponIn, int slot) {
        switch (slot) {
            case 0:
                weapon0 = weaponIn;
                break;
            case 1:
                weapon1 = weaponIn;
                break;
        }
    }

    public void OnEquippedChange (int newEquipped) {
        dynamic prevEquipped = GetEquipped(true);
        prevEquipped.End();

        dynamic equipped = GetEquipped();
        equipped.Init(gameObject);
    }

    public int GetAmmo (EnumAmmoType type) {
        int amount;
        ammo.TryGetValue(type, out amount);
        return amount;
    }

    public void AddAmmo (EnumAmmoType type, int amount) {
        int currentAmount;

        ammo.TryGetValue(type, out currentAmount);
        ammo.Remove(type);

        ammo.Add(type, currentAmount+amount);
    }
}
