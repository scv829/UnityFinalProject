using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public string StageName;
    public long StageLevel;
    // TODO: 라운드 추가 - 라운드는 스테이지의 연속 생성에 필요한 내용으로 라운드에 등장할 몬스터들을 보야주는 곳
    public List<long> EnemyList;

    public Stage(DataSnapshot snapshot, StringBuilder sb)
    {
        EnemyList = new List<long>();

        // 이름 설정
        sb.Clear();
        sb.Append(snapshot.Child("StageName").Value);
        StageName =  sb.ToString();

        // 스테이지 권장 레벨 설정
        sb.Clear();
        StageLevel = (long)snapshot.Child("StageLevel").Value;

        // 등장하는 적 리스트
        foreach(DataSnapshot data in snapshot.Child("MonsterGroup").Children)
        {
            for(int i = 0; i < (long)data.Child("count").Value; i++)
            {
                EnemyList.Add((long)data.Child("MonsterID").Value);
            }
        }
    }
}
