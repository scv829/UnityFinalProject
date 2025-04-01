using Firebase.Database;
using System;
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
    [SerializeField] Stat stat;
    // 캐릭터 공격 타입
    public CharacterEnum.AttackType AttackType;
    // 캐릭터 공격 속도
    public double AttackSpeed;
    // 캐릭터 이동 속도
    public double MoveSpeed;
    public double AttackRange;

    public void SetStat(DataSnapshot data, long level)
    {
        Debug.Log("세팅1");
        AttackSpeed = (double)data.Child("/baseStat/AS").Value;
        Debug.Log("세팅1-1");
        MoveSpeed = (double)data.Child("baseStat/MoveSpeed").Value;
        AttackRange = (double)data.Child("baseStat/ATKRange").Value;
        Debug.Log("세팅2");

        Stat stat = new();
        Debug.Log("세팅3");

        stat.ATK = (long)data.Child("baseStat/ATK").Value * (long)Mathf.Pow((float)data.Child("growStat/ATK_Growth").Value, level);
        stat.Def = (long)data.Child("baseStat/DEF").Value * (long)Mathf.Pow((float)data.Child("growStat/DEF_Growth").Value, level);
        stat.Hp = (long)data.Child("baseStat/HP").Value * (long)Mathf.Pow((float)data.Child("growStat/HP_Growth").Value, level);

        this.stat = stat;
        Debug.Log("세팅완!");
    }
}

[System.Serializable]
public struct Stat
{
    // 캐릭터 체력
    public long Hp;
    // 캐릭터 방어력
    public long Def;
    // 캐릭터 공격력
    public long ATK;
}
