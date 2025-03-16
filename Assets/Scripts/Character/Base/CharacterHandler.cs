using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공통으로 사용할 캐릭터 핸들러
/// </summary>
public class CharacterHandler : MonoBehaviour, IHit
{
    [Header("프로퍼티")]
    [SerializeField] CharacterSO characterData;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer render;
    [SerializeField] BehaviorTree bt;

    [Header("전투")]
    [SerializeField] float currentHp;
    [SerializeField] bool isInitComplete;

    [Header("스킬")]
    [SerializeField] int index;
    [SerializeField] GameObject target;
    [SerializeField] List<float> coolTimeList;
    [SerializeField] List<float> currentCoolTimeList;


    private void Awake()
    {
        coolTimeList = new List<float>();
        currentCoolTimeList = new List<float>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        bt = GetComponent<BehaviorTree>();
        isInitComplete = false;
    }

    private void Start()
    {
        foreach (Skill skill in characterData.CharacterSkill)
        {
            coolTimeList.Add(skill.SkillCoolTime);
            currentCoolTimeList.Add(skill.SkillCoolTime);
        }
        currentHp = characterData.HP;
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

    public float Damage => characterData.Damage;

    private void Update()
    {
        if (IsDied)
        {
            gameObject.layer = LayerMask.NameToLayer("Die");
            animator.CrossFade("Die", 0f);

            NotifyDeath(); // 스테이지 매니저에게 사망 알림
        }
        CoolTime();
    }

    private void CoolTime()
    {
        for (int i = 0; i < currentCoolTimeList.Count; i++)
        {
            if (currentCoolTimeList[i] <= 0f) continue;
            currentCoolTimeList[i] -= Time.deltaTime;
        }
    }

    public bool CanUseSkill(out int index)
    {
        index = -1;
        for (int i = 0; i < currentCoolTimeList.Count; i++)
        {
            // 같이 호출이 된다
            if (currentCoolTimeList[i] <= 0f)
            {
                Debug.Log($"{i}번 스킬 사용 가능!");
                index = i;
                return true;
            }
        }
        return false;
    }

    public void SkillAction(in int index, GameObject target)
    {
        if (index == -1 || target == null) return;

        this.index = index;
        this.target = target;

        // 애니메이션 시작
        animator.CrossFade(characterData.CharacterSkill[index].AnimationName, 0f);
        currentCoolTimeList[index] = coolTimeList[index];
    }

    public void Action() => characterData.CharacterSkill[index].Action(this, target);

    /// <summary>
    /// 캐릭터 생싱 초기화 함수
    /// </summary>
    /// <param name="tag">아군/적군 식별</param>
    public void Init(string tag)
    {
        gameObject.tag = tag;
        render.flipX = gameObject.CompareTag("Enemy");

        // 여기서 데이터를 세팅해야 하나?
        // 
        

        isInitComplete = true;
    }

    public void SetData()
    {
        // 정보 세팅
    }

    private void NotifyDeath()
    {
        // StageManager의 캐릭터 사망 처리 호출
        StageManager.Instance.OnCharacterDeath(this, gameObject.tag);
    }

    public bool IsInitComplete => isInitComplete;
}
