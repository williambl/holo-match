public abstract class HoloMod {

    public string name;

    public string version;

    public abstract void RegisterWeapons(WeaponManager manager);

    public abstract void RegisterMaps(MapManager manager);
}
