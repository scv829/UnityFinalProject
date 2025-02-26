using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Battle")]
public class ConfirmEnemy : Conditional
{
	[Tooltip("스테이지에 남아있는 적의 수")]
	public SharedInt enemyCount;

	public override TaskStatus OnUpdate()
	{
		return (enemyCount.Value > 0) ? TaskStatus.Success : TaskStatus.Failure;
	}
}