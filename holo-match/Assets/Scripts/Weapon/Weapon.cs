using UnityEngine;
using System;

public class Weapon : MonoBehaviour {

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

    void Start () {}

    void Update () {}

    public void End () {}
    public void Fire () {}
    public void Reload () {}
}
