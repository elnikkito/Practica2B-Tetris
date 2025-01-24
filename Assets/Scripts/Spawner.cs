using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] pieces;
    
    void Start()
    {
        SpawnNext();
    }
    
    void Update()
    {

    }
    
    public void SpawnNext(){
        
        int i=Random.Range(0, pieces.Length);
        Vector3 v3 = new Vector3(4, 15, 0);
        Instantiate(pieces[i], v3, Quaternion.identity);

    
    }
}
