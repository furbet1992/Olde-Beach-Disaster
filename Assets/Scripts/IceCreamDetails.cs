using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum Flavors
{
    Chocolate,
    Mint,
    Strawberry,
    Vanilla,
    FlavorCount
};

public class IceCreamDetails : MonoBehaviour
{
    [HideInInspector]
    public List<Flavors> Scoops;

    public float moveSpeed = 5.0f;
    public float scoopOffset = 0.5f;

    public Material chocolateMaterial;
    public Material mintMaterial;
    public Material strawberryMaterial;
    public Material vanillaMaterial;

    public Mesh scoopMesh;
    public void Init(List<Flavors> flavors, Vector3 position)
    {
        foreach (Flavors flavor in flavors)
        {
            Scoops.Add(flavor);
        }

        transform.position = position;
        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, moveSpeed), ForceMode.Impulse);

        Transform parent = this.transform;
        GameObject scoop;

        foreach (Flavors flavor in Scoops)
        {
            scoop = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            scoop.GetComponent<Collider>().enabled = false;
            scoop.GetComponent<MeshFilter>().mesh = scoopMesh;
            scoop.transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z + scoopOffset);
            scoop.transform.SetParent(parent);
            parent = scoop.transform;

            Renderer scoopMaterial = scoop.GetComponent<Renderer>();
            switch (flavor)
            {
                case Flavors.Chocolate:
                    scoopMaterial.material = chocolateMaterial;
                    break;
                case Flavors.Mint:
                    scoopMaterial.material = mintMaterial;
                    break;
                case Flavors.Strawberry:
                    scoopMaterial.material = strawberryMaterial;
                    break;
                case Flavors.Vanilla:
                    scoopMaterial.material = vanillaMaterial;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "IceCreamDestroyer")
        {
            Destroy(gameObject);
        }
    }
}