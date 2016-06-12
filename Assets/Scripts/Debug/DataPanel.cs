using UnityEngine;
using System.Collections;

public class DataPanel : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text m_DataText;
    [SerializeField]
    private LifePartController m_LifeParts;

	void Update ()
    {
        m_DataText.text = getShipStateFormated();
    }

    string getShipStateFormated()
    {
        string result = "";
        result += "Shield : " 
            + m_LifeParts.getShieldLife(1) + " " 
            + m_LifeParts.getShieldLife(2) + " " 
            + m_LifeParts.getShieldLife(3) + " " 
            + m_LifeParts.getShieldLife(4) + '\n';
        result += "Projectors : " 
            + m_LifeParts.getProjectorLife(1) + " " 
            + m_LifeParts.getProjectorLife(2) + " " 
            + m_LifeParts.getProjectorLife(3) + " " 
            + m_LifeParts.getProjectorLife(4) + '\n';
        result += "CoolingUnits : "
           + m_LifeParts.getCoolingUnitLife(1) + " "
           + m_LifeParts.getCoolingUnitLife(2) + " "
           + m_LifeParts.getCoolingUnitLife(3) + " "
           + m_LifeParts.getCoolingUnitLife(4) + " "
           + m_LifeParts.getCoolingUnitLife(5) + " "
           + m_LifeParts.getCoolingUnitLife(6) + '\n';
        result += "Turrets : "
           + m_LifeParts.getTurretlife(1) + " "
           + m_LifeParts.getTurretlife(2) + " "
           + m_LifeParts.getTurretlife(3) + '\n';
        result += "Consoles : "
           + m_LifeParts.getConsoleLife(0) + " "
           + m_LifeParts.getConsoleLife(1) + " "
           + m_LifeParts.getConsoleLife(2) + " "
           + m_LifeParts.getConsoleLife(3) + " "
           + m_LifeParts.getConsoleLife(4) + " "
           + m_LifeParts.getConsoleLife(5) + '\n';
        result += "Reactors : "
           + m_LifeParts.getReactorLife(0) + " "
           + m_LifeParts.getReactorLife(1) + " "
           + m_LifeParts.getReactorLife(2) + " "
           + m_LifeParts.getReactorLife(2) + '\n';
        result += "Engine : "
           + m_LifeParts.getEngineLife(1) + " "
           + m_LifeParts.getEngineLife(2) + " "
           + m_LifeParts.getEngineLife(3) + " "
           + m_LifeParts.getEngineLife(4) + '\n';
        result += "Hull : "
            + m_LifeParts.getHullLife() + '\n';

        return result;
    }
}
