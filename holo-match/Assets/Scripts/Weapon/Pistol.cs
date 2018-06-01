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
    public new float reloadTime = 3f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    new void Start () {
    }

    new void Update () {
    }

    public new void End () {
    }

    public new void Fire () {
        Debug.Log("Firing");
        GameObject bullet = (GameObject)Object.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.parent.parent.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
        NetworkServer.Spawn(bullet);
        ammo--;
    }

    public new void Reload () {
        ammo = maxAmmo;
    }
}
