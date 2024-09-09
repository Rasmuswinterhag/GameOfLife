using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cell
{
    public bool alive;
    public bool willBeAlive;
    SpriteRenderer sr;
    public Vector2 position;
    public Cell(SpriteRenderer spriteRenderer)
    {
        sr = spriteRenderer;
    }

    public void UpdateTetxure()
    {
        if(alive)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.black;
        }
    }

    public void SetNewState()
    {
        alive = willBeAlive;
    }
}
