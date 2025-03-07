using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class InitAction : Action
{
	private CharacterHandler handler;
	public SharedString TargetTag; 

    public override void OnAwake()
    {
		handler = GetComponent<CharacterHandler>();
    }

	public override TaskStatus OnUpdate()
	{
		if(handler.IsInitComplete)
		{
			TargetTag.Value = (gameObject.CompareTag("Player")) ? "Enemy" : "Player";
			return TaskStatus.Success;
		}
		return  TaskStatus.Running;
	}
}