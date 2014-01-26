using UnityEngine;
using System.Collections;
using LevelStates;

public class LevelFSM : MonoFSM<Transition, StateID> 
{
	protected override void SetUpStates ()
	{
		// First state added is the default state.
		stateMachine.AddState (new PlayingState ());
		stateMachine.AddState (new PausedState ());
		stateMachine.AddState (new LevelFailedState ());
		stateMachine.AddState (new LevelCompleteState ());
	}
}
