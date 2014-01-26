using UnityEngine;
using System.Collections;

namespace LevelStates 
{
	/// <summary>
	/// Place the labels for the Transitions in this enum.
	/// Don't change the first label, NullTransition as FSMSystem class uses it.
	/// </summary>
	public enum Transition
	{
		NullTransition = 0, // Use this transition to represent a non-existing transition in your system
		PauseGame,
		ResumeGame,
		ObjectiveReached,
		ObjectiveFailed,
	};

	/// <summary>
	/// Place the labels for the States in this enum.
	/// Don't change the first label, NullTransition as FSMSystem class uses it.
	/// </summary>
	public enum StateID
	{
		NullStateID = 0, // Use this ID to represent a non-existing State in your system
		Playing,
		Paused,
		LevelComplete,
		LevelFailed,
	};

	/// <summary>
	/// Playing state.
	/// </summary>
	public class PlayingState : FSMState <Transition, StateID> {
		public PlayingState () : base () {
			// Set the state ID
			stateID = StateID.Playing;
			// Add transitions.
			AddTransition (Transition.PauseGame, StateID.Paused);
		}

		public override void Reason (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
		public override void Act (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
	};

	public class PausedState : FSMState <Transition, StateID> {
		public PausedState () : base () {
			// Set the state ID
			stateID = StateID.Paused;
			// Add transitions.
			AddTransition (Transition.ResumeGame, StateID.Playing);
		}
		
		public override void Reason (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
		public override void Act (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
	};

	public class LevelCompleteState : FSMState <Transition, StateID> {
		public LevelCompleteState () : base () {
			// Set the state ID
			stateID = StateID.LevelComplete;
			// Add transitions.
			//AddTransition (Transition.ResumeGame, StateID.Playing);
		}
		
		public override void Reason (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
		public override void Act (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
	};

	public class LevelFailedState : FSMState <Transition, StateID> {
		public LevelFailedState () : base () {
			// Set the state ID
			stateID = StateID.LevelFailed;
			// Add transitions.
			//AddTransition (Transition.ResumeGame, StateID.Playing);


		}
		
		public override void Reason (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
		public override void Act (GameObject player, GameObject npc)
		{
			// nothing to do; wait for player input
		}
	};
}
