using UnityEngine;
using static SkillEnum;

public class SkillSO : ScriptableObject
{
    [Header("스킬 정보")]
    [SerializeField] private SkillInfo skillData;
    [SerializeField] private SkillType skillType;
    [Header("스킬 상세")]
    [SerializeField] private SkillSpec skillSpec;

    #region 프로퍼티
    public SkillInfo SkillData => skillData;
    public SkillType SkillType => skillType;
    public SkillSpec Spec => skillSpec;
    #endregion
}
