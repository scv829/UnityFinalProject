using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

[TaskCategory("Battle")]
public class ConfirmBreakRequest : Conditional
{
	public SharedBool IsBreak;
	public SharedBool IsUseSkill;
    private Animator animator;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
		Debug.Log($"{gameObject.name} / {IsUseSkill.Value} {IsBreak.Value}");
		// 대기 요청자가 자신이 아니고 대기 요청이 들어왔을 경우
		if (IsBreak.Value && !IsUseSkill.Value)
		{
			Debug.Log($"{gameObject.name}정지 한다!");
			return TaskStatus.Failure;
		}
		else
		{
			Debug.Log($"{gameObject.name}정지 안한다...");
            animator.speed = 1f;
            return TaskStatus.Success;
		}
	}
}