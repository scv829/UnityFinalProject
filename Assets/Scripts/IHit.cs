using UnityEngine;
using static BattleEnum;

public interface IHit
{
    /// <summary>
    /// 데미지 함수
    /// </summary>
    /// <param name="requester">요청자</param>
    /// <param name="type">공격의 종류</param>
    /// <param name="damage">데미지</param>
    public void TakeDamage(GameObject requester, DamageType type, float damage);
}
