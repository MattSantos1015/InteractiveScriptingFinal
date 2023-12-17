using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform emitter;
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject laserPrefab;
    private bool canSeePlayer = true;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float detectionAngle = 45.0f; // Set the angle for detection

    private Queue<Rigidbody> laserPool = new Queue<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;

        // Start the coroutine in Start
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer) transform.LookAt(player);
    }

    public void Activate()
    {
        // anim.SetTrigger("Activate");
        StartCoroutine(LookForPlayer());
    }

    IEnumerator LookForPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            Ray ray = new Ray(emitter.position, player.position - emitter.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("target"))
                {
                    Vector3 targetDir = player.position - emitter.position;
                    float angle = Vector3.Angle(targetDir, emitter.forward);

                    if (angle < 45)
                    {
                        FoundPlayer();
                        Debug.Log("I see the player ");
                        Debug.DrawRay(emitter.position, player.position - emitter.position, Color.green, 4);
                    }
                    else
                    {
                        LostPlayer();
                        Debug.DrawRay(emitter.position, player.position - emitter.position, Color.yellow, 4);
                    }
                }
                else
                {
                    LostPlayer();
                    Debug.DrawRay(emitter.position, player.position - emitter.position, Color.red, 4);
                }
            }
        }
    }

    void FoundPlayer()
    {
        if (canSeePlayer = true)
        {
            anim.SetTrigger("Firing");
            canSeePlayer = true;
        }
    }

    void LostPlayer()
    {
        if (canSeePlayer = false)
        {
            anim.SetTrigger("Idle");
            canSeePlayer = false;
            this.transform.position = startPosition;
            this.transform.rotation = startRotation;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Debug.Log("Pow!");
            Rigidbody rb;
            if (laserPool.Count > 0)
            {
                rb = laserPool.Dequeue();
                rb.gameObject.SetActive(true);
                rb.velocity = Vector3.zero;
                StartCoroutine(StoreLaser(rb));
                laser.transform.position = emitter.position;
                laser.transform.rotation = emitter.rotation;
            }
            else
            {
                GameObject laser = Instantiate(laserPrefab, emitter.position, emitter.rotation) as GameObject;
                rb = laser.GetComponent<Rigidbody>();
            }

            rb.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator StoreLaser(Rigidbody laser)
    {
        yield return new WaitForSeconds(0.2f);
        if (!laser.gameObject.activeSelf)
        {
            laserPool.Enqueue(laser);
            laser.gameObject.SetActive(false);
        }
    }
}
