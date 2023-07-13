using System;
using UnityEngine;
using Lean.Pool;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
public class Shop : MonoBehaviour
{
    [SerializeField] private BlockBehavior baseObject1;
    [SerializeField] private BlockBehavior2 baseObject;
   
    [SerializeField] private MeshRenderer baseMeshRenderer;
    [SerializeField] private MeshRenderer baseMeshRenderer2;// Referencia al MeshRenderer de la base
    [SerializeField] private int basePrice; // Precio de la base en monedas
    [SerializeField] private Dictionary<Material, SkinData> skinsDictionary = new Dictionary<Material, SkinData>();
    [SerializeField] private CoinSystem coinSystem1;
    [SerializeField] private CoinSystem2 coinSystem; // Referencia al CoinSystem
    [SerializeField] private GameObject tower;
    [SerializeField] private Material oldMaterialToUse;
    [SerializeField] private Material newMaterialToUse;
    [SerializeField] private TextMeshProUGUI priceSkin;
    
    [SerializeField] private MaterialData rendMats2;
    [System.Serializable]

    public class MaterialData
    {
        
        
        
        public int price;
        public bool[] purchasedSkins = { false,false,false,false,false,false,false,false,false,false,false};

        public void setPrice(int price)
        {
            this.price=price;
        }

        public int getPrice()
        {
            print(this.price);
            return this.price;
        }
    }

    [System.Serializable]
    public class SkinData
    {
        public Material material;
        public int price;
        public bool isPurchased;
    }
    public Material[] newMaterials;  //your list of new materials

    public Material[] rendMats;   //temp list, a copy of rend.Materials
    private int matnum  ;
    private bool isPurchased = false; // Indica si la base ha sido comprada
    private int skinN;

    private void Start()
    {
        rendMats[0]= newMaterials[0];

        //baseMeshRenderer.enabled = false;
        rendMats = baseMeshRenderer.materials;


    }
    public void skinPurchase()
    {
        for (int i = 0; i < rendMats2.purchasedSkins.Length; i++)
        {

            string key = "PurchasedSkin" + i.ToString();
            int value = rendMats2.purchasedSkins[i] ? 1 : 0;
            PlayerPrefs.SetInt(key, value);
            int value2 = PlayerPrefs.GetInt(key);
            print("Skin no:"+value2);

            if (PlayerPrefs.GetInt(key)== 1)
            {

                //print("Skin comprada almacenado");
                PlayerPrefs.SetInt("Skin", skinN);
                PlayerPrefs.Save();
                //    for (int i = 0; i < rendmats2.purchasedskins.length; i++)
                //    {
                //        string key = "purchasedskin" + i.tostring();
                //        int value = playerprefs.getint(key);

                //        if (value == 1)
                //        {

                //            // realizar las acciones correspondientes para el elemento comprado
                //            // por ejemplo, aplicar el material al objeto base
                //            basemeshrenderer.materials = rendmats;
                //            basemeshrenderer2.materials = rendmats;

                //            // ...
                //        }
                //        else
                //        {
                //            //coincount -= priceskin;
                //            basemeshrenderer.materials = rendmats;
                //            basemeshrenderer2.materials = rendmats;
                //        }
                //    }


            }

            else
            {
               // print("Skin no comprada");
            }

        }
       

    }

    public void PurchaseBase(int matnum2)
    {
        // Obtener la cantidad de monedas guardadas en memoria
        int coinCount = PlayerPrefs.GetInt("Coins");
       // print("Monedas actuales:"+coinCount);
        // Calcular el precio de la skin usando el valor de matnum
        int priceSkin = skinN * 1000;
        rendMats[0]= newMaterials[matnum2];
        if (priceSkin <= coinCount && rendMats2.purchasedSkins[skinN] == false)
        {
            //print("no. skin: "+skinN);
            
            rendMats2.purchasedSkins[skinN] = true;
            // Guardar la nueva cantidad de monedas en memoria
            PlayerPrefs.SetInt("Coins", coinCount);
            //print("Comprada skin:"+matnum2);
            skinPurchase();
            // Actualizar la interfaz de usuario para mostrar la nueva cantidad de monedas
            baseMeshRenderer.materials = rendMats;
            baseMeshRenderer2.materials = rendMats;

            if (skinN >15)
            {   
                skinN=0;
                matnum2=0;
            }
            else
            {
                skinN=matnum2;
            }
            
            
            PlayerPrefs.SetInt("Skin", skinN);
            PlayerPrefs.Save();
        }

        else
        {
           // print("No alcanza:");
            // Mostrar un mensaje o realizar alguna acción si no se tienen suficientes monedas para comprar la skin
        }

        baseMeshRenderer.materials = rendMats;
        baseMeshRenderer2.materials = rendMats;
        //rendMats2.setPrice(50);
        //rendMats2.getPrice();

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
