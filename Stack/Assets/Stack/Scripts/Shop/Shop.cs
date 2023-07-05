using System;
using UnityEngine;
using Lean.Pool;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Shop : MonoBehaviour
{
    [SerializeField] private BlockBehavior baseObject1;
    [SerializeField] private BlockBehavior2 baseObject;
    [SerializeField] private MeshRenderer baseMeshRenderer; // Referencia al MeshRenderer de la base
    [SerializeField] private int basePrice; // Precio de la base en monedas
    [SerializeField] private Dictionary<Material, SkinData> skinsDictionary = new Dictionary<Material, SkinData>();
    [SerializeField] private CoinSystem coinSystem1;
    [SerializeField] private CoinSystem2 coinSystem; // Referencia al CoinSystem
    [SerializeField] private GameObject tower;
    [SerializeField] private Material oldMaterialToUse;
    [SerializeField] private Material newMaterialToUse;
    [SerializeField] private bool[] purchasedSkins;

    [System.Serializable]
    public class SkinData
    {
        public Material material;
        public int price;
        public bool isPurchased;
    }
    public Material[] newMaterials;  //your list of new materials

    public Material[] rendMats;   //temp list, a copy of rend.Materials
    private int matnum ;
    private bool isPurchased = false; // Indica si la base ha sido comprada
    private int skinN;
    private void Start()
    {
        //baseMeshRenderer.enabled = false;
         rendMats = baseMeshRenderer.materials;
        // Asignar precios y estado de compra a cada skin
        foreach (KeyValuePair<Material, SkinData> pair in skinsDictionary)
        {
            pair.Value.isPurchased = false; // Marcar todas las skins como no compradas inicialmente
        }
        // Desactivar el MeshRenderer de la base inicialmente
       
    }

    public void CurrentSkin(int matnum2)
    {

        rendMats[0] = newMaterials[matnum2];
        print("Skin puesta:"+matnum2);
        baseMeshRenderer.materials = rendMats;
        PurchaseSkin(newMaterials[matnum2]);
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


    public void PurchaseBase(int matnum2)
    {

        rendMats[0] = newMaterials[matnum2];
        print("Comprada skin:"+matnum2);
        
        baseMeshRenderer.materials = rendMats;
        PurchaseSkin(newMaterials[matnum2]);
        skinN=matnum2;
        PlayerPrefs.SetInt("Skin", skinN);
        PlayerPrefs.Save();
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

    public void PurchaseSkin(Material material)
    {
        if (skinsDictionary.TryGetValue(material, out SkinData skinData))
        {
            if (!skinData.isPurchased && coinSystem.HasEnoughCoins(skinData.price))
            {
                // Descontar el precio de la skin del CoinSystem
                coinSystem.RemoveCoins(skinData.price);

                // Activar el MeshRenderer de la base
                baseMeshRenderer.enabled = true;

                // Asignar el nuevo material a la base
                baseMeshRenderer.material = material;

                // Marcar la skin como comprada
                skinData.isPurchased = true;

                // Guardar el estado de compra en el dispositivo (por ejemplo, utilizando PlayerPrefs)
                PlayerPrefs.SetInt(material.name + "_IsPurchased", 1);
            }
        }
    }

}
