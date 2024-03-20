using UnityEngine;

public class Walla : MonoBehaviour
{
    public GameObject block;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Block"))
        {
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
}
