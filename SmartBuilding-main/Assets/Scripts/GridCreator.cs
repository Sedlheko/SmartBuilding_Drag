using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public GameObject block1; 
 
    public int worldWidth  = 10;
    public int worldHeight  = 10;
    public int i = 0;
    public int j = 0;
    public float spawnSpeed = 0;
 
    void  Start () {
        StartCoroutine(CreateWorld());
    }
 
    IEnumerator CreateWorld () {
        for(int x = -(worldWidth-1); x < worldWidth; x++) {
            j = j+1;
            yield return new WaitForSeconds(spawnSpeed);
            for(int z = 0; z < worldHeight; z++) {  
                i = z+1;              
                yield return new WaitForSeconds(spawnSpeed);
                GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                block.transform.parent = transform;
                block.transform.localPosition = new Vector3(x, 1, z-((worldWidth/2)-1));
                block.gameObject.name = "c"+j.ToString() + "r" +i.ToString();
            }
        }
    }
}
