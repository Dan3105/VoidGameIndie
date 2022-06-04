using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilitiesStats : ScriptableObject
{
    public enum TYPE_ABILITY{
        OFFENSIVE,
        DEFENSIVE
    }
    public abstract TYPE_ABILITY type { get; }
    public string skillDes;
    public Sprite skillImg;
    
    public virtual void SkillActive() { }

}


