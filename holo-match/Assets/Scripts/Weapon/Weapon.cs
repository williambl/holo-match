using UnityEngine;
using UnityEngine.Networking;
using System;

public class Weapon : NetworkBehaviour {

    [System.NonSerialized]
    public string name;

    [System.NonSerialized]
    public EnumSlot slot;

    [System.NonSerialized]
    public EnumAmmoType ammoType;
    [System.NonSerialized]
    public EnumWeaponType type;
    [System.NonSerialized]
    public EnumFireType fireType;

    [System.NonSerialized]
    public int ammo;
    [System.NonSerialized]
    public int maxAmmo;
    [System.NonSerialized]
    public bool infiniteAmmo;

    [System.NonSerialized]
    public float fireCooldown;
    [System.NonSerialized]
    public float nextFireTime;

    [System.NonSerialized]
    public float reloadTime;

    [System.NonSerialized]
    public GameObject weaponGObject;

    [System.NonSerialized]
    public int damage;

    void Start () {}

    void Update () {}

    public void End () {}
    public void Fire () {}
    public void Reload () {}
}
