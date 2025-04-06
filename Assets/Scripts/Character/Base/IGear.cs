using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGear
{
    /// <summary>
    /// 캐릭터 스탯 합산 함수
    /// </summary>
    /// <returns>증가하는 량</returns>
    public float GetAddAmount();
}
