using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Weapons currentWeapon;
    public GameObject bullet;
    private bool isShooting= false;
    public float attackSpeed = 10f;
    private float timer;
    private float timeBetweenShots = 1f;
    private Vector3 dir;
    
    Camera cam;
    private void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
            
        }

        if (context.canceled)
        {
            isShooting = false;
            
        }
    }
    public void Aim(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
        dir = cam.ScreenToWorldPoint(dir);
        dir -= transform.position;
        dir.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShots = 10 / (attackSpeed*currentWeapon.firerate);
        if (isShooting)
        {
            bool positive = true;
            int lastResult = 0;
            timer += Time.deltaTime;
            if (timer>=timeBetweenShots)
            {
                for (int i = 1; i <= currentWeapon.bulletCount; i++)
                {
                    
                    if (currentWeapon.diverging)
                    {
                        Vector3 apparationPoint = new Vector3(transform.position.x,transform.position.y,0) + new Vector3(dir.x, dir.y, 0).normalized* .5f;
                        float baseangle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                        float myangle = 0f;
                        if (currentWeapon.bulletCount%2 == 0)
                        {
                            if (positive)
                            {
                                myangle = baseangle+(currentWeapon.offset* (i-lastResult));
                                lastResult = (i - lastResult);
                            }
                            else
                            {
                                myangle = baseangle-(currentWeapon.offset*(i*.5f));
                                
                            }
                        }
                        else
                        {
                            if (positive)
                            {
                                myangle = baseangle + (currentWeapon.offset * (i - (i + 1) * .5f));
                                
                            }
                            else
                            {
                                myangle = baseangle - (currentWeapon.offset * (i *.5f));

                            }
                        }

                        
                        
                        Vector2 newdir = new Vector2(Mathf.Cos(myangle*Mathf.Deg2Rad), Mathf.Sin(myangle * Mathf.Deg2Rad));
                        
                        
                        Instantiate(bullet,apparationPoint, Quaternion.identity).GetComponent<BulletBehaviour>().dir = new Vector3(newdir.x, newdir.y, 0).normalized;
                        positive = !positive;
                    }

                    if (currentWeapon.straight)
                    {
                        float baseangle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        float myangle = 0f;
                        if (currentWeapon.bulletCount % 2 == 0)
                        {
                            if (positive)
                            {
                                myangle = baseangle + (currentWeapon.offset * (i - lastResult));
                                lastResult = (i - lastResult);
                            }
                            else
                            {
                                myangle = baseangle - (currentWeapon.offset * (i * .5f));

                            }
                        }
                        else
                        {
                            if (positive)
                            {
                                myangle = baseangle + (currentWeapon.offset * (i - (i + 1) *.5f));

                            }
                            else
                            {
                                myangle = baseangle - (currentWeapon.offset * (i * .5f));

                            }
                        }
                        
                        

                        Vector2 newdir = new Vector2(Mathf.Cos(myangle * Mathf.Deg2Rad), Mathf.Sin(myangle * Mathf.Deg2Rad));

                        Vector3 apparationPoint = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(newdir.x, newdir.y, 0).normalized * .5f;

                        GameObject newBullet = Instantiate(bullet, apparationPoint, Quaternion.identity);
                        newBullet.GetComponent<BulletBehaviour>().dir = new Vector3(dir.x, dir.y, 0).normalized;
                        Debug.Log(newBullet.GetComponent<BulletBehaviour>().dir);
                        Quaternion.AngleAxis(myangle, Vector3.forward);
                        positive = !positive;
                    }

                    if (currentWeapon.converging)
                    {
                        float baseangle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        float myangle = 0f;
                        float myanglev2 = 0f;
                        if (currentWeapon.bulletCount % 2 == 0)
                        {
                            if (positive)
                            {
                                myangle = baseangle + (currentWeapon.offset * (i - lastResult));
                                myanglev2 = baseangle - (currentWeapon.offset * (i - lastResult));
                                lastResult = (i - lastResult);
                                
                            }
                            else
                            {
                                myangle = baseangle - (currentWeapon.offset * (i * .5f));
                                myanglev2 = baseangle + (currentWeapon.offset * (i * .5f));
                            }
                        }
                        else
                        {
                            if (positive)
                            {
                                myangle = baseangle + (currentWeapon.offset * (i - (i + 1) * .5f));
                                myanglev2 = baseangle - (currentWeapon.offset * (i - (i + 1) * .5f));
                            }
                            else
                            {
                                myangle = baseangle - (currentWeapon.offset * (i * .5f));
                                myanglev2 = baseangle + (currentWeapon.offset * (i * .5f));
                            }
                        }



                        Vector2 newdir = new Vector2(Mathf.Cos(myangle * Mathf.Deg2Rad), Mathf.Sin(myangle * Mathf.Deg2Rad));
                        Vector2 newdir2 = new Vector2(Mathf.Cos(myanglev2 * Mathf.Deg2Rad), Mathf.Sin(myanglev2 * Mathf.Deg2Rad));

                        Vector3 apparationPoint = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(newdir.x, newdir.y, 0).normalized * .5f;

                        Vector3 convergePoint = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(dir.x, dir.y, 0).normalized * currentWeapon.convergingRange;

                        
                        GameObject newBullet = Instantiate(bullet, apparationPoint, Quaternion.identity);
                        newBullet.GetComponent<BulletBehaviour>().dir = (convergePoint - apparationPoint).normalized;
                        Debug.Log(newBullet.GetComponent<BulletBehaviour>().dir);
                        
                        positive = !positive;
                    }

                }
                
                timer = 0;
            }
            
        }
        
    }

    
}
