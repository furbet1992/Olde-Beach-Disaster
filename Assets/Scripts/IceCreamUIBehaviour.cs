using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceCreamUIBehaviour : MonoBehaviour
{
    public GameObject pirateParent;
    public GameObject iceCreamSpritePrefab;
    public Vector3 offset;
    Camera mainCamera;
    List<GameObject> iceCreamSprites = new List<GameObject>();
    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(pirateParent.transform.position + offset);
    }

    public void SetIceCreams(List<Flavors> flavors)
    {
        float width = GetComponent<RectTransform>().sizeDelta.x / 2 - 12.5f;
        for (int i = 0; i < flavors.Count; i++)
        {
            iceCreamSprites.Add(Instantiate(iceCreamSpritePrefab));
            iceCreamSprites[i].transform.SetParent(this.transform);
            iceCreamSprites[i].GetComponent<IceCreamSpriteBehavior>().Init((int)flavors[i]);
            iceCreamSprites[i].transform.localPosition = new Vector3(-width + i * 25, 0);
        }
    }

    public bool UpdateFlavors(List<Flavors> incomingFlavors)
    {
        int iterator = 0;

        if (incomingFlavors.Count >= iceCreamSprites.Count)
        {
            for (int i = 0; i < iceCreamSprites.Count; i++)
            {
                if (incomingFlavors[i] == (iceCreamSprites[i].GetComponent<IceCreamSpriteBehavior>().flavor))
                {
                    iterator++;
                }
            }
        }

        if (iterator == iceCreamSprites.Count)
        {
            for (int i = 0; i < iceCreamSprites.Count; i++)
            {
                Destroy(iceCreamSprites[i]);
                iceCreamSprites.RemoveAt(i);
            }
            return true;
        }

        return false;
    }
}
