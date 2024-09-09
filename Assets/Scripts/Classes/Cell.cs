using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class Cell
{
    bool alive;
    int neighbors;
    SpriteRenderer sr;
    public Vector2 position;
    public Cell(SpriteRenderer spriteRenderer)
    {
        sr = spriteRenderer;
    }
}
