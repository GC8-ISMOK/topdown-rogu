using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 4f;
    
    private void Start() 
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    public void Spawn()
    {
        if(!spawned)
        {
            if(direction == Direction.Top)
            {
                rand = Random.Range(0, variants.TopRooms.Length);
                Instantiate(variants.TopRooms[rand], transform.position, variants.TopRooms[rand].transform.rotation);
            }
            else if(direction == Direction.Left)
            {
                rand = Random.Range(0, variants.LeftRooms.Length);
                Instantiate(variants.LeftRooms[rand], transform.position, variants.LeftRooms[rand].transform.rotation);
            }
            else if(direction == Direction.Bottom)
            {
                rand = Random.Range(0, variants.BottomRooms.Length);
                Instantiate(variants.BottomRooms[rand], transform.position, variants.BottomRooms[rand].transform.rotation);
            }
            else if(direction == Direction.Right)
            {
                rand = Random.Range(0, variants.RightRooms.Length);
                Instantiate(variants.RightRooms[rand], transform.position, variants.RightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned && direction != Direction.None)
        {
            Destroy(gameObject);
            
        }
    }
}
