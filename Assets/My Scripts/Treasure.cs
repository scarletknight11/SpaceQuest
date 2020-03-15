using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	public int value = 10;
	public GameObject explosionPrefab;
 

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (GameManager.gm!=null) {
				// tell the game manager to Collect
				GameManager.gm.Collect (value);

			}
			
			// explode if specified
			if (explosionPrefab != null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}

            if (GameManager2.gm2 != null) {
                GameManager2.gm2.Collect2(value);
            }

            if (GameManager3.gm3 != null) {
                GameManager3.gm3.Collect3(value);
            }

            if (GameManager4.gm4 != null)
            {
                GameManager4.gm4.Collect4(value);
            }

            // destroy after collection
            Destroy(gameObject);
		}
	}
}
