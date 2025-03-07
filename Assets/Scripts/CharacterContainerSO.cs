using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Character")]
public class CharacterContainerSO : ScriptableObject
{
    [SerializeField] List<CharacterHandler> c_Container;

    public CharacterHandler GetCharacter(int index = -1)
    {
        return c_Container[Mathf.Clamp(index, 0, c_Container.Count)];
    }

}
