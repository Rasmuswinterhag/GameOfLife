using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOverUI = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverUI = true;
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverUI = false;
        //throw new System.NotImplementedException();
    }

    [SerializeField] GameObject customRulePanel;
    public void ShowCustomRules()
    {
        customRulePanel.SetActive(!customRulePanel.activeSelf);
    }
}
