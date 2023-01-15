using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentButton : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private bool isLeft;

    public int Num
    {
        get
        {
            return num;
        }
    }
    public bool IsLeft
    {
        get
        {
            return isLeft;
        }
    }
    public void Click()
    {
        BuisnessScript._BuyUpgrade(gameObject);
    }
}
