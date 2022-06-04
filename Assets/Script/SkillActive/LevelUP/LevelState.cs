using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelState : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI text;

    

    public void TriggerSkill()
    {
      
        Time.timeScale = 1f;
    }

}

