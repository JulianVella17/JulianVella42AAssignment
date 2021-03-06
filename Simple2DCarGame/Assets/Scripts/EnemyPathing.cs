﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> waypoints;

    [SerializeField] WaveConfig waveConfig;

    [SerializeField] int scoreValue = 5;

    [SerializeField] AudioClip PointGain;
    //allows to change volume accordingly from 0 to 1
    [SerializeField] [Range(0, 1)] float PointGainVolume = 0.75f;

    //saves the waypoint in which we want to go
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //get the list waypoints from waveConfig
        waypoints = waveConfig.GetWaypoints();

        //set the start point to waypoint 0
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            //set the target position to the waypoints where we want to go
            var targetPosition = waypoints[waypointIndex].transform.position;
            
            //to make sure that the x-axis is always 0
            targetPosition.z = 0f;

            var enemyMovement = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            //Move Enemy from current position to target position, at enemy movement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            //if enemy reach the last waypoint
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }

        //if enemy reaches last waypoint
        else
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            print("ADD");

            AudioSource.PlayClipAtPoint(PointGain, Camera.main.transform.position, PointGainVolume);

            Destroy(gameObject);
        }
    }

    //set a wave config 
    public void SetWaveConfig(WaveConfig WaveConfigToSet)
    {
        waveConfig = WaveConfigToSet;
    }
}
