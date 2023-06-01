using System.Collections;
using System.Collections.Generic;
using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] private BoxOverlapSensor sensor;
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject enemyAt;

    private EnemySenseFlag state;

    public static GameObject CurrentTargetObject;
    private int targetSelectedId = 0; //in case there's queue aim selection;

    private GameObject dummyLookAt;
    private void Awake()
    {
        dummyLookAt = new GameObject("Dummy");
        dummyLookAt.transform.SetParent(transform);
    }
    private void Update()
    {
        sensor.UpdateSensor();
        arrow.gameObject.SetActive(state == EnemySenseFlag.DETECTED);

        //Check if there are'nt detected enemies nearby
        if (!sensor.HasHit || sensor.HitCount < 1)
        {
            CurrentTargetObject = null;
            state = EnemySenseFlag.IDLE;
            return;
        }

        if (state == EnemySenseFlag.IDLE)
        {
            //get reference detected objects
            CurrentTargetObject = sensor.HitColliders[targetSelectedId].gameObject;
            state = EnemySenseFlag.DETECTED;
        }
        else
        {
            //get transform look at from current selected object for aim purposes
            dummyLookAt.transform.LookAt(CurrentTargetObject.transform);
            enemyAt.transform.position = CurrentTargetObject.transform.position;
            arrow.localEulerAngles = new Vector3(arrow.localEulerAngles.x, dummyLookAt.transform.localEulerAngles.y, arrow.localEulerAngles.z);
        }
    }
}


