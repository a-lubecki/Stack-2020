// @author Mohammad T. Sarker
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour{

	// Use this for initialization

	public GameObject explosion;
	private GameObject player;

	void Start () {
		player = GameObject.Find ("Blocks");

	}

	void OnCollisionEnter(Collision collision){

        if (collision.gameObject.tag=="Blocks")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);


        }
        if (collision.gameObject.tag=="Ground")
		{
            player.GetComponent<BlockBehavior>().destroyedParts(collision.gameObject.name);
            Instantiate(explosion, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            //Destroy(collision.gameObject);

        }

        else
		{
            Instantiate(explosion, transform.position, Quaternion.identity);
           // Destroy(gameObject);
            //Destroy(collision.gameObject);
        }


    }






}
