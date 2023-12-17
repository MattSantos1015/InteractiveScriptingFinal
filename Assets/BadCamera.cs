using System.Collections;
using UnityEngine;

public class YourClassName : MonoBehaviour
{
    [SerializeField] private float lookInterval = 0.1f;
    [Range(30, 110)]
    [SerializeField] private float fieldOfView = 75;

    private GameObject player;
    private Transform emitter;
    private bool canSeePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        emitter = transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("target");
        StartCoroutine(CheckForBadGuys());
    }

    IEnumerator CheckForBadGuys()
    {
        while (true)
        {
            yield return new WaitForSeconds(lookInterval);

            // Use the position of the player directly, not the sum of positions
            Ray ray = new Ray(emitter.position, player.transform.position - emitter.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, fieldOfView))
            {
                if (hit.collider.CompareTag("target"))
                {
                    Vector3 targetDir = player.transform.position - emitter.position;
                    float angle = Vector3.Angle(targetDir, emitter.forward);

                    if (angle < 90.0f)
                    {
                        Debug.Log("Found guy!");
                        Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.green, 4);
                       StartCoroutine (CallTurrets());
                    }
                    else
                    {
                        canSeePlayer = false;
                        Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.yellow, 4);
                    }
                }
                else
                {
                    canSeePlayer = false;
                    Debug.DrawRay(emitter.position, player.transform.position - emitter.position, Color.red, 4);
                }
            }
        }
    }

    IEnumerator CallTurrets()
    {
        if (canSeePlayer == false)
        {
            canSeePlayer = true;
            yield return new WaitForSeconds(1);
            if (canSeePlayer)
            {
                GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
                foreach (GameObject turret in turrets)
                {
                    turret.GetComponent<TurretController>().Activate();
                }
            }
        }
    }
}
