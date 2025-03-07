using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    [Header("생성 위치")]
    [Tooltip("Top -> Bottom")]
    [SerializeField] Transform[] point;
    [Header("생성된 수")]
    [SerializeField] int count;

    private int[] pos = new int[5] { 3, 2, 4, 1 ,5 };

    private void Awake()
    {
        count = 0;
    }

    /// <summary>
    /// 위치를 설정해주는 함수
    /// </summary>
    /// <returns>설정할 위치를 준다</returns>
    public Vector3 GetPos()
    {
        // 3 -> 2 -> 4 -> 1 -> 5
        return (count >= pos.Length ) ? Vector3.zero : point[pos[count++]].position;
    }

}
