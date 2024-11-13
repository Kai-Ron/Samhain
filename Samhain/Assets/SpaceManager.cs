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
            StartCoroutine(ShiftWorldDown());
            return;
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
            currentTime += Time.deltaTime;

            globalVolume.weight = endTime - currentTime;

            yield return null;
        }
    }
}
