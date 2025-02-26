using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : SkillSO
{
    /// <summary>
    /// 스킬의 종류 중 사용 스킬
    /// </summary>
    public abstract void SkillStart();
    /// <summary>
    /// 스킬의 종류 중 상시 스킬
    /// </summary>
    public abstract void SkillUpdate();
    /// <summary>
    /// 스킬의 동작
    /// </summary>
    public abstract void Action();
    /// <summary>
    /// UI에 배치하려고 하는 스킬의 정보들
    /// </summary>
    /// <returns></returns>
    public abstract SkillData GetSkillData();
}
