using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class SkillAction : Action
{
	public SharedGameObject Target;
	public CanUseSkill CanUseSkill;

	public SharedBool IsUseSkill;
	public SharedBool IsBreak;

	private CharacterHandler characterHandler;

    public override void OnAwake()
    {
		characterHandler = GetComponent<CharacterHandler>();
    }

    public override TaskStatus OnUpdate()
	{
		IsUseSkill.Value = true;
		IsBreak.Value = true;

		characterHandler.SkillAction(in CanUseSkill.index, Target.Value);

		return TaskStatus.Success;
	}

}