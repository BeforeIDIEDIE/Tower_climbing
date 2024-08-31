using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossFloorEntryTrigger : MonoBehaviour
{
    public Slider healthBar;
    private bool used = false;
    [SerializeField] private GameObject Boss;
    private void OnTriggerEnter(Collider other)
    {
        if (used)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Boss.SetActive(true);
            healthBar.gameObject.SetActive(true);
            used = true;
        }
    }
}
