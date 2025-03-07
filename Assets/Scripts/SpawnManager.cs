using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 캐릭터 생성 매니저
/// </summary>
public class SpawnManager : MonoBehaviour
{
    // TODO
    // 캐릭터 생성 및 배치
    // 1. 아마 생성 요청을 하고 정보를 넘겨준다
    // 2. 정보에는 해당 스테이지의 수, 유저의 캐릭터, 상대 캐릭터
    // 3. 유저의 캐릭터를 스폰 위치에 배치
    // 4. 상대 캐릭터를 스폰 위치에 배치
    // 5. BattleManage에게 전투 시작 요청

    [Header("캐릭터")]
    [SerializeField] CharacterContainerSO containerSO;
    [SerializeField, Tooltip("x : Id, Y : 수")] List<Vector2> players;
    [SerializeField, Tooltip("x : Id, Y : 수")] List<Vector2> enemies;

    [Header("플레이어 생성 위치")]
    [SerializeField] SpawnPos[] playerSpawnPos;

    [Header("적 생성 위치")]
    [SerializeField] SpawnPos[] EnemySpawnPos;

    private void Start()
    {
        // 1. DB에 웨이브 및 출전 캐릭터 ID 배열 받기
        // player = GetData();
        // enemies = GetData();

        // 2. Database에서 가져온 친구들의 데이터를 불러오기?
        // 3. 불러온 캐릭터들의 데이터 설정
        foreach(var item in players)
        {
            for (int i = 0; i < item.y; i++) SetPlayerCharacter((int)item.x);
        }

        foreach (var item in enemies)
        {
            for (int i = 0; i < item.y; i++) SetEnemyCharater((int)item.x);
        }
    }

    private void SetPlayerCharacter(int index)
    {
        CharacterHandler character = Instantiate(containerSO.GetCharacter(index));
        character.Init("Player");
        character.gameObject.transform.position = playerSpawnPos[0].GetPos();
    }

    private void SetEnemyCharater(int index)
    {
        CharacterHandler character = Instantiate(containerSO.GetCharacter(index));
        character.gameObject.transform.position = EnemySpawnPos[2].GetPos();
        character.Init("Enemy");
    }

}
