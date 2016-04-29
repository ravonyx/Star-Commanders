using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoosePanel : MonoBehaviour
{
	private List<GameObject> _spaceshipList;
	[SerializeField]
	private Transform _panel;
	[SerializeField]
	private TP tp;

	private Color _unselectedColor;
	private GameObject _selectedObject;
	private bool _init;
	private int _indexSelected;

	public void OnEnable()
	{
		if (_spaceshipList == null)
		{
			_spaceshipList = new List<GameObject>();
			_unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
		}
		_indexSelected = 0;
		InvokeRepeating("UpdatePanel", 0, 2);
	}

	public void OnDisable()
	{
		CancelInvoke();
	}

	public void Update()
	{
		if (_spaceshipList.Count > _indexSelected)
		{
			for(int i = 0; i < _spaceshipList.Count; i++)
				_spaceshipList[i].GetComponent<RawImage>().color = _unselectedColor;
			if (_spaceshipList[_indexSelected] != null)
				_spaceshipList[_indexSelected].GetComponent<RawImage>().color = Color.green;

			if (Input.GetKeyDown(KeyCode.LeftArrow) && _indexSelected > 0)
				_indexSelected--;
			if (Input.GetKeyDown(KeyCode.RightArrow) && _indexSelected + 1 < _spaceshipList.Count)
				_indexSelected++;
			if (Input.GetKeyDown(KeyCode.Return))
				tp.doTP(_indexSelected);
		}
	}

	private void UpdatePanel()
	{
		for (int j = 0; j < _spaceshipList.Count; j++)
		{
			Destroy(_spaceshipList[j]);
		}
		_spaceshipList.Clear();
        string tag = "";
        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
            tag = "SpaceshipBlue";
        else
            tag = "SpaceshipRed";

        GameObject[] spaceship = GameObject.FindGameObjectsWithTag(tag);
		for (int i = 0; i < spaceship.Length; i++)
		{

			GameObject button = (GameObject)Instantiate(Resources.Load("UI/SpaceshipTP"));
			if (button)
			{
				_spaceshipList.Add(button);
				button.SetActive(true);
				button.transform.SetParent(_panel, false);
			}
			else
				Debug.Log("Add SpaceshipTP prefab in folder Resources");

		}
	}
}
