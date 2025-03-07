using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Skill/TestSkill")]
public class Test_Skill : Skill
{
    public override void Action(CharacterHandler character, GameObject target)
    {
        try
        {
            IHit hit = target.GetComponent<IHit>();
            if (hit != null)
            {
                bool result = hit.TakeDamage(character.gameObject, BattleEnum.DamageType.Skill, SkillDamage);
                if (result) target = null;
            }
        }
        catch
        {
            Debug.Log("타겟이 없어졌다!");
        }
    }

    public override SkillData GetSkillData()
    {
        throw new System.NotImplementedException();
    }

    public override void SkillStart()
    {
        throw new System.NotImplementedException();
    }

    public override void SkillUpdate()
    {
        throw new System.NotImplementedException();
    }
}
