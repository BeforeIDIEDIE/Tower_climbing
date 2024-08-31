using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManage : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float rotationSpeed = 2f;
    public float moveSpeed = 3f;
    public string playerTag = "Player";
    public Material originalMaterial;
    public Material transparentMaterial;
    private Transform player;
    private bool isChasing = false;
    private List<Renderer> childRenderers = new List<Renderer>();
    private Renderer mainRenderer;

    void Awake()
    {
        GetChildRenderers(transform);
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null) 
            {
                renderer.material = transparentMaterial;
            }
        }
        mainRenderer = GetComponent<Renderer>();
        if (mainRenderer != null) 
        {
            mainRenderer.material = transparentMaterial;
        }
    }

    void Update()
    {
        if (!isChasing)
        {
            DetectPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerTag))
            {
                player = hitCollider.transform;
                isChasing = true;
                StartCoroutine(BlinkAndSetMaterial(originalMaterial));
                return;
            }
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) > detectionRadius)
            {
                isChasing = false;
                player = null;
                StartCoroutine(BlinkAndSetMaterial(transparentMaterial));
            }
        }
        else
        {
            isChasing = false;
            StartCoroutine(BlinkAndSetMaterial(transparentMaterial));
        }
    }

    IEnumerator BlinkAndSetMaterial(Material targetMaterial)
    {
        int blinkCount = 2;
        float blinkDuration = 0.1f;

        for (int i = 0; i < blinkCount; i++)
        {
            if (mainRenderer != null)
            {
                mainRenderer.material = transparentMaterial;
            }
            SetMaterialForAllRenderers(transparentMaterial);
            yield return new WaitForSeconds(blinkDuration);
            if (mainRenderer != null) 
            {
                mainRenderer.material = originalMaterial;
            }
            SetMaterialForAllRenderers(originalMaterial);
            yield return new WaitForSeconds(blinkDuration);
        }

        SetMaterialForAllRenderers(targetMaterial);
        mainRenderer.material = targetMaterial;
    }

    void SetMaterialForAllRenderers(Material material)
    {
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    void GetChildRenderers(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name != "eye")
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    childRenderers.Add(renderer);
                }
                GetChildRenderers(child);
            }
        }
    }
}
