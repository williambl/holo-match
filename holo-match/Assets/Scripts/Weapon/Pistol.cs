using UnityEngine;
using UnityEngine.Networking;

public class Pistol : Weapon {

    public new EnumAmmoType ammoType = EnumAmmoType.LIGHT;
    public new EnumWeaponType type = EnumWeaponType.PISTOL;
    public new EnumFireType fireType = EnumFireType.SEMI_AUTO;

    public new int ammo = 8;
    public new int maxAmmo = 8;
    public new bool infiniteAmmo = false;

    public new float fireCooldown = 0.1f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public new void Fire () {
        Debug.Log("Firing");
        GameObject bullet = (GameObject)Object.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.parent.parent.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
        NetworkServer.Spawn(bullet);
        ammo--;
    }
}
