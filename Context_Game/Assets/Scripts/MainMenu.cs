using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadScene1()
	{
		SceneManager.LoadScene(1);
	}
	public void LoadScene2()
	{
		SceneManager.LoadScene(2);
	}
	public void LoadScene3()
	{
		SceneManager.LoadScene(3);
	}
	public void LoadScene4()
	{
		SceneManager.LoadScene(4);
	}
	public void StopGame()
	{
		Application.Quit();

	}
	public void ToMenu()
	{
		print("working");
		SceneManager.LoadScene(0);
	}
}
