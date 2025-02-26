using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공통으로 사용할 캐릭터 핸들러
/// </summary>
public class CharacterHandler : MonoBehaviour, IHit
{
    [SerializeField] CharacterSO characterData;

    [SerializeField] private float currentHp;

    private void Start()
    {
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
        if (IsDied) { gameObject.layer = LayerMask.NameToLayer("Die"); }
    }
}
