using System;
using UnityEngine;

[Serializable]
public struct SkillData
{
    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;
    [SerializeField] private Sprite skillIcon;

    #region 프로퍼티
    public string SkillDescription => skillDescription;
    public string SkillName => skillName;
    public Sprite SkillIcon => skillIcon;
    #endregion
}
