using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType { Sword, Wand, Bow }
/// <summary>
/// 무기
/// </summary>
[CreateAssetMenu(menuName = "Gear/Weapon")]
public class Weapon : ScriptableObject, IGear
{
    [SerializeField] string weaponName;         // 무기 이름
    [SerializeField] float attackSpeed;         // 무기 공격속도
    [SerializeField] float damage;              // 무기 공격 데미지
    [SerializeField] float attackRange;         // 무기 공격 범위
    [SerializeField] AttackType attackType;     // 무기 공격 애니메이션

    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    
    /// <summary>
    /// 애니메이션 해쉬 값 반환
    /// </summary>
    /// <return>해쉬 값</return>
    public int AttackAnimHash => Animator.StringToHash("Attack");

    public float GetAddAmount() => damage;

}
