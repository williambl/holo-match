public class Inventory {
    
    public Weapon weapon0;
    public Weapon weapon1;

    public int equippedWeapon = 0;

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
    }
}
