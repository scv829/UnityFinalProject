using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class CanContinueBattle : Conditional
{
	protected CharacterHandler handler;

    public override void OnAwake()
    {
        handler = GetComponent<CharacterHandler>();
    }

    public override TaskStatus OnUpdate()
	{
		return (handler.IsDied) ? TaskStatus.Failure : TaskStatus.Success;
	}
}
