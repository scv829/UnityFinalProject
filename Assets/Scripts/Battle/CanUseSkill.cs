using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class CanUseSkill : Conditional
{
	public int index;
	
	private CharacterHandler handler;

	public override void OnAwake()
	{
		handler = GetComponent<CharacterHandler>();
	}

	public override TaskStatus OnUpdate()
	{
		return ( handler.CanUseSkill(out index) ) ? TaskStatus.Success : TaskStatus.Failure;
	}
}