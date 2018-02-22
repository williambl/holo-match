public interface IWeapon {

    EnumAmmoType ammoType;
    EnumWeaponType type;

    int ammo;
    int maxAmmo;
    bool infiniteAmmo;

    void Fire ()
}
