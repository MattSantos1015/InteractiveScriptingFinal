using Unity.VisualScripting;
using UnityEngine;

public class AgroAI : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private float dist; // distance
    public float enemySpeed;
    public float prox; // proximity

    private Transform spawn;
    public float resetDistance;
    public float lungeMultiplier;
    public float lungeDistance;
    bool speedChange = false;
    private Rigidbody rb;


    public float explosionForce;


    public float teleportDistance;

    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("spawn").transform;
        target = GameObject.FindGameObjectWithTag("target").transform;   // sets the ai's target to anything tagged "target"
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (target == null)
    {
        // Attempt to find the target again
        target = GameObject.FindGameObjectWithTag("target")?.transform;

        // If still null, log an error and return
        if (target == null)
        {
            Debug.LogError("Target is null. Make sure you have a game object with the 'target' tag in your scene.");
            return;
        }
    }
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= prox) 
        {
            transform.LookAt(target);
            rb.AddForce(transform.forward * enemySpeed * Time.deltaTime);
        }


        if(dist <= lungeDistance)
        {
            if (!speedChange) 
            {
                enemySpeed = enemySpeed * lungeMultiplier;
                speedChange = true;
            }

            Destroy(gameObject, .5f); // if not using a whole number include an f for float\\
        }

        if(dist <= teleportDistance)
        {
           transform.position = target.position;
           if (CompareTag("target"))
            {
                //rb.AddExplosionForce(explosionForce * 100);

            }
        }
        if(dist <= resetDistance) 
        {
            target.position = spawn.position;
        }

    }

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  
        Gizmos.DrawWireSphere(transform.position, prox);


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, lungeDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, teleportDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, resetDistance);
    }
}
