using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuisnessScript : MonoBehaviour
{
    private static BuisnessScript _internal;
    private float money;
    [SerializeField] private BuisnessNames buisnessNames;
    [SerializeField] private BuisnessConfig buisnessConfig;
    [SerializeField] private TextMeshProUGUI[] textName;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI[] textPrice;
    [SerializeField] private TextMeshProUGUI[] textIncome;
    [SerializeField] private TextMeshProUGUI[] priceUpgrade_1;
    [SerializeField] private TextMeshProUGUI[] priceUpgrade_2;
    [SerializeField] private TextMeshProUGUI[] textLvl;
    [SerializeField] private Button[] btnBuyUpgrade_1;
    [SerializeField] private Button[] btnBuyUpgrade_2;
    [SerializeField] private Button[] btnLvlUp;
    [SerializeField] private Slider[] sliderProgress;
    private bool[] lvlUpgrade_1 = new bool[5];
    private bool[] lvlUpgrade_2 = new bool[5];
    private int[] buisnessLvl = new int[5] {0,0,0,0,0};
    private float[] priceLvlUp = new float[5];
    private float[] income = new float[5];
    private int isFirstEnter;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {

        isFirstEnter = PlayerPrefs.GetInt("isFirstEnter", 0);
        money = PlayerPrefs.GetFloat("money", 0);
        _internal = this;
        for (int i=0; i<textName.Length; i++)
        {
            buisnessLvl[i] = PlayerPrefs.GetInt("buisnessLvl" + i);
            sliderProgress[i].maxValue = PlayerPrefs.GetFloat("sliderProgressMaxValue" + i);
            sliderProgress[i].value = PlayerPrefs.GetFloat("sliderProgress" + i);
            if (PlayerPrefs.GetInt("lvlUpgrade_1"+i) > 0) lvlUpgrade_1[i] = true;
            else lvlUpgrade_1[i] = false;
            if (PlayerPrefs.GetInt("lvlUpgrade_2"+i) > 0) lvlUpgrade_2[i] = true;
            else lvlUpgrade_2[i] = false;
            textLvl[i].text = "Lvl " + buisnessLvl[i].ToString("0");
            textName[i].text = buisnessNames.BuisnessName[i];
            RefreshPriceUpgrades(i);
            RefreshPriceLvlUp(i);
            CheckIncome(i);
        }
        if (isFirstEnter==0)
        {
            sliderProgress[0].maxValue = buisnessConfig.TimeIncome[0];
            buisnessLvl[0] = 1;
            textLvl[0].text = "Lvl " + buisnessLvl[0].ToString("0");
            CheckIncome(0);
            RefreshPriceLvlUp(0);
            PlayerPrefs.SetFloat("sliderProgressMaxValue" + 0, sliderProgress[0].maxValue);
            PlayerPrefs.SetInt("buisnessLvl" + 0, buisnessLvl[0]);
            PlayerPrefs.SetInt("isFirstEnter", 1);
        }
        ChangeMoney(true, 0);

        InvokeRepeating("SaveProgress", 0.5f, 0.5f);
    }

    void RefreshPriceUpgrades(int m)
    {      
            if (!lvlUpgrade_1[m]) priceUpgrade_1[m].text = buisnessNames.UpgradeName_1[m] + "\nДоход: + " + (buisnessConfig.IncomeMultiplier_1[m] * 100) + "%" + "\nЦена: " + buisnessConfig.PriceUpgrade_1[m].ToString("#.#") + "$";
            else priceUpgrade_1[m].text = buisnessNames.UpgradeName_1[m] + "\nДоход: + " + (buisnessConfig.IncomeMultiplier_1[m] * 100) + "%" + "\nКуплено";
            if (!lvlUpgrade_2[m]) priceUpgrade_2[m].text = buisnessNames.UpgradeName_2[m] + "\nДоход: + " + (buisnessConfig.IncomeMultiplier_2[m] * 100) + "%" + "\nЦена: " + buisnessConfig.PriceUpgrade_2[m].ToString("#.#") + "$";
            else priceUpgrade_2[m].text = buisnessNames.UpgradeName_2[m] + "\nДоход: + " + (buisnessConfig.IncomeMultiplier_2[m] * 100) + "%" + "\nКуплено";
    }
    public static void _BuyUpgrade(GameObject obj)
    {
        _internal.BuyUpgrade(obj);
    }
    public void BuyUpgrade(GameObject obj)
    {
        int m = obj.GetComponent<CurrentButton>().Num;
        bool q = obj.GetComponent<CurrentButton>().IsLeft;
        if (q)
        {
            lvlUpgrade_1[m] = true;
            ChangeMoney(false, buisnessConfig.PriceUpgrade_1[m]);
            
        }
        else
        {
            lvlUpgrade_2[m] = true;
            ChangeMoney(false, buisnessConfig.PriceUpgrade_2[m]);
        }
        
        RefreshPriceUpgrades(m);
        CheckIncome(m);
        if (lvlUpgrade_1[m]) PlayerPrefs.SetInt("lvlUpgrade_1"+m, 1);
        else PlayerPrefs.SetInt("lvlUpgrade_1"+m, 0);
        if (lvlUpgrade_2[m]) PlayerPrefs.SetInt("lvlUpgrade_2"+m, 1);
        else PlayerPrefs.SetInt("lvlUpgrade_2"+m, 0);
    }

    void RefreshPriceLvlUp(int m)
    {
       if(m!=0) priceLvlUp[m] = (buisnessLvl[m] + 1) * buisnessConfig.PriceBase[m];
       else priceLvlUp[m] = buisnessLvl[m] * buisnessConfig.PriceBase[m];
        textPrice[m].text = "LVL UP \nЦена: " + priceLvlUp[m] + "$";
    }
    void ChangeMoney(bool isPlus, float var)
    {
        if (isPlus) money += var;
        else money -= var;
        if (money < 0) money = 0;
        PlayerPrefs.SetFloat("money", money);
        moneyText.text = "Баланс: " + money.ToString("0.#") + "$";
        for (int i=0; i<btnLvlUp.Length; i++)
        {
            if (money >= priceLvlUp[i]) btnLvlUp[i].interactable = true;
            else btnLvlUp[i].interactable = false;
        }
        
    }
    public void BuyBuisness(int m)
    {
        ChangeMoney(false, priceLvlUp[m]);
        sliderProgress[m].maxValue = buisnessConfig.TimeIncome[m];
        PlayerPrefs.SetFloat("sliderProgressMaxValue" + m, sliderProgress[m].maxValue);
        buisnessLvl[m]++;
        RefreshPriceLvlUp(m);
        CheckIncome(m);
        textLvl[m].text = "Lvl " + buisnessLvl[m].ToString("0");
        PlayerPrefs.SetFloat("money", money);
        PlayerPrefs.SetInt("buisnessLvl" + m, buisnessLvl[m]);
    }
    void CheckIncome(int m)
    {
        float m1 = 0;
        float m2=0;
        if (lvlUpgrade_1[m]) m1 = buisnessConfig.IncomeMultiplier_1[m]; 
        if (lvlUpgrade_2[m]) m2 = buisnessConfig.IncomeMultiplier_2[m];
        income[m] = buisnessLvl[m] * buisnessConfig.IncomeBase[m] * (1 + m1 + m2);
        textIncome[m].text = "Доход: " + income[m].ToString("0.#") + "$";
        PlayerPrefs.SetFloat("income" + m, income[m]);
    }

    void PlusIncome(int m)
    {
        ChangeMoney(true, income[m]);
        sliderProgress[m].value = 0;
    }
    void SaveProgress()
    {
        for (int i=0; i<sliderProgress.Length;i++)
        {
            if (buisnessLvl[i]>0) PlayerPrefs.SetFloat("sliderProgress" + i, sliderProgress[i].value);
        }
    }
    void Update()
    {
        
        for (int i=0; i<btnBuyUpgrade_1.Length; i++)
        {
            if (buisnessLvl[i] > 0)
            {
                if (!lvlUpgrade_1[i] && money >= buisnessConfig.PriceUpgrade_1[i]) btnBuyUpgrade_1[i].interactable = true;
                else btnBuyUpgrade_1[i].interactable = false;
                if (!lvlUpgrade_2[i] && money >= buisnessConfig.PriceUpgrade_2[i]) btnBuyUpgrade_2[i].interactable = true;
                else btnBuyUpgrade_2[i].interactable = false;
                if (sliderProgress[i].value < sliderProgress[i].maxValue) sliderProgress[i].value += Time.deltaTime;
                else PlusIncome(i);
            }
            else
            {
                btnBuyUpgrade_2[i].interactable = false;
                btnBuyUpgrade_1[i].interactable = false;
            }
            

        }
    }
}
