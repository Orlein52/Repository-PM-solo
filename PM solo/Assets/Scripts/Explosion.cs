using UnityEngine;


public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public Weapon weapon;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(float a)
    {

        Destroy(gameObject, a);
        
        
    }

}
