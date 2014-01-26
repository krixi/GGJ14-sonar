using UnityEngine;
using System.Collections;
using LevelStates;

/// <summary>
/// Level FSM
/// This actually contains the update loop that drives the FSM,
/// as well as instantiates the classes that implement the state behaviors. 
/// </summary>
public class LevelFSM : MonoFSM<Transition, StateID> 
{
	protected override void SetUpStates ()
	{
		// First state added is the default state.
		stateMachine.AddState (new LoadingLevelState());
		stateMachine.AddState (new PlayingState ());
		stateMachine.AddState (new PausedState ());
		stateMachine.AddState (new LevelFailedState ());
		stateMachine.AddState (new LevelCompleteState ());
	}
}
