using UnityEngine;
using System.Collections;

public class StationManager : MonoBehaviour {

    private int[] station1Status = new int[2]; // station1status[0] => capture lvl, station1Status[1] team ownership
    private int[] station2Status = new int[2];
    private int[] station3Status = new int[2];
    private int[] station4Status = new int[2];

    // Use this for initialization
    void Start () {
        station1Status[0] = 100;
        station1Status[1] = 0;

        station2Status[0] = 100;
        station2Status[1] = 0;

        station3Status[0] = 100;
        station3Status[1] = 0;

        station4Status[0] = 100;
        station4Status[1] = 0;

        InvokeRepeating("StationRegen", 1f, 1f);


    }

    // Update is called once per frame
    void StationRegen () {
        if(station1Status[0] < 100)
            station1Status[0]++;
        if (station2Status[0] < 100)
            station2Status[0]++;
        if (station3Status[0] < 100)
            station3Status[0]++;
        if (station4Status[0] < 100)
            station4Status[0]++;

    }

    public void updateStatus(int stationID,int teamAttacking)
    {
        Debug.Log("Station " + stationID + " In Capture by " + teamAttacking);
        switch(stationID)
        {
            case 1:
                if(teamAttacking != station1Status[1])
                    station1Status[0]--;
                if (station1Status[0] < 0)
                    station1Status[1] = teamAttacking;
                break;
            case 2:
                if (teamAttacking != station2Status[1])
                    station2Status[0]--;
                if (station2Status[0] < 0)
                    station2Status[1] = teamAttacking;
                break;
            case 3:
                if (teamAttacking != station3Status[1])
                    station3Status[0]--;
                if (station2Status[0] < 0)
                    station2Status[1] = teamAttacking;
                break;
            case 4:
                if (teamAttacking != station4Status[1])
                    station4Status[0]--;
                if (station2Status[0] < 0)
                    station2Status[1] = teamAttacking;
                break;
        }
        
    }

    public int getStationStatus(int stationID)
    {
        switch (stationID)
        {
            case 1:
                return station1Status[0];
            case 2:
                return station2Status[0];
            case 3:
                return station3Status[0];
            case 4:
                return station4Status[0];
            default:
                return -1;
        }
    }
}
