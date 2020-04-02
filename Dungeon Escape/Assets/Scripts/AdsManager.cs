using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded Ad");

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult

            };
            Advertisement.Show("rewardedVideo", options);
        }
        Debug.Log("doneeeeeeeeee");
    }

    void HandleShowResult(ShowResult result)
    {

        /*if (result == ShowResult.Failed)
        {
            Debug.Log("The video failed, it must not have been ready");
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.Log("Ypu skipped the ad! Np Gems for you!");
        }
        else
        {
            Debug.Log("100 GEMSSSSSSSSSSS");
        }*/
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("The video failed, it must not have been ready");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ypu skipped the ad! Np Gems for you!");
                break;
            case ShowResult.Finished:
                Debug.Log("100 GEMSSSSSSSSSSS");
                GameManager.Instance.player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.player.diamonds);
                break;
        }

    }
}
