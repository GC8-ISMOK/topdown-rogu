using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject wallEffect;
    public GameObject door;
    public GameObject[] enemyTypes;
    public Transform[] enemySpawner;
    public GameObject shield;
    public GameObject healthPotion; 
    public List<GameObject> enemies;
    private bool Wintrue = false;
    private RoomVariants variants;
    public int g;
    private bool spawned;
    private bool wallsDestroyed;
    private void Start() 
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !spawned)
        {
            spawned = true;
            foreach(Transform spawner in enemySpawner)
            {
                int rand = Random.Range(0,11);
                if(rand < 9)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; 
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform; 
                }
                else if(rand == 9)
                {
                    Instantiate(healthPotion, spawner.position, Quaternion.identity); 
                }
                else if(rand == 10)
                {
                    Instantiate(shield, spawner.position, Quaternion.identity); 
                }
                StartCoroutine(CheckEnemies());
                Destroy(spawner.gameObject);
            }
        }
        else if(other.CompareTag("Player") && spawned){
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && spawned){
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = true;
            }
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach(GameObject wall in walls)
        {
            if(wall != null && wall.transform.childCount != 0)
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
                
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(wallsDestroyed && other.CompareTag("wall"))
        {
            Destroy(other.gameObject);
        }
    }
    private void Update() {
        if(GameObject.Find("EnemySpawner") == null)
        {
            if(GameObject.Find("BigDemon") == null && GameObject.Find("Demon")== null)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                Wintrue = true;
                Debug.Log(Wintrue);
            }
            Debug.Log("test2");
        }
    }
}
