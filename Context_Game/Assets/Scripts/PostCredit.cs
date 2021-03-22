using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostCredit : MonoBehaviour
{
	public float animatieTime = 15;
	private void Start()
	{
		Debug.Log("start");
		StartCoroutine(WaitForAnimation());
		Debug.Log("end");
	}
	IEnumerator WaitForAnimation()
	{
		Debug.Log("1");
		yield return new WaitForSeconds(animatieTime);
		SceneManager.LoadScene(1);
		Debug.Log("2");
		
	}
}
