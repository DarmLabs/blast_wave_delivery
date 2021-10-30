using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPs : MonoBehaviour
{
    public GameObject MenuManager;
    Main_Menu_Manager main_Menu_Manager;
    private string coins1750 = "com.darmlabs.blastwavedelivery.1750coins";
    private string coins3500 = "com.darmlabs.blastwavedelivery.3500coins";
    private string coins7000 = "com.darmlabs.blastwavedelivery.7000coins";
    private string coins14000 = "com.darmlabs.blastwavedelivery.14000coins";
    private string coins28000 = "com.darmlabs.blastwavedelivery.28000coins";
    private string vipStatus = "com.darmlabs.blastwavedelivery.vipStatus";
    void Start()
    {
        main_Menu_Manager = MenuManager.GetComponent<Main_Menu_Manager>();
    }

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == coins1750)
        {
            SaveData.current.monedas += 1750;
        }
        if(product.definition.id == coins3500)
        {
            SaveData.current.monedas += 3500;
        }
        if(product.definition.id == coins7000)
        {
            SaveData.current.monedas += 7000;
        }
        if(product.definition.id == coins14000)
        {
            SaveData.current.monedas += 14000;
        }
        if(product.definition.id == coins28000)
        {
            SaveData.current.monedas += 28000;
        }
        if(product.definition.id == vipStatus)
        {
            SaveData.current.deactivatedAds = true;
            SaveData.current.skinInferno = true;
            SaveData.current.skinLight = true;
            SaveData.current.skinRadiactive = true;
            SaveData.current.skinRetro = true;
            main_Menu_Manager.ConfirmationVIP();
        }
        main_Menu_Manager.OnSaveGame();
        main_Menu_Manager.OnLoadGame();
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed because " + failureReason);
    }
}
