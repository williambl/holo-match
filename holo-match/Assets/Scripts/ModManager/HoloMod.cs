﻿using System.Collections.Generic;
using UnityEngine;

public abstract class HoloMod {

    public string name;

    public string version;

    public List<AssetBundle> assetBundles = new List<AssetBundle>();

    public abstract void RegisterWeapons(WeaponManager manager);

    public abstract void RegisterMaps(MapManager manager);

    public abstract void RegisterProjectiles(ProjectileManager manager);
}
