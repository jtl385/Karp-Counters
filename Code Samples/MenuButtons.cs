using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public GameObject pageOne;
	public GameObject pageTwo;
	public GameObject menuStuff;
	public GameObject player;
	public GameObject energyBar;
	public GameObject buttons;
	public GameObject levelSelect;

	public void StartGame(int level){
		LevelHolder.control.level = level;
		if (level == 5)
			LevelHolder.control.boss = true;
		SceneManager.LoadScene ("Scene1");
	}
	public void toPageOne(){
		player.SetActive (true);
		energyBar.SetActive (true);
		buttons.SetActive (true);
		pageOne.SetActive (true);
		pageTwo.SetActive (false);
		menuStuff.SetActive (false);
	}
	public void toPageTwo(){
		pageOne.SetActive (false);
		pageTwo.SetActive (true);
	}
	public void toMainMenu(){
		player.SetActive (false);
		energyBar.SetActive (false);
		buttons.SetActive (false);
		pageOne.SetActive (false);
		pageTwo.SetActive (false);
		menuStuff.SetActive (true);
		levelSelect.SetActive (false);
	}
	public void toLevelSelect(){
		menuStuff.SetActive (false);
		levelSelect.SetActive (true);
	}

}
