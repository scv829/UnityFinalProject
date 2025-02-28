using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Test")]
public class TakeDamageAction : Action
{
	public int damage;
	public BattleEnum.DamageType damageType;
	protected CharacterHandler handler;
    public override void OnAwake()
    {
		handler = GetComponent<CharacterHandler>();
    }

	public override TaskStatus OnUpdate()
	{
		if(CanTakeDamage())
		{
			handler.TakeDamage(gameObject, damageType, damage);
		}

		return TaskStatus.Success;
	}

	private bool CanTakeDamage()
	{
		return handler as IHit != null;
	}
}