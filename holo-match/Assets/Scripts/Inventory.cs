using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    
    public Weapon weapon0;
    public Weapon weapon1;

    [SyncVar(hook="OnEquippedChange")]
    public int equippedWeapon = 0;

    public Dictionary<EnumAmmoType, int> ammo;

    public Weapon GetEquipped () {
        switch (equippedWeapon) {
            case 0:
                return weapon0;
            case 1:
                return weapon1;
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
        OnEquippedChange(equippedWeapon);
    }

    public void OnEquippedChange (int newEquipped) {
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
