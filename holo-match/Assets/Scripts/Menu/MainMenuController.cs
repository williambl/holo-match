using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuController : MonoBehaviour {

    public Button host;
    public Button join;
    public Button exit;
    public InputField joinIP;

    public Dropdown[] weaponDropdowns = new Dropdown[3];

    void Start () {
        host.onClick.AddListener(Host);
        join.onClick.AddListener(Join);
        exit.onClick.AddListener(Exit);

        List<string> weaponNames = WeaponManager.weaponManager.weaponNames;
        foreach (Dropdown dropdown in weaponDropdowns) {
            dropdown.AddOptions(weaponNames);
        }
    }
	
    void Host () {
        ChooseWeapons();
        NetworkManager.singleton.StartHost();
    }

    void Join () {
        ChooseWeapons();
        NetworkManager.singleton.networkAddress = joinIP.text == "" ? "localhost" : joinIP.text;
        NetworkManager.singleton.StartClient();
    }

    void Exit () {
        Application.Quit();
    }

    void ChooseWeapons () {
        WeaponManager wm = WeaponManager.weaponManager;

        for (int i = 0; i < wm.selectedWeapons.Length; i++) {
            wm.selectedWeapons[i] = weaponDropdowns[i].value;
        }
    }
}
