public class Weapon {

    public EnumAmmoType ammoType;
    public EnumWeaponType type;
    public EnumFireType fireType;

    public int ammo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float fireCooldown;
    public float nextFireTime;

    public void Fire () {}
}