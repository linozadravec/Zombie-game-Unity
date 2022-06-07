using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] int moneyAmount = 0;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Start()
    {
        MoneyGUIUpdate();
    }

    public void AddMoney(int amount)
    {
        moneyAmount += amount;
        MoneyGUIUpdate();
    }

    public void SubtractMoney(int amount)
    {
        moneyAmount -= amount;
        MoneyGUIUpdate();
    }

    public int MoneyAmount()
    {
        return moneyAmount;
    }

    private void MoneyGUIUpdate()
    {
        moneyText.text = "$" + moneyAmount;
    }
}
