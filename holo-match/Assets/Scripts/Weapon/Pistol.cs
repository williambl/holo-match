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

    public new GameObject prefab = Resources.Load("Prefabs/pistol") as GameObject;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public new void Init (GameObject player) {
        if (gObject != null)
            return;
        Vector3 spawnPosition = player.transform.position + new Vector3(0, 0.5f, 0.5f);
        gObject = Object.Instantiate(prefab, spawnPosition, player.transform.rotation, player.transform);

        bulletSpawn = gObject.transform.Find("ShootPoint");
    }

    public new void End () {
        Debug.Log("ending");
        GameObject.Destroy(gObject);
        gObject = null;
        bulletSpawn = null;
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
