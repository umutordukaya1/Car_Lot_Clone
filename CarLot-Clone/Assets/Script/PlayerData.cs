using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; set; }
    [System.Serializable]
    public class ParkData
    {
        public string parkName;
        public int parkCurrency;
        public GameObject parks;
    }
    [Header("ParkMenu")]
    public ParkData[] data;
    public List<GameObject> parkmoneyList;
    public int currency = 0;

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt("Money", currency);
    }
    public void AddCurrency(int money)
    {
        currency = PlayerPrefs.GetInt("Money");
        currency += money;
        SaveCurrency();
    }
    public void DeductCurrency(int money)
    {
        if (currency > 0)
        {
            currency -= money;
        }
        SaveCurrency();
    }
    void Awake()
    {
        instance = this;
    }

}
