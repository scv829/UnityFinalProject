using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillEnum;

/// <summary>
/// 스킬(스팩)
/// </summary>
[System.Serializable]
public struct SkillSpec
{
    // 스킬 쿨타임
    public float CoolTime;
    // 공격력 기반 데미지 비율
    public float DamageRatio;
    // 방어력 기반 방어력 비율
    public float DefenseRatio;
    // 공격력 기반 힐량 비율
    public float HealRatio;
}
