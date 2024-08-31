using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class falling : MonoBehaviour
{
    public PlayerHealthSystem hs;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hs != null)
            {
                hs.Die();
            }
        }
    }
}
