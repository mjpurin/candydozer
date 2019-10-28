﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int SphereCandyFrequency = 3;
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int sampleCandyCount;
    int shotPower = MaxShotPower;
    AudioSource shotSound;
    public GameObject[] candyPrefabs;
    public GameObject[] candySquarePrefabs;
    
    public CandyHolder candyHolder;
    //public GameObject candyPrefab;
    public float shotSpeed;
    public float shotTorque;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shot();
        }
    }
    GameObject SampleCandy() {
        GameObject prefab = null;
        if(sampleCandyCount % SphereCandyFrequency == 0) {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];
        } else {
            int index = Random.Range(0, candySquarePrefabs.Length);
            prefab = candySquarePrefabs[index];
        }

        sampleCandyCount++;
        return prefab;
    }
    Vector3 GetInstantiatePosition() {
        float x = baseWidth * (Input.mousePosition.x / Screen.width)
            - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }
    public void Shot() {
        if (candyHolder.GetCandyAmount() <= 0) {
            return;
        }
        if(shotPower <= 0) {
            return;
        }
        GameObject candy = Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
            );
      
        candy.transform.parent = candyHolder.transform;
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotSpeed);
        candyRigidBody.AddTorque(new Vector3(shotTorque, shotTorque,  0));
        candyHolder.ConsumeCandy();
        ConsumePower();
        shotSound.Play();
       
        
    }
    private void OnGUI() {
        GUI.color = Color.black;
        string label = "";
        for(int i = 0; i < shotPower; i++) {
            label = label + "+";
        }
        GUI.Label(new Rect(0, 15, 100, 30), label);
    }
    void ConsumePower() {
        shotPower--;
        StartCoroutine(RecoveryPower());
    }
    IEnumerator RecoveryPower() {
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
