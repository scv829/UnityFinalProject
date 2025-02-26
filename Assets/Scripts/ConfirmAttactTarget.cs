using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using Unity.VisualScripting;

[TaskCategory("Battle")]
public class ConfirmAttactTarget : Conditional
{
    [Tooltip("공격 범위에 들어온 적 캐릭터")]
    public SharedGameObject Target;

    public override TaskStatus OnUpdate()
	{
		return (Target.Value != null) ? TaskStatus.Success : TaskStatus.Failure;
	}
}