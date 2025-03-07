using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class BTEnableAction : Action
{
    public SharedBool IsUseSkill;
    public SharedBool IsBreak;


	public override TaskStatus OnUpdate()
	{
        IsUseSkill.Value = false;
        IsBreak.Value = false;

        return TaskStatus.Success;
	}
}