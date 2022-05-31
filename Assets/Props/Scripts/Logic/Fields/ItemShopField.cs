using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopField : MonoBehaviour, IField
{
    public FieldTypeEnum FieldType { get; set; } = FieldTypeEnum.ItemShopField;
    public bool IsDoneShopping { get; set; } = false;
    public bool PlayerInShop { get; set; } = false;
    public void InteractWithPlayer(PlayerInfo player)
    {
        PlayerInShop = true;
        Debug.Log("shop not implemented yet");
        Debug.Log("press w if you are done shopping");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && PlayerInShop)
        {
            PlayerInShop = false;
            IsDoneShopping = true;
        }
    }
}
