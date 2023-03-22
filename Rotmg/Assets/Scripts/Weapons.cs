using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/New Weapon", order = 1)]
public class Weapons : ScriptableObject
{
    public string weaponName;

    public int bulletCount;

    public float offset;

    public bool alternating;

    public bool converging;

    public float convergingRange;

    public bool straight;

    public bool diverging;

    public float bulletSpeed;

    public float firerate;

    public GameObject bulletType;


}
