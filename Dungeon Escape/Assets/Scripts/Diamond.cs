using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;

    [SerializeField]
    private AudioClip _collectDiamondClip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddGems(gems);
                AudioSource.PlayClipAtPoint(_collectDiamondClip, other.transform.position, 0.5f);
                Destroy(this.gameObject);
            }
        }
    }
}
