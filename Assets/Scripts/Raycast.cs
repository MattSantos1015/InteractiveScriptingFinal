using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [Header("RaycastParams")]
    #region RaycastParams
    public float raycastRange;
    public float shootRange;
    #endregion


    [Header("GunParams")]
    #region GunParams
    public int gunDamage;

    public int gun2Damage;
    public int maxClipSize = 12;
    public int clip;
    public int ammo;
    public int startingAmmo = 100;
    public int gun2Clip;

    public int gun2MaxClipSize;

    public int gun2Ammo;

    //Physics
    public float bulletForce;
    #endregion

    [Header("GameObjects")]
    #region GameObjects
    public GameObject redKey;
    public GameObject heldKeyGFX;

    public GameObject gun;
    public GameObject gunGFX;
    public GameObject Bullet;


    public GameObject gun2;

    public GameObject gun2GFX;

    public GameObject redDoor;

    #endregion

    [Header("Transforms")]
    #region Transforms
    public Transform bulletSpawn;
    #endregion

    #region Bools
    bool hasRedKey = false;
    bool hasGun = false;
    bool gunOut = false;

    bool gunOut2 = false;

    bool hasGun2 = false;
    #endregion



    [Header("UI")]
    #region UI
    public Text clipTXT;
    public Text ammoTXT;

    public GameObject clipUI;
    public GameObject ammoUI;
    public GameObject slashUI;
    #endregion
    AudioSource gunShot;

    void Start()
    {
        clip += maxClipSize;
        ammo += startingAmmo;
        gunShot = gameObject.AddComponent<AudioSource>();

    }


   void Update()
{
    clipTXT.text = clip.ToString();
    ammoTXT.text = ammo.ToString();

    if (Input.GetKeyDown("e"))
    {
        CastRay();
    }

    if (Input.GetKeyDown(KeyCode.Mouse0) && hasGun)
    {
        gunShot.Play();
    }

    if (Input.GetMouseButtonDown(0) && clip >= 1 && gunOut)
    {
        GunShootRaycast();
        clip--;
    }

    if (Input.GetKeyDown(KeyCode.Mouse0) && hasGun2 && gunOut2 && gun2Clip >= 1)
    {
        GunShootRaycast();
        gun2Clip--;
        Debug.Log("Gun2 is shooting");
    }

    if (Input.GetKeyDown("r") && ammo >= 1 && gunOut)
    {
        int reloadAmount = maxClipSize - clip;
        clip += reloadAmount;
        ammo -= reloadAmount;
    }

    if (clip <= 0 && ammo >= 1 && gunOut)
    {
        if (ammo >= maxClipSize)
        {
            ammo -= maxClipSize;
            clip += maxClipSize;
        }
        else
        {
            clip += ammo;
            ammo -= ammo;
        }
    }

    if (clip <= 0 && ammo >= 1 && gunOut2 && gun2Clip >= 1)
    {
        if (ammo >= maxClipSize)
        {
            ammo -= maxClipSize;
            gun2Clip += maxClipSize;
        }
        else
        {
            gun2Clip += ammo;
            ammo -= ammo;
        }
    }

    if (ammo <= 0)
    {
        ammoTXT.text = 0.ToString();
    }

    if (gunOut)
    {
        clipTXT.text = clip.ToString();
        ammoTXT.text = ammo.ToString();
    }
    else if (gunOut2)
    {
        clipTXT.text = gun2Clip.ToString();
        ammoTXT.text = gun2Ammo.ToString();
    }

    GunControls();
    GunControls2(); // Ensure that this method is called
}


    void CastRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
        {
            if (hit.transform.gameObject.tag == "redKey")
            {
                Destroy(redKey);
                heldKeyGFX.SetActive(true);
                hasRedKey = true;
            }

            if (hit.transform.gameObject.tag == "redKeyHole" && hasRedKey)
            {
                Destroy(redDoor);
                heldKeyGFX.SetActive(false);
            }

            if (hit.transform.gameObject.tag == "Gun")
            {
                gunOut = true;
                hasGun = true;
                Destroy(gun);
                gunGFX.SetActive(true);
            }
            if(hit.transform.gameObject.tag == "Gun2")
            {
                gunOut2 = true;
                hasGun = true;
                gun2GFX.SetActive(true);
                Destroy(gun2);
                
                
            }
        }
    }

    void GunControls()
    {
        if (hasGun)
        {
            clipUI.SetActive(true);
            ammoUI.SetActive(true);
            slashUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (gunOut)
                {
                    gunGFX.SetActive(false);
                    gunOut = false;
                    gun2GFX.SetActive(true);
                    gunOut2 = true;
                }
                else
                {
                    gunGFX.SetActive(true);
                    gunOut = true;
                    gun2GFX.SetActive(false);
                   gunOut2 = false;
                }
            }
        }
        else
        {
            clipUI.SetActive(false);
            ammoUI.SetActive(false);
            slashUI.SetActive(false);
        }

    
    }

    void GunControls2()
    {
        if(hasGun2)
        {
            clipUI.SetActive(true);
            ammoUI.SetActive(true);
            slashUI.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
               if(gunOut2)
               {
                gun2GFX.SetActive(false);
                gunOut2 = false;
                gunGFX.SetActive(true);
                gunOut = true;
               }
               else
               {
                gun2GFX.SetActive(true);
                gunOut2 = true;
                gunGFX.SetActive(false);
                gunOut = false;

               }

            }


        }
    }

    void GunShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, shootRange))
        {
            if (hit.transform.gameObject.tag == "enemy" && hasGun && gunOut)
            {
                hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(gunDamage);
            }
            if (hit.transform.gameObject.tag == "enemy" && hasGun2 && gunOut2)
            {
                hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(gun2Damage);
            }
        }
        
        
    }

    void GunShootPhysics()
    {
        if (hasGun && gunOut)
        {
            GameObject tempBullet = Instantiate(Bullet, bulletSpawn.position, Quaternion.identity);
            Rigidbody temprRB = tempBullet.GetComponent<Rigidbody>();
            temprRB.AddForce(bulletSpawn.forward * 10 * bulletForce);
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                gunShot.Play();
            }
            
        }

    }
public void ReplenishAmmo(int amount)
{
    ammo = Mathf.Min(ammo + amount, startingAmmo);
    if (gunOut)
    {
        clip = Mathf.Min(clip + amount, maxClipSize);
    }
    else if (gunOut2)
    {
        gun2Clip = Mathf.Min(gun2Clip + amount, gun2MaxClipSize);
    }
}

}
