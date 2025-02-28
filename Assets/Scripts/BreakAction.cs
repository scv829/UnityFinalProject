using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class BreakAction : Action
{
	public override TaskStatus OnUpdate()
	{
		Time.timeScale = 0f;
		return TaskStatus.Success;
	}
}