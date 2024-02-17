using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;
    [SerializeField]

    private GameObject poolingObjectPrefab;


    public GameObject basicSpellMagicObjectPrefab;
    public GameObject fireSpellMagicObjectPrefab;


    [SerializeField]
    Queue<Magic_Object> basicSpellMagicObjectQueue = new Queue<Magic_Object>();
    [SerializeField]
    Queue<Magic_Object> fireSpellMagicObjectQueue = new Queue<Magic_Object>();


    private void Awake()

    {
        Instance = this;
        Initialize(10);

    }
    private void Initialize(int initCount)

    {

        for (int i = 0; i < initCount; i++)

        {

            basicSpellMagicObjectQueue.Enqueue(CreateNewObject(basicSpellMagicObjectPrefab));
            fireSpellMagicObjectQueue.Enqueue(CreateNewObject(fireSpellMagicObjectPrefab));
        }

    }



    private Magic_Object CreateNewObject(GameObject obj)

    {

        var newObj = Instantiate(obj).GetComponent<Magic_Object>();

        newObj.gameObject.SetActive(false);

        newObj.transform.SetParent(transform);

        return newObj;

    }



    public static Magic_Object GetObject(string value)

    {
        if(value.ToString() == "fire")
        {
            Debug.Log("FIRE!!!!!!!!!");
            if (Instance.fireSpellMagicObjectQueue.Count > 0)
            {

                var obj = Instance.fireSpellMagicObjectQueue.Dequeue();

                obj.transform.SetParent(null);

                obj.gameObject.SetActive(true);

                return obj;

            }
            else
            {
                return null;
            }
            

            /*/ 접근문제로 인한 코드 일시 봉인..
            else

            {

                var newObj = Instance.CreateNewObject();

                newObj.gameObject.SetActive(true);

                newObj.transform.SetParent(null);

                return newObj;

            }
            */


        }
        else
        {
            if (Instance.basicSpellMagicObjectQueue.Count > 0)
            {
                Debug.Log("BASIC!!!!!!!!!");
                var obj = Instance.basicSpellMagicObjectQueue.Dequeue();

                obj.transform.SetParent(null);

                obj.gameObject.SetActive(true);

                return obj;

            }
            else
            {
                return null;
            }
        }
        

    }



    public static void ReturnObject(Magic_Object obj)

    {

        obj.gameObject.SetActive(false);

        obj.transform.SetParent(Instance.transform);

        Instance.basicSpellMagicObjectQueue.Enqueue(obj);

    }

}


