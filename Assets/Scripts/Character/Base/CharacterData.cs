using Firebase.Database;
using System;
using UnityEngine;

/// <summary>
///  캐릭터 데이터
/// </summary>
[System.Serializable]
public class CharacterData
{
    // 캐릭터 이름
    public string Name;
    // 캐릭터 등급
    public int Rarity;
    // 캐릭터 위치
    public CharacterEnum.PositionType Position;
    // 캐릭터 체력
    public float Hp;
    // 캐릭터 공격력
    public float ATK;
    // 캐릭터 방어력
    public float Def;
    // 캐릭터 이동 속도
    public float MoveSpeed;
}
