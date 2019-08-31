using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DoodleStudio95;

namespace DoodleStudio95Examples {
[RequireComponent(typeof(Collider))]
public class TriggerTransition : MonoBehaviour {

	[Tooltip("Point to a UI element that covers the screen, with an animator")]
	public DoodleAnimator m_Animator;
	[Tooltip("Point to the transition animation you want to show")]
	public DoodleAnimationFile m_Transition;
	[Tooltip("Name of the scene to transition to")]
	public string m_NextScene;


	bool _inTransition = false;

	void Start() {
		// Set the collider to be a trigger
		GetComponent<Collider>().isTrigger = true;

		m_Animator.Hide();
	}
	
	void OnTriggerEnter(Collider c) {
		StartTransition();
	}

	IEnumerator Transition() {
		_inTransition = true;

		// Make sure the animator stays 
		DontDestroyOnLoad(m_Animator.transform.root.gameObject);

		// Set the animation
		m_Animator.ChangeAnimation(m_Transition);
		m_Animator.Show();

		// Play the transition and wait
		yield return m_Animator.PlayAndPauseAt();

		// We can do anything while the player isn't looking.
		//  If the next scene is empty, we'll load the same scene. 
		// You can modify this to for example move the player somewhere else in the level
		yield return SceneManager.LoadSceneAsync(string.IsNullOrEmpty(m_NextScene) ? m_NextScene : SceneManager.GetActiveScene().name);

		// Play the transition backwards (from the last frame to the first)
		yield return m_Animator.PlayAndPauseAt(-1, 0); 
		
		// Hide the animator
		m_Animator.Hide();

		// Destroy the animator we kept around (Warning: This'll destroy everything in the same Canvas)
		Destroy(m_Animator.transform.root.gameObject);

		_inTransition = false;
	}

	public void StartTransition() {
		if(_inTransition)
			return;
		m_Animator.StopAllCoroutines();
		m_Animator.StartCoroutine(Transition());
	}
}
}