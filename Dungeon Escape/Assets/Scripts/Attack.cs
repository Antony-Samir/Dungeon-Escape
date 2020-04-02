using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private AudioClip attackClip;

    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit" + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (_canDamage == true)
            {
                AudioSource.PlayClipAtPoint(attackClip, this.transform.position, 1f);
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ReseDamage());
            }
            
        }
    }

    IEnumerator ReseDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    
    
    }
}
