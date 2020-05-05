using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceCreamSpriteBehavior : MonoBehaviour
{
    Image image;
    public Sprite[] iceCreamSprites;
    public Flavors flavor;

    public void Init(int flavorIndex)
    {
        image = GetComponent<Image>();
        image.sprite = iceCreamSprites[flavorIndex];
        flavor = (Flavors)flavorIndex;
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
    }

}
