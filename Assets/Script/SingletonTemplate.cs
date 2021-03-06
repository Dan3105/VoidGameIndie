using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTemplate<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance { get; set; }
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            instance = FindObjectOfType<T>();
    //            //if(instance == null)
    //            //{
    //            //    GameObject obj = new GameObject();
    //            //    obj.name = typeof(T).Name;
    //            //    instance = obj.AddComponent<T>();
    //            //}  
    //        }
    //        return instance;
    //    }
    //}

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
            Destroy(gameObject);

    }
}
