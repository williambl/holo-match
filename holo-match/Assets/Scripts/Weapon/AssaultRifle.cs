using UnityEngine;
using UnityEngine.Networking;

public class AssaultRifle : Weapon {

    [HideInInspector]
    public new EnumAmmoType ammoType = EnumAmmoType.MEDIUM;
    [HideInInspector]
    public new EnumWeaponType type = EnumWeaponType.ASSAULT_RIFLE;
    [HideInInspector]
    public new EnumFireType fireType = EnumFireType.AUTO;

    [HideInInspector]
    public new int ammo = 30;
    [HideInInspector]
    public new int maxAmmo = 30;
    [HideInInspector]
    public new bool infiniteAmmo = false;

    [HideInInspector]
    public new float fireCooldown = 0.1f;
    [HideInInspector]
    public new float reloadTime = 5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    PlayerController pc;

    new void Start () {
        pc = GetComponent<PlayerController>();
    }

    new void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetButton("Fire1")) {

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
        Debug.Log("firing?");
        CmdInstantiateAndAccelerate();
        ammo--;
        nextFireTime = Time.time + fireCooldown; 
    }

    public new void Reload () {
        ammo = maxAmmo;
    }

    [Command]
    public void CmdInstantiateAndAccelerate () {
        Debug.Log("FIRING");

        //Create a new bullet GameObject
        GameObject bullet = (GameObject)Object.Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        //Give it a push
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 18;
        //Spawn it on the network
        NetworkServer.Spawn(bullet);
    }
}
