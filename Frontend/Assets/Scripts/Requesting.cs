using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Requesting : MonoBehaviour
{
    public float scaleFactor;
    public GameObject[] objectArr;
    public GameObject[] rotationReferences;
    public GameObject[] prefabCarArray;
    public GameObject[] trafficLightsArray;
    public  List<GameObject> spawnedCarsList;
    public int amountOfCars;
    public  List<Vector3> targetPositions = new List<Vector3>();
    public string test;
    private bool carsSpawned=false;

    void Start() {
        StartCoroutine(loopRequests());
    }
    
    IEnumerator loopRequests(){
        while(true){
            StartCoroutine(GetPositions());
            Debug.Log("---------Nuevo Request realizado-------------");
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator GetPositions() {
        float inicio = Time.time;
        
        Data infoSimul;
        using(UnityWebRequest requestPositions = UnityWebRequest.Get("http://localhost:8080/")){
            yield return requestPositions.SendWebRequest();
            if (requestPositions.result != UnityWebRequest.Result.Success) {
                Debug.Log(requestPositions.error);
            }else{
                Debug.Log(requestPositions.downloadHandler.text);
            }
            infoSimul=JsonUtility.FromJson<Data>(requestPositions.downloadHandler.text);   //Mapeo de JSON
        }


        int numSpawnedCar = 0;
        amountOfCars = infoSimul.cars.Length;
        
        Debug.Log("Total de carros:"+amountOfCars);

        if(carsSpawned==false){
            foreach(Car c in infoSimul.cars){
                Vector3 spawnCarro = new Vector3(c.x,c.z,c.y);
                if(numSpawnedCar == amountOfCars){
                    numSpawnedCar=0;
                }
                GameObject newCar = Instantiate(prefabCarArray[numSpawnedCar], spawnCarro, rotationReferences[c.direction].transform.rotation);
                newCar.name = "Car "+c.id;
                newCar.transform.localScale = new Vector3(0.5f, 0.5f,0.5f);
                spawnedCarsList.Add(newCar);
                numSpawnedCar++;
            }
            carsSpawned = true;
        }
        Debug.Log("----------Movimiento----------");
        foreach(Car c in infoSimul.cars){
            Vector3 movePosition = new Vector3(c.x,c.z,c.y);
            spawnedCarsList[c.id].transform.position=movePosition;
        }

        //Setup de luces
        foreach(TrafficLights luz in infoSimul.traffic_lights){
            Light componenteLuz = trafficLightsArray[luz.id].GetComponent<Light>();
            
            Color estadoSemaforo=Color.red;
            if(luz.state==0){
                estadoSemaforo=Color.red;
            }else if(luz.state==1){
                estadoSemaforo=Color.yellow;
            }else if(luz.state==2){
                estadoSemaforo=Color.green;
            }
            componenteLuz.color = estadoSemaforo;
            Debug.Log("Semaforo:"+ luz.id+" estado:"+luz.state);
        }
    }
    void Update() {
    }

}
