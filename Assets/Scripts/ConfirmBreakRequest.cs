using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class ConfirmBreakRequest : Conditional
{
	public SharedGameObject requseter;
	public SharedBool IsBreak;

	public override TaskStatus OnUpdate()
	{
		// 대기 요청자가 자신이 아니고 대기 요청이 들어왔을 경우
		if (requseter.Equals(null) && IsBreak.Value)
		{
			return TaskStatus.Success;
		}
		else
		{
			Time.timeScale = 1.0f;
			return TaskStatus.Failure;
		}
	}
}