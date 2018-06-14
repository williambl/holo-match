using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    // Use this for initialization
    public void Init () {
        Debug.Log("copying");
        var weapon = GetComponent<Weapon>();
        CopyComponent<Weapon>(weapon, transform.parent.parent.parent.gameObject);
        Destroy(weapon);
    }
	
    //From http://answers.unity.com/answers/589400/view.html
    T CopyComponent<T>(T original, GameObject destination) where T : Component {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
