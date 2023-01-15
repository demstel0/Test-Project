using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuisnessName", menuName = "Buiness Name", order = 51)]
public class BuisnessNames : ScriptableObject
{
    [SerializeField] private string[] buisnessName;
    [SerializeField] private string[] upgradeName_1;
    [SerializeField] private string[] upgradeName_2;

    public string[] BuisnessName
    {
        get
        {
            return buisnessName;
        }
    }
    public string[] UpgradeName_1
    {
        get
        {
            return upgradeName_1;
        }
    }
    public string[] UpgradeName_2
    {
        get
        {
            return upgradeName_2;
        }
    }
}
