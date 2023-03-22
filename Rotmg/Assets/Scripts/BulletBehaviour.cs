using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    
    public Vector3 dir;
    public float bulletSpeed = 3f;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += dir * Time.deltaTime * bulletSpeed;
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        
    }
}
