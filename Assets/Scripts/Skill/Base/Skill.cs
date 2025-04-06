using UnityEngine;

public abstract class Skill : SkillSO
{
    /// <summary>
    /// 스킬의 동작
    /// </summary>
    public abstract void Action(CharacterHandler character, GameObject target = null);
    /// <summary>
    /// UI에 배치하려고 하는 스킬의 정보들
    /// </summary>
    /// <returns></returns>
    public abstract SkillInfo GetSkillData();
}
