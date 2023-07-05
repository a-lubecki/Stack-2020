using System;
using UnityEngine;
using Lean.Pool;

public class Shop : MonoBehaviour
{
    [SerializeField] private BlockBehavior2 baseObject;
    [SerializeField] private MeshRenderer baseMeshRenderer; // Referencia al MeshRenderer de la base
    [SerializeField] private int basePrice; // Precio de la base en monedas
    [SerializeField] private CoinSystem coinSystem; // Referencia al CoinSystem
    [SerializeField] private GameObject tower;
    [SerializeField] private Material oldMaterialToUse;
    [SerializeField] private Material newMaterialToUse;

    public Material[] newMaterials;  //your list of new materials

    public Material[] rendMats;   //temp list, a copy of rend.Materials
    private int matnum ;
    private bool isPurchased = false; // Indica si la base ha sido comprada

    private void Start()
    {
        // Desactivar el MeshRenderer de la base inicialmente
        rendMats = baseMeshRenderer.materials;
    }

    public void PurchaseBase(int matnum2)
    {

        rendMats[0] = newMaterials[matnum2];
        print("numero skin:"+matnum2);
        baseMeshRenderer.materials = rendMats;
        //if (coinSystem.HasEnoughCoins(basePrice) && !isPurchased)
        //{
        //    // Descontar el precio de la base del CoinSystem
        //    //coinSystem.RemoveCoins(basePrice);

        //    // Activar el MeshRenderer de la base
        // baseObject.materials[0] = materialToUse;

        //    // Marcar la base como comprada
        //    isPurchased = true;

        //    // Llamar a cualquier otra función o realizar acciones adicionales después de la compra
        //    // ...
        //}
    }
}
