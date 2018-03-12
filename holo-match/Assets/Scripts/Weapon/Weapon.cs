using UnityEngine;

public class Weapon {

    public EnumAmmoType ammoType;
    public EnumWeaponType type;
    public EnumFireType fireType;

    public Mesh mesh;

    public int ammo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float fireCooldown;
    public float nextFireTime;

    public float reloadTime;

    public void Fire () {}
    public void Reload () {}
}
