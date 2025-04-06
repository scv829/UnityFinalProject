using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterEnum;


[CreateAssetMenu(menuName = "Character/Data")]
public class CharacterSO : ScriptableObject
{
    /* 기존 코드
    [SerializeField] int character_id;
    [SerializeField] string character_name;
    [SerializeField] int character_star;
    [SerializeField] string character_description;
    [SerializeField] AttackType character_type;
    [SerializeField] float hp;
    [SerializeField] float damage;
    [SerializeField] List<Skill> character_skill;
    
    #region 프로퍼티
    public string Name => character_name;
    public int CharacterId => character_id;
    public string CharacterName => character_name;
    public int CharacterStar => character_star;
    public string CharacterDescription => character_description;
    public AttackType CharacterType => character_type;
    public float HP => hp;
    public float Damage => damage;
    public List<Skill> CharacterSkill => character_skill;
    #endregion
    */

    // 캐릭터 이름
    public string Name;
    // 캐릭터 등급
    [Range(1, 5)]
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
