using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class BreakAction : Action
{
    public SharedBool IsBreak;
	private Animator animator;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
		Debug.Log("작동하냐?");
		// 대기 하는 상태
		animator.speed = 0f;
		// Time.timeScale = 0f;
		// 애니메이션을 멈춘다
		return (IsBreak.Value) ? TaskStatus.Running : TaskStatus.Success;
	}
}