using System.Collections;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float lookInterval = 0.1f;
 [Range(30,110)]
[SerializeField] private float fieldOfView = 75;
    

    private GameObject[] badGuys;
    private Transform emitter;

    // Start is called before the first frame update
    void Start()
    {
        emitter = this.transform.GetChild(0);
        badGuys = GameObject.FindGameObjectsWithTag("enemy");
        StartCoroutine(CheckForBadGuys());
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic here if needed
    }

    IEnumerator CheckForBadGuys()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (GameObject guy in badGuys)
            {
                // Use the position of the guy directly, not the sum of positions
                Ray ray = new Ray(emitter.position, guy.transform.position - emitter.position);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, fieldOfView))
                {
                    if (hit.collider.CompareTag("enemy"))
                    {
                        Vector3 targetDir = guy.transform.position - emitter.position;
                        float angle = Vector3.Angle(targetDir, emitter.forward);

                        if (angle < 90.0f)
                        {
                            Debug.Log("Found guy!");
                            Debug.DrawRay(emitter.position, guy.transform.position - emitter.position, Color.green, 4);
                        }
                        else
                        {
                            Debug.DrawRay(emitter.position, guy.transform.position - emitter.position, Color.yellow, 4);
                        }
                    }
                    else
                    {
                        Debug.DrawRay(emitter.position, guy.transform.position - emitter.position, Color.red, 4);
                    }
                }
            }
        }
    }
}
