using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Mono FSM.
/// This implements the basic FSM
/// </summary>
public abstract class MonoFSM<T, S> : MonoBehaviour where S : IComparable {
	
	/// <summary>
	/// Whether or not this object is destroyed when a level is loaded.
	/// </summary>
	public bool dontDestroyOnLoad = true;
	
	/// <summary>
	/// The state machine.
	/// </summary>
	protected FSMSystem<T,S> stateMachine;
	
	/// <summary>
	/// Gets the current state ID.
	/// </summary>
	/// <value>
	/// The current state ID.
	/// </value>
	public S CurrentStateID {
		get { return stateMachine.CurrentStateID; }
	}
	
	/// <summary>
	/// Sets up states.
	/// </summary>
	protected abstract void SetUpStates ();

	// Use this for initialization
	protected virtual void Start () {
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (gameObject);
		}
		
		MakeFSM ();
	}
	
	/// <summary>
	/// Makes the FSM
	/// </summary>
	protected virtual void MakeFSM () {
		if (stateMachine ==  null) {
			// Initialize the FSM.
			stateMachine = new FSMSystem<T, S>();
			
			// Set the NPC
			stateMachine.SetNPC (gameObject);
			
			// Add the states.
			SetUpStates (); // this is implemented in deriving classes.
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		stateMachine.ProcessCurrentState ();
	}
	
	/// <summary>
	/// Performs the transition.
	/// </summary>
	public virtual void PerformTransition (T trans) {
		MakeFSM ();
		stateMachine.PerformTransition (trans);
	}
}
