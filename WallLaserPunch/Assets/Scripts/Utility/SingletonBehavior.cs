using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehavior <T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;

    public bool ReplaceExisting = false;
    public bool DontDestroy = true;

    public static T Instance
    {
        get { return _instance; }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (DontDestroy)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            if (ReplaceExisting)
            {
                Destroy(_instance.gameObject);
                _instance = this as T;

                if (DontDestroy)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
