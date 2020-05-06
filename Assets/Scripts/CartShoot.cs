using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartShoot : MonoBehaviour
{
    public GameObject iceCreamPrefab;
    public int maxFlavors = 3;
    public float iceCreamSpawnOffset = 2.0f;
    List<Flavors> currentIceCream = new List<Flavors>();

    public List<RawImage> scoopUI;
    public Texture chocolateTexture;
    public Texture mintTexture;
    public Texture strawberryTexture;
    public Texture vanillaTexture;

    public AudioSource shoot;
    public AudioSource selectFlavor;
    public AudioSource cancelIceCream;

    bool playSelectSound = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (RawImage image in scoopUI)
        {
            image.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playSelectSound = false;

        if (currentIceCream.Count < maxFlavors && Input.GetKeyDown(KeyCode.A))
        {
            currentIceCream.Add(Flavors.Chocolate);
            scoopUI[currentIceCream.Count - 1].enabled = true;
            scoopUI[currentIceCream.Count - 1].texture = chocolateTexture;
            playSelectSound = true;
        }
        if (currentIceCream.Count < maxFlavors && Input.GetKeyDown(KeyCode.S))
        {
            currentIceCream.Add(Flavors.Mint);
            scoopUI[currentIceCream.Count - 1].enabled = true;
            scoopUI[currentIceCream.Count - 1].texture = mintTexture;
            playSelectSound = true;
        }
        if (currentIceCream.Count < maxFlavors && Input.GetKeyDown(KeyCode.D))
        {
            currentIceCream.Add(Flavors.Strawberry);
            scoopUI[currentIceCream.Count - 1].enabled = true;
            scoopUI[currentIceCream.Count - 1].texture = strawberryTexture;
            playSelectSound = true;
        }
        if (currentIceCream.Count < maxFlavors && Input.GetKeyDown(KeyCode.F))
        {
            currentIceCream.Add(Flavors.Vanilla);
            scoopUI[currentIceCream.Count - 1].enabled = true;
            scoopUI[currentIceCream.Count - 1].texture = vanillaTexture;
            playSelectSound = true;
        }

        if (playSelectSound)
        {
            selectFlavor.Play();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentIceCream.Clear();
            foreach (RawImage image in scoopUI)
            {
                image.enabled = false;
                cancelIceCream.Play();
            }
        }

        if (currentIceCream.Count > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject iceCream = Instantiate(iceCreamPrefab);
            iceCream.GetComponent<IceCreamDetails>().Init(currentIceCream, new Vector3(transform.position.x, transform.position.y, transform.position.z + iceCreamSpawnOffset));
            currentIceCream.Clear();
            foreach (RawImage image in scoopUI)
            {
                image.enabled = false;
            }
            shoot.Play();
        }
    }
}
