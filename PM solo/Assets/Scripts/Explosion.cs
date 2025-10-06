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

    public IEnumerator explosionWait(float time, GameObject proj)
    {
        Debug.Log("Triggered");
        yield return new WaitForSeconds(time);
        GameObject e = Instantiate(explosion, proj.transform.position, proj.transform.rotation);
        Destroy(proj);
        Destroy(e, explosionLifespan);
        
    }
}
