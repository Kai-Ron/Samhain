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

    List<GameObject> realWorldItems = new List<GameObject>();
    List<GameObject> ghostWorldItems = new List<GameObject>();

    private void Start()
    {
        realWorldItems.AddRange(GameObject.FindGameObjectsWithTag("Real"));
        ghostWorldItems.AddRange(GameObject.FindGameObjectsWithTag("Ghost"));
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

            for (int i = 0; i < realWorldItems.Count; i++)
            {
                realWorldItems[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < ghostWorldItems.Count; i++)
            {
                ghostWorldItems[i].gameObject.SetActive(false);
            }

            StartCoroutine(ShiftWorldDown());
            return;
        }

        for (int i = 0; i < realWorldItems.Count; i++)
        {
            realWorldItems[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < ghostWorldItems.Count; i++)
        {
            ghostWorldItems[i].gameObject.SetActive(true);
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
            currentTime += Time.deltaTime;
            globalVolume.weight = endTime - currentTime;
            yield return null;
        }
    }
}
