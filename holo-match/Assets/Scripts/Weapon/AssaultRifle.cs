using UnityEngine;
using UnityEngine.Networking;

public class AssaultRifle : Weapon {

    public new EnumAmmoType ammoType = EnumAmmoType.MEDIUM;
    public new EnumWeaponType type = EnumWeaponType.ASSAULT_RIFLE;
    public new EnumFireType fireType = EnumFireType.AUTO;

    public new int ammo = 30;
    public new int maxAmmo = 30;
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
