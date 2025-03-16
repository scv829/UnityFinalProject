using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 몬스터 및 플레이어 캐릭터 생성하는 매니저
/// </summary>
public class SpawnManager : MonoBehaviour
{

    [Header("캐릭터")]
    [SerializeField] CharacterContainerSO containerSO;
    [SerializeField] List<CharacterHandler> players;
    [SerializeField] List<CharacterHandler> enemies;

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
    }

    public void PlayerInit(List<long> playerCharacters)
    {
        foreach (var item in players)
        {
           // for (int i = 0; i < item.y; i++) SetPlayerCharacter((int)item);
        }
    }

    public void EnemyInit(List<long> enemyCharacters)
    {
        foreach (var item in enemyCharacters)
        {
            SetEnemyCharater(item);
        }
    }

    /// <summary>
    /// 플레이어 캐릭터 세팅 함수
    /// </summary>
    /// <param name="index">캐릭터 ID</param>
    private void SetPlayerCharacter(int index)
    {
        CharacterHandler character = Instantiate(containerSO.GetCharacter(index));
        character.Init("Player");
        // 위치 설정을 어떻게?
        character.gameObject.transform.position = playerSpawnPos[0].GetPos();
    }

    /// <summary>
    /// 적 캐릭터 세팅 함수
    /// </summary>
    /// <param name="index">캐릭터 ID</param>
    private void SetEnemyCharater(long index)
    {
        CharacterHandler character = Instantiate(containerSO.GetCharacter((int)index));
        character.SetData();
        
        character.gameObject.transform.position = EnemySpawnPos[2].GetPos();
        character.Init("Enemy");
    }

}
