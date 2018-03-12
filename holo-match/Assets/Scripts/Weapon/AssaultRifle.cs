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
    public new float reloadTime = 5f;

    public new GameObject prefab = Resources.Load("Prefabs/assault_rifle") as GameObject;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public new void Init (GameObject player) {
        if (gObject != null)
            return;
        Vector3 spawnPosition = player.transform.position + new Vector3(0, 0.5f, 0);
        gObject = Object.Instantiate(prefab, spawnPosition, player.transform.rotation, player.transform);

        bulletSpawn = gObject.transform.Find("ShootPoint");
    }

    public new void End () {
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
