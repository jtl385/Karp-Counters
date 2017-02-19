using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

	public static GameControllerScript control;
	[HideInInspector] public AudioSource source;
	private int level;

	public GameObject grassEnemy;
	public GameObject waterEnemy;
	public GameObject fireEnemy;
	public GameObject chansey;
	public GameObject tauros;
	public GameObject voltorb;
	public GameObject gameOverScreen;
	public GameObject winLevelScreen;
	public GameObject introMessageScreen;

	public Text gameOverText;
	public Text winLevelText;
	public Text scoreText;
	public Text livesText;
	public Text pauseText;

	public AudioClip chanseyCry;
	public AudioClip taurosCry;
	public AudioClip voltorbCry;
	public AudioClip hurtSound;
	public AudioClip gainLivesSound;
	public AudioClip gainEnergySound;

	[HideInInspector] public float spawnInterval;
	[HideInInspector] public float spawnTime;
	[HideInInspector] public float spawnIntervalTwo;
	[HideInInspector] public float spawnTimeTwo;
	[HideInInspector] public float timeLimit;
	private bool otherVarsSet;

	[HideInInspector] public bool isGameOver;
	[HideInInspector] public bool isPaused;

	[HideInInspector] public int lives;
	[HideInInspector] public int score;
	[HideInInspector] public int musicVol;
	[HideInInspector] public int soundVol;

	[HideInInspector] public List<Transform> allEnemies;
	private System.Action[] actions;

	void Awake(){
		if (control == null) {
			control = this;
		} else if (control != this)
			Destroy (gameObject);
	}

	void Start () {
		source = GetComponent<AudioSource> ();
		level = LevelHolder.control.level;
		spawnInterval = LevelHolder.control.spawnIntervals [level];
		spawnTime = Time.time + 1f;
		timeLimit = Time.time + LevelHolder.control.timeLimits [level];

		isGameOver = false;
		lives = 10;
		score = 0;
		musicVol = 4;
		soundVol = 10;
		isPaused = false;
		PauseGame ();

		scoreText.text = "Score: " + score.ToString ();
		livesText.text = "Lives: " + lives.ToString ();

		actions = new System.Action[] { () => WinLevel(), () => LevelOne (), () => LevelTwo (), () => LevelThree() };
		otherVarsSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		actions [level] ();

	}


	//ENEMY SPAWNERS=================================================================================


	void SpawnEnemy(GameObject enemy){
		int xVal = (Random.Range (0, 2) * 20) - 10; //will be either -10 or 10
		Vector3 spawnLoc = new Vector3 (xVal, Random.Range (-3f, 3f), 0);

		GameObject cloneEnemy = Instantiate (enemy, spawnLoc, Quaternion.identity) as GameObject;

		cloneEnemy.GetComponent<EnemyScript> ().dir = -cloneEnemy.transform.position.normalized;
		if (xVal < 0) {
			cloneEnemy.GetComponentInChildren<SpriteRenderer> ().flipX = true; //flip the sprite so it's facing the right way
		}
		allEnemies.Add (cloneEnemy.transform);
	}
	void SpawnEnemySide(GameObject enemy){
		int xVal = (Random.Range (0, 2) * 18) - 9; //will be either -9 or 9
		int yVal = (Random.Range (0, 2) * 6) - 3; //will be either -3 or 3
		Vector3 spawnLoc = new Vector3(xVal, yVal, 0);

		GameObject cloneEnemy = Instantiate (enemy, spawnLoc, Quaternion.identity) as GameObject;

		if (xVal < 0) {
			cloneEnemy.GetComponentInChildren<SpriteRenderer> ().flipX = true; //flip the sprite so it's facing the right way
			cloneEnemy.GetComponent<EnemyScript> ().dir = Vector2.right;
		} else
			cloneEnemy.GetComponent<EnemyScript> ().dir = -Vector2.right;
	}
	void SpawnTauros(){
		SpawnEnemy (tauros);
		source.PlayOneShot (taurosCry, .8f);
	}
	void SpawnChansey(){
		SpawnEnemySide (chansey);
		source.PlayOneShot (chanseyCry, 1f);
	}
	void SpawnVoltorb(){
		SpawnEnemySide (voltorb);
		source.PlayOneShot (voltorbCry, 1f);

	}


	//LOGISTICS========================================================================================


	public void UpdateLives(int n){ //n is lives lost
		lives -= n;
		if (lives < 0)
			lives = 0;
		livesText.text = "Lives: " + lives.ToString ();

		if (n > 0)
			source.PlayOneShot (hurtSound, 1f);
		else
			source.PlayOneShot (gainLivesSound, 1f);

		if (lives == 0 && !isGameOver)
			GameOver ();
	}
	public void UpdateScore(int n){
		score += n;
		scoreText.text = "Score: " + score.ToString();
	}
	public void PauseGame(){
		if (!isPaused) {
			Time.timeScale = 0;
			isPaused = true;
			pauseText.text = "RESUME";
		} else{
			Time.timeScale = 1;
			isPaused = false;
			pauseText.text = "PAUSE";
		}
	}
	public void CloseIntroMessage(){
		introMessageScreen.SetActive (false);
	}
	public void GameOver(){
		if (!isGameOver) {
			isGameOver = true;
			gameOverScreen.SetActive (true);
			gameOverText.text = "GAME OVER!\nFinal Score: " + score.ToString ();
		}
	}
	public void WinLevel(){
		if (!isGameOver) {
			isGameOver = true;
			winLevelScreen.SetActive (true);
			winLevelText.text = "LEVEL COMPLETE!\nFinal Score: " + score.ToString ();
		}
	}
	public void ToMenu(){
		SceneManager.LoadScene ("Menu");
	}
	public bool CleanedAllEnemies(){
		allEnemies.RemoveAll (t => t == null);
		if (allEnemies.Count == 0)
			return true;
		else
			return false;
	}


	//LEVEL CODE=====================================================

	void LevelOne(){
		if (!isGameOver && Time.time < timeLimit) {
			if (Time.time > spawnTime) {
				int chooser = Random.Range (0, 3);
				if (chooser == 0)
					SpawnEnemy (grassEnemy);
				else if (chooser == 1)
					SpawnEnemy (waterEnemy);
				else if (chooser == 2)
					SpawnEnemy (fireEnemy);

				spawnTime += spawnInterval;
			}
		}
		if (Time.time > timeLimit && CleanedAllEnemies ()) {
			WinLevel ();
		}
	}
	void LevelTwo(){
		LevelOne (); //levels are the same except spawnInterval, which will get changed when level starts
	}
	void LevelThree(){
		if (!otherVarsSet) {
			spawnIntervalTwo = 15f;
			spawnTimeTwo = Time.time + 10f;
			otherVarsSet = true;
		}

		if (!isGameOver && Time.time < timeLimit) {
			if (Time.time > spawnTime) {
				int chooser = Random.Range (0, 3);
				if (chooser == 0)
					SpawnEnemy (grassEnemy);
				else if (chooser == 1)
					SpawnEnemy (waterEnemy);
				else if (chooser == 2)
					SpawnEnemy (fireEnemy);

				spawnTime += spawnInterval;
			}
			if (Time.time > spawnTimeTwo) {
				SpawnTauros();
				spawnTimeTwo += spawnIntervalTwo;
			}
		}
		if (Time.time > timeLimit && CleanedAllEnemies ()) {
			WinLevel ();
		}
	}
}
