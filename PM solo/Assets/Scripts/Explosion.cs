using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;

    public float explosionLifespan = 2;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode(GameObject proj)
    {
        GameObject e = Instantiate(explosion, proj.transform.position, proj.transform.rotation);

        Destroy(proj);
        Destroy(e, explosionLifespan);
    }


    


        
    
}
