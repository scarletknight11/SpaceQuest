using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public Image currentHealthbar;
    public Text ratioText;

    public float hitpoint = 1;
    public float maxHitpoint = 1;

	// Use this for initialization
	void Start () {
		
	}
	
    private void TakeDamage(float damage)
    {
        hitpoint -= damage;
        if (hitpoint < 0)
        {
            hitpoint = 0;
            Debug.Log("Dead");
        }
    }

    private void HealDamage(float heal)
    {

    }

}
