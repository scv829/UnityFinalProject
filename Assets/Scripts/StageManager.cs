using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 스테이지 관리(진입, 전환, 캐릭터 관리 등)하는 매니저
/// </summary>
public class StageManager : MonoBehaviour
{

    [Header("스테이지 정보")]
    [SerializeField] int id;
    [SerializeField] int currentStage;
    [SerializeField] int lastStage;
    [SerializeField] List<Round> stages;
    [Header("캐릭터 생성")]
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] List<(int, CharacterSO)> playerChracters;
    [Header("캐릭터 생존")]
    [SerializeField] List<GameObject> alivePlayerCharaceters;
    [SerializeField] List<GameObject> aliveEnemyCharaceters;

    private DatabaseReference data;

    private StringBuilder sb;

    private void Awake()
    {
        alivePlayerCharaceters = new List<GameObject>();
        aliveEnemyCharaceters = new List<GameObject>();

        sb = new StringBuilder();
        currentStage = 0;
    }

    private void Start()
    {

        // 시작 시 스테이지에 대한 정보 가져오기
        stages = DataManager.Instance.GetStageData(id);
        // 시작 시 플레이어 선택에 대한 정보 가져오기
        playerChracters = DataManager.Instance.GetPlayerCharacterData();

        // 플레이어 캐릭터 생성
        foreach ((int, CharacterSO) data in playerChracters)
        {
            spawnManager.SpawnPlayerCharacter(data.Item1, data.Item2, ref alivePlayerCharaceters);
        }

        // 초기 스테이지 설정
        lastStage = stages.Count;

        currentStage = 0;

        CheckVictoryCondition();
    }

    public void CheckAlivePlayerCharaceter(GameObject requester)
    {
        if(alivePlayerCharaceters.Contains(requester))
        {
            alivePlayerCharaceters.Remove(requester);
            CheckDefeatCondition();
        }
    }

    public void CheckAliveEnemyCharaceter(GameObject requester)
    {
        if (aliveEnemyCharaceters.Contains(requester))
        {
            aliveEnemyCharaceters.Remove(requester);

            Destroy(requester);

            if(aliveEnemyCharaceters.Count <= 0)
                CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition()
    {
        // 남은 스테이지가 있다면
        if (currentStage < lastStage)
        {
            // 플레이어 캐릭터 위치 초기화
            ResetPlayerCharacterPos();
            // 다음 스테이지에 등장하는 적 캐릭터 생성
            spawnManager.SpawnEnemyCharacter(stages[currentStage++], ref aliveEnemyCharaceters);
        }
        else
        {
            // 승리 처리 로직
            Time.timeScale = 0f;
            Debug.Log("모든 적 캐릭터가 사망했습니다. 게임 승리!");
        }
    }

    private void ResetPlayerCharacterPos()
    {
        spawnManager.ResetPlayerSpawnPosCount();

        foreach(GameObject character in alivePlayerCharaceters)
        {
            if(character.layer.Equals(LayerMask.NameToLayer("Die"))) gameObject.SetActive(false);

            spawnManager.ResetPlayerCharacterPos(character);
        }
    }

    private void CheckDefeatCondition()
    {
        if (alivePlayerCharaceters.Count <= 0)
        {
            // 패배 처리 로직
            Time.timeScale = 0f;
            Debug.Log("모든 플레이어 캐릭터가 사망했습니다. 게임 패배!");
        }
    }

}
