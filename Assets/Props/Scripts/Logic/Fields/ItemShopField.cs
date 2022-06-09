using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.ItemShopField;
    public bool IsDoneShopping { get; set; } = false;
    public bool PlayerInShop { get; set; } = false;
    private int PlayerMoney=5;
    private PlayerInfo playerInfo;
    public void InteractWithPlayer(PlayerInfo player)
    {
        PlayerInShop = true;
        Debug.Log("press w if you are done shopping");
        LoadShop();
    }

    void LoadShop(){
        Debug.Log("Welcome to the shop");
        GetShopItems();
        Debug.Log("Press 1 for Double Dice, Press 2 for Custom Dice");
    }
    void GetShopItems(){
        Debug.Log("Fetching Shop items!");
        //return list of shop items
    }
    bool CheckMoney(int money){
        Debug.Log("Coins available: " + PlayerMoney);
        if(PlayerMoney<money){
            return false;
        }
        PlayerMoney-=money;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInShop){
            if (Input.GetKeyDown("w")){
                PlayerInShop = false;
                IsDoneShopping = true;
                return;
            }
            if (Input.GetKeyDown("1")){
                if(CheckMoney(5)){
                    Debug.Log("Player bought Double Dice");
                    return;
                }
                Debug.Log("Player has not enough money to buy a Double Dice");
                return;
            }
            if (Input.GetKeyDown("2")){
                if(CheckMoney(10)){
                    Debug.Log("Player bought Custom Dice");
                    return;
                }
                Debug.Log("Player has not enough money to buy a Custom Dice");
                return;
            }
        }
        
    }
}
