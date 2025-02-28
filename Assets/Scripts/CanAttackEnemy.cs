using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Battle")]
public class CanAttackEnemy : Conditional
{
    [Tooltip("공격 범위에 들어온 적 캐릭터")]
    public SharedGameObject Target;
    [Tooltip("공격 범위에 들어온 적 캐릭터의 태그")]
    public SharedString TargetTag;

    [SerializeField] Collider2D[] characters;
    [SerializeField] float attackRange;
    [SerializeField] Vector2 range;
    private Vector2 pos;
    private SpriteRenderer renderer;

    public override void OnAwake()
    {
        range = new Vector2(1f, 7.5f);
        characters = new Collider2D[10];
        renderer = GetComponent<SpriteRenderer>();
    }

    public override void OnEnd()
    {
        if (string.IsNullOrEmpty(TargetTag.Value))
        {
            TargetTag.Value = gameObject.CompareTag("Enemy") ? "Player" : "Enemy";
        }
    }

    public override TaskStatus OnUpdate()
    {
        CheckEnemyInAttackArea();

        return Target.Value != null ? TaskStatus.Success : TaskStatus.Failure;
    }

    public void CheckEnemyInAttackArea()
    {
        // 이미 공격타겟이 있으면 확인하는 로직 실행 x
        if (Target.Value != null) return;

        Debug.Log("연산은 하냐");

        for(int i = 0; i < characters.Length; i++) characters[i] = null;

        // 공격 범위
        range = new Vector2(attackRange, 7.5f);

        // 탐색할 위치 = 현재 캐릭터의 위치(x) + 공격 범위의 반절
        pos = new Vector2(transform.position.x + attackRange * (renderer.flipX ? -1f : 1f), 0f);

        // 공격 범위에 들어온 캐릭터의 수
        int count = Physics2D.OverlapBoxNonAlloc(
                pos,
                range, 
                0f, 
                characters, LayerMask.GetMask("Character")
            );

        if (count > 0)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                // 순차적으로 들어와서 없을 경우 더이상 탐색이 불필요
                if (characters[i] == null) break;
                // 공격 범위에 들어온 캐릭터의 태그가 나와 적대 관계의 태그라면
                if (characters[i].gameObject.CompareTag(TargetTag.Value))
                {
                    // 해당 대상을 타겟
                    Target.Value = characters[i].gameObject;
                    break;
                }
            }
        }
    }

    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2( Owner.transform.position.x + (Owner.GetComponent<SpriteRenderer>().flipX ? -1f : 1f), 0f), (Vector2.right * (Owner.CompareTag("Player") ? 1 : -1) + Vector2.up * 7.5f));
#endif
    }

}