using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuisnessConfig", menuName = "Buiness Config", order = 51)]
public class BuisnessConfig : ScriptableObject
{
    [SerializeField] private float[] timeIncome;
    [SerializeField] private float[] priceBase;
    [SerializeField] private float[] incomeBase;
    [SerializeField] private float[] incomeMultiplier_1;
    [SerializeField] private float[] incomeMultiplier_2;
    [SerializeField] private float[] priceUpgrade_1;
    [SerializeField] private float[] priceUpgrade_2;

    public float[] TimeIncome
    {
        get
        {
            return timeIncome;
        }
    }

    public float[] PriceBase
    {
        get
        {
            return priceBase;
        }
    }

    public float[] IncomeBase
    {
        get
        {
            return incomeBase;
        }
    }

    public float[] IncomeMultiplier_1
    {
        get
        {
            return incomeMultiplier_1;
        }
    }
    public float[] IncomeMultiplier_2
    {
        get
        {
            return incomeMultiplier_2;
        }
    }

    public float[] PriceUpgrade_1
    {
        get
        {
            return priceUpgrade_1;
        }
    }

    public float[] PriceUpgrade_2
    {
        get
        {
            return priceUpgrade_2;
        }
    }
}
