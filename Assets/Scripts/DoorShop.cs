using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorShop : MonoBehaviour
{
    [SerializeField] Canvas shopCanvas;
    [SerializeField] TextMeshProUGUI buyTextMesh;
    [SerializeField] int doorPrice;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] Animator animator;

    bool canBuy = true;
    bool isColliding = false;

    void Update()
    {
        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.F) && canBuy)
            {
                if (playerMoney.MoneyAmount() >= doorPrice)
                {
                    canBuy = false;
                    playerMoney.SubtractMoney(doorPrice);
                    animator.enabled = true;
                    shopCanvas.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBuy)
        {
            shopCanvas.enabled = true;
            buyTextMesh.text = "Press F to open the door \n" + "\t    ($" + doorPrice + ")";
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBuy)
        {
            isColliding = false;
            shopCanvas.enabled = false;
        }
    }
}
