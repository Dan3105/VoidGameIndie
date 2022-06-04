using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonTemplate<LevelManager>
{
    [Header("List Skill Active")]
    public List<AbilitiesStats> skillList = new List<AbilitiesStats>();
    public SkillActive[] listButton = new SkillActive[3];
    public CharacterAbilities charSkill;
    public Dictionary<AbilitiesStats, bool> checkUsed = new Dictionary<AbilitiesStats, bool>();
    int rangeRandom = 0;

    private void Start()
    {
        rangeRandom = 0;
        foreach(var skill in skillList)
        {
            checkUsed[skill] = false;
        }

    }

    public void SetRandomOpt()
    {
        
        for (int i = 0; i < skillList.Count - rangeRandom; i++)
        {
            if (skillList[i].type == AbilitiesStats.TYPE_ABILITY.OFFENSIVE
            && checkUsed[skillList[i]])
            {

                swap(skillList, i, skillList.Count - rangeRandom - 1);
                rangeRandom++;
            }
        }
        Debug.Log(rangeRandom);
            //Shuffle
        for (int j = 0; j < 10; j++)
        {
            int start = Random.Range(0, skillList.Count - rangeRandom);
            int end = Random.Range(0, skillList.Count - rangeRandom);
            var temp = skillList[start];
            skillList[start] = skillList[end];
            skillList[end] = temp;
        }

        for (int i = 0; i < listButton.Length; i++)
        {
            SetUpBtn(ref listButton[i], skillList[i]);
        }

    }
    void SetUpBtn(ref SkillActive btn, AbilitiesStats skill)
    {
        btn.SetSkill(skill);
    }

    public void swap(List<AbilitiesStats> list, int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}
