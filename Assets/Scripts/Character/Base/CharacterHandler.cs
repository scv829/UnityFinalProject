using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공통으로 사용할 캐릭터 핸들러
/// </summary>
public class CharacterHandler : MonoBehaviour, IHit
{
    [Header("프로퍼티")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer render;
    [SerializeField] BehaviorTree bt;

    [Header("스팩")]
    [SerializeField] CharacterSO characterData;
    [SerializeField] float HP;
    [SerializeField] float ATK;
    [SerializeField] float DEF;
    [SerializeField] float moveSpeed;

    [Header("전투")]
    [SerializeField] float currentHp;
    [SerializeField] bool isInitComplete;
    [SerializeField] BattleManager battleManager;

    [Header("스킬")]
    [SerializeField] SkillHandler skillHandler;

    [Header("장비")]
    [SerializeField] Armor[] armors;
    [SerializeField] Weapon weapon;

    public BattleManager BattleManager { 
        set 
        { 
            battleManager = value; 
        } 
    }

    public CharacterSO Data
    {
        set { if(value != null) characterData = value; }
    }


    /// <summary>
    /// 캐릭터 생싱 초기화 함수
    /// </summary>
    /// <param name="tag">아군/적군 식별</param>
    public void Init(string tag)
    {
        gameObject.tag = tag;
        render.flipX = gameObject.CompareTag("Enemy");

        foreach(Armor armor in armors)
        {
            // 착용 방어구에 따른 스탯 추가량
            switch (armor.ArmorType)
            {
                case ArmorType.Head:
                case ArmorType.Shoes:
                    HP = characterData.Hp += armor.GetAddAmount();
                    break;
                case ArmorType.Top:
                case ArmorType.Bottom:
                    DEF = characterData.Def += armor.GetAddAmount();
                    break;
                case ArmorType.Gloves:
                    ATK = characterData.ATK += armor.GetAddAmount();
                    break;
            }
        }

        moveSpeed = characterData.MoveSpeed;

        currentHp = characterData.Hp;

        skillHandler.InitSkillData();

        isInitComplete = true;
    }

    public int SpawnPos => (int)characterData.Position;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        bt = GetComponent<BehaviorTree>();
        skillHandler = GetComponent<SkillHandler>();
        isInitComplete = false;
    }

    public bool TakeDamage(GameObject requester, BattleEnum.DamageType type, float damage)
    {
        currentHp -= damage;

        return IsDied;
    }

    /// <summary>
    /// 사망 여부 확인
    /// </summary>
    public bool IsDied => currentHp <= 0;

    public float Damage => (ATK + weapon.GetAddAmount());

    private void Update()
    {
        if (IsDied)
        {
            // 사망 로직
            battleManager.OnDied(gameObject);

            gameObject.layer = LayerMask.NameToLayer("Die");
            animator.CrossFade("Die", 0f);
        }
    }

    /// <summary>
    /// 사용 가능한 스킬 여부 확인 호출 함수
    /// </summary>
    /// <param name="index">사용가능 한 스킬 인덱스</param>
    /// <returns>사용 가능 여부</returns>
    public bool CanUseSkill(out int index)
    {
        index = skillHandler.CanUseSkill();
        return (index.Equals(-1)) ? false : true;
    }

    /// <summary>
    /// 스킬 사용 호출
    /// </summary>
    /// <param name="index">사용할 스킬의 인덱스</param>
    /// <param name="target">공격을 맞을 타겟</param>
    public void UseSkill(in int index, GameObject target) => skillHandler.UseSkill(index, target);

    public bool IsInitComplete => isInitComplete;

    public float AttackRange => weapon.AttackRange;
    public float AttackSpeed => weapon.AttackSpeed;
    public int AttackAnimHash => weapon.AttackAnimHash;
}
