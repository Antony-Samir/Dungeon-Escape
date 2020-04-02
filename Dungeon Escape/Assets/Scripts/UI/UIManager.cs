using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip hitClip;


    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }
            return _instance;
        } 
    }
    public Text PlayerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] healthBars;
    
    private void Awake()
    {
        _instance = this;
    }
    public void OpenShop(int gemCount)
    {
        PlayerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);    
    }

    public void updateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    
    public void UpdateLives(int livesRemaining)
    {

        for (int i = 0; i <= livesRemaining; i++)
        {//do nothing till we got hit the max
            if (i == livesRemaining)
            {//hide this health bar
                AudioSource.PlayClipAtPoint(hitClip, GameManager.Instance.player.transform.position, 1f);
                healthBars[i].enabled = false;
            }

        }
    
    
    }
    public void AddLives()
    {

        for (int i = 0; i < 4; i++)
        {//do nothing till we got hit the max
            //AudioSource.PlayClipAtPoint(hitClip, GameManager.Instance.player.transform.position, 1f);
            GameManager.Instance.player.Health = 4;
            healthBars[i].enabled = true;
        }


    }

}
