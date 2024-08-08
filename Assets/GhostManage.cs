using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManage : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float rotationSpeed = 2f;
    public float moveSpeed = 3f;
    public string playerTag = "Player";
    private Transform player;
    private bool isChasing = false;
    private List<Renderer> childRenderers = new List<Renderer>();
    private List<Color> originalColors = new List<Color>();
    private Renderer mainRenderer;
    private Color originalColor;

    void Awake()
    {
        GetChildRenderers(transform);
        mainRenderer = GetComponent<Renderer>();
        originalColor = mainRenderer.material.color;
        SetChildAndMainObjectsAlpha(0f);
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
                SetChildAndMainObjectsAlpha(1f);
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
                SetChildAndMainObjectsAlpha(0f);
            }
        }
        else
        {
            isChasing = false;
            SetChildAndMainObjectsAlpha(0f);
        }
    }

    void SetChildAndMainObjectsAlpha(float alpha)
    {
        StartCoroutine(FadeChildAndMainObjects(alpha));
    }

    IEnumerator FadeChildAndMainObjects(float targetAlpha)
    {
        float startAlpha = (targetAlpha == 0f) ? 1f : 0f;
        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

            for (int i = 0; i < childRenderers.Count; i++)
            {
                Color color = childRenderers[i].material.color;
                color.a = currentAlpha;
                childRenderers[i].material.color = color;
            }

            Color mainColor = mainRenderer.material.color;
            mainColor.a = currentAlpha;
            mainRenderer.material.color = mainColor;

            elapsedTime += Time.deltaTime;
        }
        yield return null;
    }

    void GetChildRenderers(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name != "eye")
            {
                Renderer renderer = child.GetComponent<Renderer>();
                childRenderers.Add(renderer);
                originalColors.Add(renderer.material.color);
                GetChildRenderers(child);
            }
        }
    }
}
