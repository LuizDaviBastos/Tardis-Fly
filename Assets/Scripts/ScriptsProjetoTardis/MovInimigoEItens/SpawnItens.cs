using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnItens : MonoBehaviour
{
    public float rangeEmYMin;
    public float rangeEmYMax;
    public float _speed = 5f;
    public List<PrefabsItens> prefabsList;

    private bool meteoros = true;
    

    private void FixedUpdate()
    {
        AtirarMeteoro();
    }


    void AtirarMeteoro()
    {
        if (meteoros)
        {
            meteoros = false;
            StartCoroutine(AtirarM());
        }

        IEnumerator AtirarM()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 20f));
            AtirarItem(prefabsList.ElementAt(UnityEngine.Random.Range(0,prefabsList.Count - 1)), 5f);
            meteoros = true;
        }
    }


    void AtirarItem(PrefabsItens prefab, float speed)
    {
        var instant = Instantiate(prefab.Prefab, new Vector2(transform.position.x, transform.position.y + UnityEngine.Random.Range(rangeEmYMin, rangeEmYMax)), Quaternion.identity);

        var destino = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);

        instant.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);

    }


}

