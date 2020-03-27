using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager
{
    public enum shootingType
    {
        semiAutomatic,
        burst,
        fullAutomatic
    }

    //the current weapon
    Weapon currentWeapon;

    //the list of all weapon types inn the game
    List<Weapon> weaponTypes;

    WeaponManager(string name)
    {
        weaponTypes = InitializeWeapons();
        currentWeapon = GetWeaponByName(name, weaponTypes);
    }

    private Weapon GetWeaponByName(string name, List<Weapon> weaponTypes)
    {
        Weapon weapon = new Weapon();
        bool found = false;

        foreach (Weapon element in weaponTypes)
        {
            if (element.name == name)
            {
                weapon = element;
                found = true;
                break;
            }
        }

        if (found) return weapon;

        Debug.LogWarning("The weapon name has not been found, changing the weapon to a pistol!!!");
        return GetWeaponByName("pistol", weaponTypes);
    }

    private List<Weapon> InitializeWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        weapons.Add(new Weapon() { name="Pistol", ammo=12, selectiveFire=false, type=shootingType.semiAutomatic });
        weapons.Add(new Weapon() { name="Submachine gun", ammo=30, selectiveFire=true,  type=shootingType.burst });
        weapons.Add(new Weapon() { name="Machine gun", ammo=25, selectiveFire=true,  type=shootingType.fullAutomatic });
        weapons.Add(new Weapon() { name="Sniper rifle", ammo=5, selectiveFire=false,  type=shootingType.semiAutomatic });

        return weapons;
    }

    public struct Weapon
    {
        public string name;
        public int ammo;
        public bool selectiveFire;
        public shootingType type;
    }
}