﻿using UnityEngine;
using System.Collections;


public class TurretController : MonoBehaviour
{
    [SerializeField]
    private int m_frontTurretMax;
    [SerializeField]
    private int m_rearLeftTurretMax;
    [SerializeField]
    private int m_rearRightTurretMax;

    private int m_frontTurret;
    private int m_rearLeftTurret;
    private int m_rearRightTurret;

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnTurretEvent;
    }

    void Start()
    {
        m_frontTurret = m_frontTurretMax;
        m_rearLeftTurret = m_rearLeftTurretMax;
        m_rearRightTurret = m_rearRightTurretMax;
    }

    public void frontTurretImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_frontTurret--;
            PhotonNetwork.RaiseEvent(4, m_frontTurret, true, null);
            Debug.Log("m_frontTurret " + m_frontTurret);
        }
    }

    public void rearLeftTurretImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rearLeftTurret--;
            PhotonNetwork.RaiseEvent(5, m_rearLeftTurret, true, null);
            Debug.Log("m_rearLeftTurret " + m_rearLeftTurret);
        }
    }

    public void rearRightTurretImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rearRightTurret--;
            PhotonNetwork.RaiseEvent(6, m_rearRightTurret, true, null);
            Debug.Log("m_rearRightTurret " + m_rearRightTurret);
        }
    }

    public int GetTurretLifeLevel(int TurretID)
    {
        switch(TurretID)
        {
            case 1:
                return m_frontTurret;
            case 2:
                return m_rearLeftTurret;
            case 3:
                return m_rearRightTurret;
            default:
                return -1;
        }
    }

    private void OnTurretEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == 4)
            m_frontTurret = (int)content;
        else if (eventcode == 5)
            m_rearLeftTurret = (int)content;
        else if (eventcode == 6)
            m_rearRightTurret = (int)content;
    }
}
