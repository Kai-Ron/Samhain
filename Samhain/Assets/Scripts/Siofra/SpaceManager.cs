using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager instance;
    public Volume globalVolume;

    private bool isSpirit;
    private float timeToChange = 1.0f;

    List<Collider> realWorldColliders = new List<Collider>();
    List<Collider> ghostWorldColliders = new List<Collider>();

    List<MeshRenderer> realWorldRenderers = new List<MeshRenderer>();
    List<MeshRenderer> ghostWorldRenderers = new List<MeshRenderer>();

    public Material realWorldMaterial;
    public Material ghostWorldMaterial;

    private void Start()
    {
        List<GameObject> realWorldObjects = new List<GameObject>();
        List<GameObject> ghostWorldObjects = new List<GameObject>();

        realWorldObjects.AddRange(GameObject.FindGameObjectsWithTag("Real"));
        ghostWorldObjects.AddRange(GameObject.FindGameObjectsWithTag("Ghost"));

        foreach (GameObject item in realWorldObjects)
        {
            if (item.TryGetComponent<Collider>(out Collider collider))
            {
                realWorldColliders.Add(item.GetComponent<Collider>());
            }

            foreach (Collider colliderComp in item.GetComponentsInChildren<Collider>())
            {
                realWorldColliders.Add(colliderComp);
            }

            foreach (MeshRenderer renderer in item.GetComponentsInChildren<MeshRenderer>())
            {
                realWorldRenderers.Add(renderer);
            }
        }

        foreach (GameObject item in ghostWorldObjects)
        {
            if (item.TryGetComponent<Collider>(out Collider collider))
            {
                realWorldColliders.Add(item.GetComponent<Collider>());
            }

            foreach (Collider colliderComp in item.GetComponentsInChildren<Collider>())
            {
                ghostWorldColliders.Add(colliderComp);
            }

            foreach (MeshRenderer renderer in item.GetComponentsInChildren<MeshRenderer>())
            {
                ghostWorldRenderers.Add(renderer);
            }
        }

        for (int i = 0; i < ghostWorldColliders.Count; i++)
        {
            ghostWorldColliders[i].enabled = false;
        }

        for (int i = 0; i < ghostWorldRenderers.Count; i++)
        {
            ghostWorldRenderers[i].enabled = false;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ChangeState()
    {
        if (isSpirit)
        {
            // Change to grey
            isSpirit = false;

            for (int i = 0; i < realWorldColliders.Count; i++)
            {
                realWorldColliders[i].enabled = true;
            }

            for (int i = 0; i < ghostWorldColliders.Count; i++)
            {
                ghostWorldColliders[i].enabled = false;
            }

            for (int i = 0; i < ghostWorldRenderers.Count; i++)
            {
                ghostWorldRenderers[i].enabled = false;
            }

            for (int i = 0; i < realWorldRenderers.Count; i++)
            {
                realWorldRenderers[i].enabled = true;
            }

            StartCoroutine(ShiftWorldDown());
            return;
        }

        for (int i = 0; i < realWorldColliders.Count; i++)
        {
            realWorldColliders[i].enabled = false;
        }

        for (int i = 0; i < ghostWorldColliders.Count; i++)
        {
            ghostWorldColliders[i].enabled = true;
        }

        for (int i = 0; i < ghostWorldRenderers.Count; i++)
        {
            ghostWorldRenderers[i].enabled = true;
        }

        for (int i = 0; i < realWorldRenderers.Count; i++)
        {
            realWorldRenderers[i].enabled = false;
        }

        isSpirit = true;
        StartCoroutine(ShiftWorldUp());
        return;
    }

    private IEnumerator ShiftWorldUp()
    {
        float currentTime = Time.time;
        float endTime = currentTime + timeToChange;

        while (currentTime <= endTime)
        {
            // Change Alpha of objects here too
            // realWorldMaterial.SetFloat("_Alpha", endTime - currentTime);
            // ghostWorldMaterial.SetFloat("_Alpha", 1 - (endTime - currentTime));
            globalVolume.weight = 1 - (endTime - currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ShiftWorldDown()
    {
        float currentTime = Time.time;
        float endTime = currentTime + timeToChange;

        while (currentTime <= endTime)
        {
            // Change Alpha of objects here too
            // realWorldMaterial.SetFloat("_Alpha", 1 - (endTime - currentTime));
            // ghostWorldMaterial.SetFloat("_Alpha", endTime - currentTime);
            currentTime += Time.deltaTime;
            globalVolume.weight = endTime - currentTime;
            yield return null;
        }
    }
}
