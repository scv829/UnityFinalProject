using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArmorType { Head, Top, Bottom, Shoes, Gloves }
/// <summary>
/// 방어구
/// </summary>
[CreateAssetMenu(menuName = "Gear/Armor")]
public class Armor : ScriptableObject, IGear
{
    [SerializeField] string armorName;
    [SerializeField] ArmorType armorType;
    [SerializeField] float amount;

    public ArmorType ArmorType => armorType;

    public float GetAddAmount() => amount;
}
