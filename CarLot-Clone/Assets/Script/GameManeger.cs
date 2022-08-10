using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManeger : PlayerData
{
    [SerializeField] public GameObject Car;
    [SerializeField] private Transform Spawn;
    [SerializeField] private Transform EnterCount;
    [SerializeField] private Text carMoney;
    private bool parkmoneyControl=false;
    private NavMeshAgent _agent;
    GameObject Ins_Car;
    int Money = 0;
    int activeindex = 0;
    int oldindex;
    void Start()
    {
        CreateCar();


    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Movement>())
                {
                    _agent = hit.transform.GetComponent<NavMeshAgent>();
                }
                if (hit.transform.tag == "BackGround")
                {
                    _agent.destination = hit.point;
                    _agent.transform.parent = null;
                }
                if (hit.transform.tag == "MoneyPoint")
                {
                    ParkBuy();
                }
               
            }
            
        }
        ParkControl();  
        Money = PlayerPrefs.GetInt("Money");
        carMoney.text = Money.ToString();

    }
   
    void CreateCar()
    {
        for (int i = 0; i < EnterCount.childCount; i++)
        {
            if (EnterCount.GetChild(i).gameObject.activeInHierarchy && EnterCount.GetChild(i).childCount <= 0)
            {
                Ins_Car = Instantiate(Car, Spawn.position, Quaternion.identity);
                //Ins_Car.transform.position = Vector3.MoveTowards(Ins_Car.transform.position, EnterCount.GetChild(i).position, speed * Time.deltaTime);
                Ins_Car.GetComponent<Movement>().agent.destination = EnterCount.GetChild(i).transform.position;
                Ins_Car.transform.parent = EnterCount.GetChild(i);

            }

        }
        StartCoroutine(CreateCarTime());

    }

    IEnumerator CreateCarTime()
    {

        yield return new WaitForSeconds(3f);
        CreateCar();
    }
    void ParkBuy()
    {
        if (Money >= data[activeindex].parkCurrency)
        {
            activeindex++;
            PlayerPrefs.SetInt("ParkIndex", activeindex);
            parkmoneyControl = true;
        }
    }
    void ParkControl()
    {
        activeindex = PlayerPrefs.GetInt("ParkIndex");
        for (int i = 0; i < data.Length; i++)
        {
            if (i <= activeindex )
            {
                data[i].parks.SetActive(true);
            }
            if (activeindex == i)
            {
                parkmoneyList[activeindex].GetComponent<TextMesh>().text = data[activeindex].parkCurrency.ToString();
                parkmoneyList[activeindex].SetActive(true);
            }
            else
            {
                parkmoneyList[i].SetActive(false);
            }
        }
        if (parkmoneyControl)
        {
            DeductCurrency(data[activeindex-1].parkCurrency);
            parkmoneyControl=false;
        }
    }


}
