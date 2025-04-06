using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    [Header("스킬 데이터")]
    [SerializeField] Skill data;

    [Header("프로퍼티")]
    [SerializeField] Animator animator;
    [SerializeField] int index;
    [SerializeField] GameObject target;                 // 공격할 대상, 스킬 애니메이션 중에 공격을 입힐려면 해당 대상을 알고 있어야 해서
    [SerializeField] List<float> coolTimeList;          
    [SerializeField] List<float> currentCoolTimeList;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 스킬 데이터 초기화 함수
    /// </summary>
    public void InitSkillData()
    {
        if (data == null) return;

        coolTimeList = new List<float>();
        currentCoolTimeList = new List<float>();

        coolTimeList.Add(data.Spec.CoolTime);
        currentCoolTimeList.Add(data.Spec.CoolTime);
    }

    private void Update()
    {
        ReduceCoolTime();
    }

    /// <summary>
    /// 스킬 쿨타임 감소 함수
    /// </summary>
    private void ReduceCoolTime()
    {
        for(int i = 0; i < currentCoolTimeList.Count; i++)
        {
            if (currentCoolTimeList[i] <= 0)
            {
                currentCoolTimeList[i] = 0f;
                continue;
            }

            currentCoolTimeList[i] -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 스킬 사용 가능 여부 확인 함수
    /// </summary>
    /// <returns>스킬 사용 가능한 인덱스, 전부 불가능 시 : -1</returns>
    public int CanUseSkill()
    {
        for (int i = 0; i < currentCoolTimeList.Count; i++)
        {
            // 같이 호출이 된다
            if (currentCoolTimeList[i] <= 0f)
            {
                Debug.Log($"{i}번 스킬 사용 가능!");
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 스킬 사용 함수
    /// </summary>
    /// <param name="index">사용할 스킬 인덱스</param>
    public void UseSkill(int index, GameObject target)
    {
       if (index == -1 || target == null) return;
       
       this.index = index;
       this.target = target;

        Debug.Log($"{index} 번 스킬 사용!");

        // 애니메이션 시작
        currentCoolTimeList[index] = coolTimeList[index];
        animator.CrossFade(data.SkillData.AnimName, 0f);

    }

    /// <summary>
    /// 스킬의 피해 타이밍
    /// </summary>
    public void Action() => data.Action(GetComponent<CharacterHandler>(), target);
}
