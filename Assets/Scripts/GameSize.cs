using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Search.SearchUtils;

public class GameSize : MonoBehaviour
{
    bool XIsValid;
    bool YIsValid;
    int XSize;
    int YSize;
    
    [SerializeField] TMP_InputField XInputField;
    [SerializeField] TMP_InputField YInputField;
    [SerializeField] TMP_Text aliveChanceSliderValueText;
    [SerializeField] Slider aliveChanceSlider;
    [SerializeField] Button generateButton;

    void Start()
    {
        XInput(XInputField.text);
        YInput(YInputField.text);

        AliveChanceSlider(aliveChanceSlider.value);
    }
    public void XInput(string input)
    {
        if(!TryParse(input, out XSize))
        {
            XIsValid = false;
            return;
        }
        if(XSize <= 0)
        {
            XIsValid = false;
            return;
        }

        XIsValid = true;
    }

    public void YInput(string input)
    {
        if(!TryParse(input, out YSize))
        {
            YIsValid = false;
            return;
        }
        if(YSize <= 0)
        {
            YIsValid = false;
            return;
        }
        YIsValid = true;
    }

    void Update()
    {
        if(XIsValid && YIsValid)
        {
            generateButton.interactable = true;
            Messanger.instance.menuGameSize = new(XSize, YSize);
        }
        else
        {
            generateButton.interactable = false;
        }
    }

    public void AliveChanceSlider(float value)
    {
        aliveChanceSliderValueText.text = (value * 100).ToString("0") + "%";

        Messanger.instance.menuAliveChance = value;
    }
}
