using UnityEngine;

/// <summary>
/// 전투 관리 매니저
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    // 스킬 사용 우선순위 담당
    // 전투에서 사망시 StageManager에게 전달
    // 스킬 사용 시 연출 담당

    public void OnDied(GameObject requester)
    {
        switch (requester.tag)
        {
            case "Player":
                stageManager.CheckAlivePlayerCharaceter(requester);
                break;
            case "Enemy":
                stageManager.CheckAliveEnemyCharaceter(requester);
                break;
        }
    }

}
