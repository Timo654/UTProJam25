using System.Security.Cryptography;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GameObject playerSpawnPoint;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpawnPoint = GameObject.FindWithTag("PlayerSpawnPoint");
        Spawn();
    }

    void Spawn()
    {
        Instantiate(player, playerSpawnPoint.transform.position, Quaternion.identity);
    }

    public void Die()
    {
        Destroy(player);
    }

}
