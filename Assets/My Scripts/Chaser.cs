using UnityEngine;
using System.Collections;
using enableGame;
using UnityEngine.Networking;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {
	
	//public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;
    egFloat EnemySpeed = 0.4f;

    public GameObject explosionPrefab;


    // Use this for initialization
    //   void Start () 
    //{
    //       //if no target specified, assume the player
    //       if (target == null) {

    //		if (GameObject.FindWithTag ("Player")!=null)
    //		{
    //			target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
    //		}
    //	}
    //}


    void Awake()
    {
        VariableHandler.Instance.Register(RBParameterStrings.ENEMY_SPEED, EnemySpeed);
        print("EnemySpeed=" + EnemySpeed);
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        // face the target
        transform.LookAt(target);

        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);

        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if (distance > minDist)
            transform.position += transform.forward * EnemySpeed * Time.deltaTime;
    }

    //   // Update is called once per frame
    //   void Update () 
    //{
    //	if (target == null)
    //		return;

    //	// face the target
    //	transform.LookAt(target);

    //	//get the distance between the chaser and the target
    //	float distance = Vector3.Distance(transform.position,target.position);

    //	//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
    //	if(distance > minDist)	
    //		transform.position += transform.forward * EnemySpeed * Time.deltaTime;	
    //}

    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
