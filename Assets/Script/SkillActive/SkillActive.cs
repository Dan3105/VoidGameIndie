using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillActive : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI text;
    public AbilitiesStats ability;

    public void ShowText()
    {
        text.text = ability.skillDes;
    }

    public void HideText()
    {
        text.text = "";
    }

    public void SetSkill(AbilitiesStats newAbility)
    {
        ability = newAbility;
        btn.image.sprite = ability.skillImg;
       
    }

    public void ActiveSkill()
    {
        if (!ability)
            return;
        LevelManager.Instance.checkUsed[ability] = true;
        ability.SkillActive();
        
        Time.timeScale = 1f;
        HideText();
    }
}
