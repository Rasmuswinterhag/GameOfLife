using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    [SerializeField] bool playerTyping = false;
    [SerializeField] string currentString = "";
    [SerializeField] List<CheatCodeInstance> cheatCodeList = new List<CheatCodeInstance>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(playerTyping)
            {
                CheckCheat(currentString);
            }
            playerTyping = !playerTyping;
        }

        if(playerTyping)
        {
            foreach (char c in Input.inputString)
            {
                if(c == '\b') //backspace or delete
                {
                    if(currentString.Length > 0)
                    {
                        currentString = currentString.Substring(0, currentString.Length-1);
                    }
                }
                else if (c == '\n' || c == '\r') //enter or return
                {
                    currentString = "";
                }
                else
                {
                    currentString += c;
                }
            }
        }
    }

    bool CheckCheat(string input)
    {
        foreach (CheatCodeInstance code in cheatCodeList)
        {
            if (input == code.code)
            {
                code.cheatEvent?.Invoke();
                return true;
            }
        }
        return false;
    }
}