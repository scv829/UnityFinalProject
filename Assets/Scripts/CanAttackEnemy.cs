using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Battle")]
public class CanAttackEnemy : Conditional
{
	// 하는 일
	// 적이 공격 범위 안에 있는지 확인
	// 있으면 해당 적을 타겟을 설정 및 성공 반환
	// 없으면 null로 하고 실패 반환

	// 할 수 있는 방법 2가지
	// 1. Physic2D.overlapSphere();
	// 2. OnTriggerEnter(); <- 이걸로 선택
	// 이유 ? -> 일단 없는데
	// 1번으로 선회 -> 이유 : 안된다

	[Tooltip("공격 범위에 들어온 적 캐릭터")]
	public SharedGameObject Target;

	[SerializeField] Collider2D[] enemies;
	[SerializeField] float attackRange;
	[SerializeField] Vector2 range;

    public override void OnAwake()
    {
        range = new Vector2(1f, 7.5f);
        enemies = new Collider2D[1];
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

        range = new Vector2(attackRange, 7.5f);
		enemies[0] = null;
		Physics2D.OverlapBoxNonAlloc(Vector2.right * transform.position.x + Vector2.right,  range, 0f, enemies, LayerMask.GetMask("Character"));

		if (enemies[0] != null)
		{
			Target.Value = enemies[0].gameObject;
		}
	}

    public override void OnCollisionEnter2D(Collision2D other)
    {
		if(Target.Value == null && other.collider.gameObject.CompareTag("Enemy")) Target.Value = other.gameObject;
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (Target.Value != null 
			&& Target.Value.Equals(other.gameObject)
            && other.gameObject.CompareTag("Enemy")) Target.Value = null;
    }

    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (Vector2.right * Owner.transform.position.x + Vector2.right, (Vector2.right + Vector2.up * 7.5f));
#endif
    }

}