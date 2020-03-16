using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon;

    public struct Weapon
    {
        public string name;
        public int ammo;
        public bool selectiveFire;
        public shootingType type;
        public enum shootingType
        {
            semiAutomatic,
            burst,
            fullAutomatic            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
