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
}
