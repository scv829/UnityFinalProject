using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// 몬스터 및 플레이어 캐릭터 생성하는 매니저
/// </summary>
public class SpawnManager : MonoBehaviour
{

    [Header("주입")]
    [SerializeField] BattleManager battleManager;

    [Header("캐릭터 컨테이너")]
    [SerializeField] CharacterContainerSO containerSO;
    [Header("적")]
    [SerializeField] string data;

    [Header("플레이어 생성 위치")]
    [SerializeField] SpawnPos[] playerSpawnPos;

    [Header("적 생성 위치")]
    [SerializeField] SpawnPos[] EnemySpawnPos;



    public void SpawnPlayerCharacter(int id, CharacterSO data, ref List<GameObject> playerList)
    {
        // DataManger가 호출
        // 플레이어 캐릭터 생성
        CharacterHandler character = Instantiate(containerSO.GetCharacter(id));
        // 데이터 설정
        character.Data = data;
        // BattleManager 생성
        character.BattleManager = battleManager;
        // 생존 리스트에 추가
        playerList.Add(character.gameObject);
        // 설정 완료
        character.Init("Player");
    }

    public void SpawnEnemyCharacter(Round round, ref List<GameObject> enemyList)
    {
        // StageManager가 호출
        // 라운드의 인덱스를 받아서 생성
        // 적 캐릭터 생성

        CharacterHandler character;

        foreach (int id in round.enemyList)
        {
            character = Instantiate(containerSO.GetCharacter(id));

            character.gameObject.transform.position = EnemySpawnPos[character.SpawnPos].GetPos();
            character.BattleManager = battleManager;

            enemyList.Add(character.gameObject);

            character.Init("Enemy");
        }
    }

    public void ResetPlayerSpawnPosCount()
    {
        foreach(var item in playerSpawnPos) item.ResetPosCount();
    }

    public void ResetPlayerCharacterPos(GameObject requester)
    {
        requester.transform.position = playerSpawnPos[requester.GetComponent<CharacterHandler>().SpawnPos].GetPos();
    }
}
