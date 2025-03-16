using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 스테이지 관리(진입, 전환, 캐릭터 관리 등)하는 매니저
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("스테이지 정보")]
    [SerializeField] long id;
    [SerializeField] int currentStage;
    [SerializeField] int lastStage;
    [Header("적 정보")]
    [SerializeField] int currentEnemy;
    [SerializeField] int totalEnemy;
    [SerializeField] List<Stage> enemyCharacters;
    [Header("플레이어 정보")]
    [SerializeField] int currentPlayer;
    [SerializeField] int totalPlayer;
    [SerializeField] List<CharacterHandler> playerCharacters;
    [Header("캐릭터 생성")]
    [SerializeField] SpawnManager spawnManager;

    private DatabaseReference data;

    private StringBuilder sb;

    private void Awake()
    {
        // 싱글톤 사용 -> BT에서 접근하기 위해
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerCharacters = new List<CharacterHandler>();

        sb = new StringBuilder();
        currentStage = 0;
    }

    private void Start()
    {
        // 데이터 불러오는 코루틴
        StartCoroutine(DataConnectRoutine());
    }

    private IEnumerator DataConnectRoutine()
    {
        yield return null;
        // 데이터를 불러올 수 있을 때 까지 대기
        while (true)
        {
            try
            {
                data = DataManager.Database.RootReference;
            }
            catch
            {
                continue;
            }

            break;
        }

        // 스테이지 데이터를 불러오기
        GetStageData();
        // 플레이어 데이터 불러오기
        GetPlayerData();
    }

    private void GetPlayerData()
    {
        data = DataManager.Database.RootReference.Child($"User/1/Characters");

        // DeckManager가 UI를 담당하고 있다가 들어온 결과를 DataManager의 리스트에 전달
        // 전달 후 GetPlayerData()에서 리스트를 전달 받음
        // 리스트는 튜플로 id와 데이터로 이루어져 있음
        // 해당 내용을 기반으로 player 캐릭터를 생성

        totalPlayer = currentPlayer = playerCharacters.Count;
    }

    private void GetStageData()
    {
        data = DataManager.Database.RootReference.Child($"Stages/{id}");

        // 데이터 불러오기
        data.OrderByValue().GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("데이터 불러오기 실패!: " + task.Exception);
                    return;
                }

                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot child in snapshot.Children)
                {
                    Debug.Log($"{child.Key} : {child.Value}");
                }

                Stage testStage = new(snapshot, sb);

                enemyCharacters.Add(testStage);

                // 스테이지 세팅
                StageInit();

                totalEnemy = enemyCharacters.Sum(x => x.EnemyList.Count);
            });
    }

    /// <summary>
    /// 캐릭터 스폰 리스트
    /// </summary>
    /// <param name="character"></param>
    /// <param name="tag"></param>
    public void RegisterCharacter(CharacterHandler character, string tag)
    {
        //  if (string.Equals(tag, "Enemy") && !enemyCharacters[currentStage].EnemyList.Contains(character))
        //  {
        //      enemyCharacters[currentStage].EnemyList.Add(character);
        //  }
        //  else if (string.Equals(tag, "Player") && !playerCharacters.Contains(character))
        //  {
        //      playerCharacters.Add(character);
        //  }
    }

    /// <summary>
    /// 캐릭터 사망했을 때 작동하는 함수
    /// </summary>
    /// <param name="character">사망한 캐릭터</param>
    /// <param name="tag">태그 = Enemy:적 / Player: 플레이어</param>
    public void OnCharacterDeath(CharacterHandler character, string tag)
    {
        // if (string.Equals(tag, "Enemy"))
        // {
        //     enemyCharacters[currentStage].EnemyList.Remove(character);
        //     currentEnemy = enemyCharacters[currentStage].EnemyList.Count;
        //     CheckVictoryCondition();
        // }
        // else if (string.Equals(tag, "Player"))
        // {
        //     playerCharacters.Remove(character);
        //     CheckDefeatCondition();
        // }
    }

    private void CheckVictoryCondition()
    {
        // 남은 스테이지가 있다면
        if (currentStage <= lastStage)
        {
            if (currentEnemy <= 0)
            {
                currentStage++;
                // 다음 스테이지 전환
                StageInit();
            }
        }
        else
        {
            // 승리 처리 로직
            Time.timeScale = 0f;
            Debug.Log("모든 적 캐릭터가 사망했습니다. 게임 승리!");
        }
    }

    private void StageInit()
    {

        Debug.Log("스테이지 세팅");
        if (totalPlayer <= 0)
        {
            // spawnManager.PlayerInit(playerCharacters);
        }

        spawnManager.EnemyInit(enemyCharacters[currentStage].EnemyList);

        currentEnemy = enemyCharacters[currentStage].EnemyList.Count;
    }

    private void CheckDefeatCondition()
    {
        if (playerCharacters.Count <= 0)
        {
            Debug.Log("모든 캐릭터가 사망했습니다. 게임 패배!");
            // 패배 처리 로직
        }
    }

}
