using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Battle")]
public class AttackAction : Action
{
    [Tooltip("공격 범위에 들어온 적 캐릭터")]
    public SharedGameObject Target;
    [Tooltip("이동 애니메이션 실행 여부")]
    public SharedBool IsMoveAnimStart;
    [Tooltip("공격 애니메이션 이름")]
    public SharedString AttackAnimationName;

    protected CharacterHandler handler;
    protected Animator animator;


    public override void OnAwake()
    {
        handler = GetComponent<CharacterHandler>();
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
        StartAttackAnimation();

		return Attack() ?  TaskStatus.Success : TaskStatus.Failure;
	}

    private bool Attack()
    {
        try
        {
            IHit hit = Target.Value.GetComponent<IHit>();
            if (hit != null)
            {
                bool result = hit.TakeDamage(gameObject, BattleEnum.DamageType.Normal, handler.Damage);
                if(result) Target.Value = null;
                return true;
            }
        }
        catch
        {
            Debug.Log("타겟이 없어졌다!");
            Target.Value = null;
        }
        return false;
    }

    private void StartAttackAnimation()
    {
        // TODO : 현재 그냥 공격을 애니메이션을 반복하는데 해당 행동을 공격 속도에 맞춰서 해야함
        animator.CrossFade(AttackAnimationName.Value, 0.01f);
        IsMoveAnimStart.Value = false;
    }
}