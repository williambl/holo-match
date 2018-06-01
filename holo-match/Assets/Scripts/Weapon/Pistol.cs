using UnityEngine;
using UnityEngine.Networking;

public class Pistol : Weapon {

    [HideInInspector]
    public new EnumAmmoType ammoType = EnumAmmoType.LIGHT;
    [HideInInspector]
    public new EnumWeaponType type = EnumWeaponType.PISTOL;
    [HideInInspector]
    public new EnumFireType fireType = EnumFireType.SEMI_AUTO;

    [HideInInspector]
    public new int ammo = 8;
    [HideInInspector]
    public new int maxAmmo = 8;
    [HideInInspector]
    public new bool infiniteAmmo = false;

    [HideInInspector]
    public new float fireCooldown = 0.1f;
    [HideInInspector]
    public new float reloadTime = 3f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    PlayerController pc;

    new void Start () {
        pc = transform.parent.parent.parent.GetComponent<PlayerController>();
    }

    new void Update () {
        if (!pc.isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire1")) {

            //If we are out of ammo, reload and forbid firing until reload time is over
            if (ammo <= 0 && !infiniteAmmo) {
 
                nextFireTime = Time.time + reloadTime;
                Debug.Log("Time after reload: " + nextFireTime);
 
                Reload();
                return;
            }

            //If we still need to wait until the next fire, then don't do anything
            if (Time.time < nextFireTime)
                return;

            Fire();
        }
    }

    public new void End () {
    }

    public new void Fire () {
        Debug.Log("Firing");
        //Create a new bullet GameObject
        GameObject bullet = (GameObject)Object.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.parent.parent.rotation);
        //Give it a push
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
        //Spawn it on the network
        NetworkServer.Spawn(bullet);
        ammo--;
        nextFireTime = Time.time + fireCooldown; 
    }

    public new void Reload () {
        ammo = maxAmmo;
    }
}
