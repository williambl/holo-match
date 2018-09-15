public class CoreMod : HoloMod {

    public string name = "CoreMod";

    public string version = "v0.0.1";

    public override void RegisterWeapons(WeaponManager manager) {
    }

    public override void RegisterMaps(MapManager manager) {
        manager.AddMapToRegistry(new Map("testLevel", "testLevel"));
        manager.AddMapToRegistry(new Map("testLevel1", "testLevel1"));
    }
}
