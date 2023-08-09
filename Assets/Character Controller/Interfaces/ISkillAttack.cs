using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillAttack: IAttack
{
    public SkillData[] NextSkillChain { get; set; }
}
