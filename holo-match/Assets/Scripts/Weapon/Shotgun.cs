using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Weapon {

    [HideInInspector]
    public new EnumAmmoType ammoType = EnumAmmoType.SHOTGUN;
    [HideInInspector]
    public new EnumWeaponType type = EnumWeaponType.SHOTGUN;
    [HideInInspector]
    public new EnumFireType fireType = EnumFireType.SEMI_AUTO;

    [HideInInspector]
    public new int ammo = 2;
    [HideInInspector]
    public new int maxAmmo = 2;
    [HideInInspector]
    public new bool infiniteAmmo = false;

    [HideInInspector]
    public new float fireCooldown = 1f;
    [HideInInspector]
    public new float reloadTime = 3f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    PlayerController pc;

    float bulletSpeed = 100;

    new void Start () {
        pc = GetComponent<PlayerController>();
    }

    new void Update () {
        if (!isLocalPlayer || !weaponGObject.activeInHierarchy || pc.pauseController.isPaused)
            return;

        if (Input.GetButtonDown("Fire1")) {

            //If we are out of ammo, reload and forbid firing until reload time is over
            if (ammo <= 0 && !infiniteAmmo) {
 
                nextFireTime = Time.time + reloadTime;
 
                Reload();
                return;
            }

            //If we still need to wait until the next fire, then don't do anything
            if (Time.time < nextFireTime)
                return;

            Fire();
        }
        pc.ammoText.text = ammo+"/"+maxAmmo;
    }

    public new void End () {
    }

    public new void Fire () {
        CmdInstantiateAndAccelerate();
        ammo--;
        nextFireTime = Time.time + fireCooldown; 
    }

    public new void Reload () {
        ammo = maxAmmo;
    }

    [Command]
    public void CmdInstantiateAndAccelerate () {
        //Create a new bullet GameObject
        GameObject bullet = (GameObject)Object.Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        //Work out the direction to shoot it in
        Ray ray = new Ray(pc.cam.transform.position+pc.cam.transform.forward, pc.cam.transform.forward);
        RaycastHit hit;
        Vector3 direction;
        if (Physics.Raycast(ray, out hit, 100))
            direction = (hit.point-bulletSpawn.position).normalized;
        else
            direction = ((pc.cam.transform.position+pc.cam.transform.forward*100)-bulletSpawn.position).normalized;
        //Give it a push
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        //Spawn it on the network
        NetworkServer.Spawn(bullet);
    }
}