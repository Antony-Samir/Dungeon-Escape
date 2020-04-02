using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    [SerializeField]
    private AudioClip selectionClip;

    public GameObject shopPanel;
    public GameObject selectionImage;
    public int currentSelectedItem;
    public int currentItemCost;
    Player _player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            UIManager.Instance.UpdateShopSelection(-800);
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }
    public void SelectItem(int item)
    {
        //0 - flame sword
        //1 - boots of flight
        //2 - key to castle
        Debug.Log("SelectItem()" + item);
        AudioSource.PlayClipAtPoint(selectionClip, this.transform.position, 1f);
        selectionImage.SetActive(true);
        switch (item)
        {
            case 0: //Full Health
                UIManager.Instance.UpdateShopSelection(70);
                currentSelectedItem = 0;
                currentItemCost = 400;
                break;
            case 1: //FlamingSword
                UIManager.Instance.UpdateShopSelection(-30);
                currentSelectedItem = 1;
                currentItemCost = 200;
                break;
            case 2: //Key
                UIManager.Instance.UpdateShopSelection(-130);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
        //selectionImage.SetActive(false);
    }
    public Image keyImg;
    public void ShowKey()
    {
        keyImg.enabled = true;
    }
    public void BuyItem()
    {
        if (_player.diamonds >= currentItemCost)
        {//award item

            if (currentSelectedItem == 0)
            {
                UIManager.Instance.AddLives();
            }
            if (currentSelectedItem == 1)
            {
                GameManager.Instance.player.fireAttack = true;
            }
            if (currentSelectedItem == 2)
            {
               ShowKey();
                keyImg.enabled = true;
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= currentItemCost;
            shopPanel.SetActive(false);
            Debug.Log("Purchesed: " + currentSelectedItem);
            Debug.Log("Current player gems: " + _player.diamonds);
        }
        else
        {
            Debug.Log("You don't have enough gems. Closing Shop");
            shopPanel.SetActive(false);
        }
    }


}
