using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering.UI;

public class SpiritSpace : MonoBehaviour
{

    public Material fullscreenSaturation;

    public float GreyLevel;

    public List<GameObject> spiritObjects = new List<GameObject>();
    public List<GameObject> normalObjects = new List<GameObject>();

    //tag items with spirit if they only exist in the spirit world
    //tag items with normal if they only exist in the regular world


    void Start()
    {
        spiritObjects.AddRange(GameObject.FindGameObjectsWithTag("spirit"));
        normalObjects.AddRange(GameObject.FindGameObjectsWithTag("normal"));


    }

    void Update()
    {
        //change the greyscale
        GreyLevel = GreyLevel + Input.GetAxis("Mouse ScrollWheel");
        fullscreenSaturation.SetFloat("_greyness", GreyLevel);
        GreyLevel = Mathf.Clamp(GreyLevel, 0, 1);

        //enter Spirit
        
        if (GreyLevel == 0)
        {
            foreach(GameObject gc in spiritObjects)
            {
                gc.SetActive(true);
            }
        }
        else if (GreyLevel == 1)
        {
            foreach (GameObject gc in normalObjects)
            {
                gc.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject gc in normalObjects)
            {
                gc.SetActive(false);
            }
            foreach (GameObject gc in spiritObjects)
            {
                gc.SetActive(false);
            }
        }
        
        

    }

}
