using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  캐릭터 캐쉬 데이터 포멧
/// </summary>
[System.Serializable]
public class CharacterData
{
    // 캐릭터 이름
    public string Name;
    // 캐릭터 등급
    public long Rarity;
    // 캐릭터 위치
    public CharacterEnum.PositionType Position;
    // 캐릭터 체력
    public long Hp;
    // 캐릭터 방어력
    public long Def;
    // 캐릭터 공격력
    public long ATK;
    // 캐릭터 공격 타입
    public CharacterEnum.AttackType AttackType;
    // 캐릭터 공격 속도
    public double AttackSpeed;
    // 캐릭터 이동 속도
    public double MoveSpeed;
}
