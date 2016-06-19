using UnityEngine;
using System.Collections;

public class StationManager : MonoBehaviour {

    public int[] station1Status = new int[2]; // station1status[0] => capture lvl, station1Status[1] team ownership
    public int[] station2Status = new int[2];
    public int[] station3Status = new int[2];
    public int[] station4Status = new int[2];

    public GameObject[] stationObject;
    public ParticleSystem[] particles;
    public Color teamBlue;
    public Color teamRed;

    void Start ()
    {
        station1Status[0] = 100;
        station1Status[1] = -1;

        station2Status[0] = 100;
        station2Status[1] = -1;

        station3Status[0] = 100;
        station3Status[1] = -1;

        station4Status[0] = 100;
        station4Status[1] = -1;
        particles = new ParticleSystem[4];
        for (int i = 0; i < stationObject.Length; i++)
            particles[i] = stationObject[i].GetComponent<ParticleSystem>();
        //InvokeRepeating("StationRegen", 1f, 1f);
    }

    void StationRegen ()
    {
       /* if(attack)
        {
            if (station1Status[0] < 100)
                station1Status[0]++;
            if (station2Status[0] < 100)
                station2Status[0]++;
            if (station3Status[0] < 100)
                station3Status[0]++;
            if (station4Status[0] < 100)
                station4Status[0]++;
        }*/
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
                {
                    station1Status[1] = teamAttacking;
                    station1Status[0] = 100;
                    if (teamAttacking == 0)
                        particles[0].startColor = teamBlue;
                    else
                        particles[0].startColor = teamRed;
                }
                break;
            case 2:
                if (teamAttacking != station2Status[1])
                    station2Status[0]--;
                if (station2Status[0] < 0)
                {
                    station2Status[1] = teamAttacking;
                    station2Status[0] = 100;
                    if (teamAttacking == 0)
                        particles[1].startColor = teamBlue;
                    else
                        particles[1].startColor = teamRed;
                }
                break;
            case 3:
                if (teamAttacking != station3Status[1])
                    station3Status[0]--;
                if (station3Status[0] < 0)
                {
                    station3Status[1] = teamAttacking;
                    station3Status[0] = 100;
                    if (teamAttacking == 0)
                        particles[2].startColor = teamBlue;
                    else
                        particles[2].startColor = teamRed;
                }
                break;
            case 4:
                if (teamAttacking != station4Status[1])
                    station4Status[0]--;
                if (station4Status[0] < 0)
                {
                    station4Status[1] = teamAttacking;
                    station4Status[0] = 100;
                    if (teamAttacking == 0)
                        particles[3].startColor = teamBlue;
                    else
                        particles[3].startColor = teamRed;
                }
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

    public bool checkStationWin(int idTeam)
    {
        if (station1Status[1] == idTeam && station2Status[1] == idTeam && station3Status[1] == idTeam && station4Status[1] == idTeam)
            return true;
        else
            return false;
    }
}
