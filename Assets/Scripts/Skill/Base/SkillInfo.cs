using System;
using UnityEngine;

/// <summary>
/// 스킬(기본)
/// </summary>
[Serializable]
public class SkillInfo
{
    // 스킬 이름
    [SerializeField] private string skillName;
    // 스킬 아이콘
    [SerializeField] private Sprite skillIcon;
    // 스킬 설명
    [SerializeField] private string skillDescription;
    // 스킬 사용 텍스트
    [SerializeField] private string skillUsingText;
    // 스킬 연출
    [SerializeField] private string animName;
    // 컷신 연출
    [SerializeField] private int cutId;

    #region 프로퍼티
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public Sprite SkillIcon => skillIcon;
    public string SkillUsingText => skillUsingText;
    public string AnimName => animName;
    public int CutId => cutId;
    #endregion
}
