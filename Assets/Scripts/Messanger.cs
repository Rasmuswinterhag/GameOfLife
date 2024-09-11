using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messanger : MonoBehaviour
{
    public static Messanger instance {get; private set;}
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public Vector2Int menuGameSize;
    public float menuAliveChance;
}
