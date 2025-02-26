using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillEnum;

public class SkillSO : ScriptableObject
{
    [Header("스킬 정보")]
    [SerializeField] private SkillData skillData;
    [SerializeField] private SkillType skillType;
    [Header("스킬 상세")]
    [SerializeField] private float skillDamage;
    [SerializeField] private int skillCoolTime;
    [SerializeField] private string animationName;

    #region 프로퍼티
    public SkillData SkillData => skillData;
    public float SkillDamage => skillDamage;
    public int SkillCoolTime => skillCoolTime;
    public string AnimationName => animationName; 
    public SkillType SkillType => skillType;
    #endregion
}
