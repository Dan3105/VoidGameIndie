using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FuncForStuff : MonoBehaviour
{
    public Texture2D texture2D;

    [ContextMenu("Get Child Texture")]
    public void GetChildTexture()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(texture2D.name);
    }
    
}
