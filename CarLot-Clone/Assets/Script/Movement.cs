using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool ParkExit = false;
    public bool Enter = true;
    public static Movement instance;
    public string playerData;
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        Debug.Log("orspu oldu kod" + playerData);

        RandomCarSelect();

    }
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (ParkExit == true)
        {
            StartCoroutine(ParkCarExit());

        }


    }
    private void RandomCarSelect()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BackGround")
        {
            PlayerData.instance.AddCurrency(100);
            ParkExit = true;

        }
    }
    IEnumerator ParkCarExit()
    {
        yield return new WaitForSeconds(4f);
        agent.enabled = false;
        transform.Translate(0, 0, -5f * Time.deltaTime);

        Destroy(gameObject, 4f);

    }

}


