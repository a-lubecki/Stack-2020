// @author Mohammad T. Sarker
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour{

	// Use this for initialization

	public GameObject explosion;
	private GameObject player;

	void Start () {
		player = GameObject.Find ("Player");

	}

	void OnCollisionEnter(Collision collision){

        //if (collision.gameObject.tag=="Blocks")
        //{
        //    GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        //    Destroy(explosionInstance, 1f);

        //}
        if (collision.gameObject.tag=="Fruits")
		{
            
            //Destroy(gameObject);
            Destroy(collision.gameObject);

        }

        else
		{
            //GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
           
            //Destroy(explosionInstance, 2f); // Destruir la instancia de la explosión después de 2 segundos
                                            // Destroy(gameObject);
                                            //Destroy(collision.gameObject);
        }


    }







}
