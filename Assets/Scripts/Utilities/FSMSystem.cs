// Uncomment this to get debugging output
#define LOG_TRANSITIONS
#define LOG_SYNCHRONIZE

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Originally from: http://unifycommunity.com/wiki/index.php?title=Finite_State_Machine

Modified by Tyson Joehler to be templateted using Generics.

For more info: http://headbonecreative.com/w/index.php/FSMSystem

A Finite State Machine System based on Chapter 3.1 of Game Programming Gems 1 by Eric Dybsand

Written by Roberto Cezar Bianchini, July 2010
Updated by Tyson Joehler, February 2012


How to use:
    1. Create enums for transitions and state IDs in the monobehavior of the script you wish to control by state machine.
    	Be sure to leave the first entry blank, or NoTransition/NoState (use the following for an example)
    	Place the labels for the transitions and the states of the Finite State System
        in the corresponding enums.
       
/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
    NullTransition = 0, // Use this transition to represent a non-existing transition in your system
}
/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum StateID
{
    NullStateID = 0, // Use this ID to represent a non-existing State in your system   
}

    2. Write new class(es) inheriting from FSMState and fill each one with pairs (transition-state).
        These pairs represent the state S2 the FSMSystem should be if while being on state S1, a
        transition T is fired and state S1 has a transition from it to S2. Remember this is a Deterministic FSM.
        You can't have one transition leading to two different states.

       Method Reason is used to determine which transition should be fired.
       You can write the code to fire transitions in another place, and leave this method empty if you
       feel it's more appropriate to your project.

       Method Act has the code to perform the actions the NPC is supposed do if it's on this state.
       You can write the code for the actions in another place, and leave this method empty if you
       feel it's more appropriate to your project.
       
    3. Create an instance of FSMSystem class and add the states to it.
     
    4. Call Reason and Act (or whichever methods you have for firing transitions and making the NPCs
         behave in your game) from your Update or FixedUpdate methods.
         
    Asynchronous transitions from Unity Engine, like OnTriggerEnter, SendMessage, can also be used,
    just call the Method PerformTransition from your FSMSystem instance with the correct Transition
    when the event occurs.


 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

/// <summary>
/// FSMSystem class represents the Finite State Machine class.
///  It has a List with the States the NPC has and methods to add,
///  delete a state, and to change the current state the Machine is on.
/// </summary>
public class FSMSystem<T, S> where S : IComparable
{
	/// <summary>
	/// The states.
	/// </summary>
    private List<FSMState<T, S>> states;

    /// <summary>
    /// The only way one can change the state of the FSM is by performing a transition
    /// Don't change the CurrentState directly
    /// </summary>
    private S currentStateID;
    public S CurrentStateID { get { return currentStateID; } }
    private FSMState<T, S> currentState;
    public FSMState<T, S> CurrentState { get { return currentState; } }
	
	/// <summary>
	/// The player.
	/// </summary>
	protected GameObject player;
	
	/// <summary>
	/// The npc.
	/// </summary>
	protected GameObject npc;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="FSMSystem`2"/> class.
	/// </summary>
    public FSMSystem()
    {
        states = new List<FSMState<T, S>>();
    }

    /// <summary>
    /// This method places new states inside the FSM,
    /// or prints an ERROR message if the state was already inside the List.
    /// First state added is also the initial state.
    /// </summary>
    public void AddState(FSMState<T, S> s)
    {
        // Check for Null reference before deleting
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        // First State inserted is also the Initial state,
        //   the state the machine is in when the simulation begins
        if (states.Count == 0)
        {
            currentState = s;
            currentStateID = s.ID;
        } 
		else 
		{
			// Add the state to the List only if it's not already in it
	        foreach (FSMState<T, S> state in states)
	        {
	            if (state.CompareTo(s) == 0)
	            {
	                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
	                               " because state has already been added");
	                return;
	            }
	        }
		}
		// Finally, if we made it here it's OK to add it to the list.
        states.Add(s);
		// Set the state machine instance in this state.
		s.stateMachine = this;
    }

    /// <summary>
    /// This method delete a state from the FSM List if it exists,
    ///   or prints an ERROR message if the state was not on the List.
    /// </summary>
    public void DeleteState(S id)
    {
        // Check for NullState before deleting
        if (id == null)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        // Search the List and delete the state if it's inside it
        foreach (FSMState<T, S> state in states)
        {
            if (state.ID.CompareTo(id) == 0)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn't have a target state for the transition passed,
    /// an ERROR message is printed.
    /// </summary>
    public void PerformTransition(T trans)
    {
#if LOG_TRANSITIONS
		string transitionString = string.Format ("{0}: {2} => ({1}) => ", 
			npc == null ? "FSMSystem" : npc.name, trans.ToString(), currentStateID.ToString());
#endif
        // Check for NullTransition before changing the current state
        if (trans == null)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        // Check if the currentState has the transition passed as argument
        S id = currentState.GetOutputState(trans);
        if (id == null || (id.CompareTo(default(S)) == 0))
        {
            Debug.LogError("FSM ERROR: State " + currentStateID.ToString() +  " does not have a target state " +
                           " for transition " + trans.ToString());
            return;
        }
		
#if LOG_TRANSITIONS
		transitionString += string.Format ("{0}", id.ToString ());
		Debug.Log (transitionString);
#endif
		
#if UNITY_EDITOR || LOG_TRANSITIONS
		// Do some extra error checking in the editor
		bool found = false;
#endif
        // Update the currentStateID and currentState      
        currentStateID = id;
        foreach (FSMState<T, S> state in states)
        {
            if (state.ID.CompareTo (currentStateID) == 0)
            {
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving (player, npc);
				
				// change the state
                currentState = state;

                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering (player, npc);
#if UNITY_EDITOR || LOG_TRANSITIONS
				found = true;
#endif
                break;
            }
        }
#if UNITY_EDITOR || LOG_TRANSITIONS
		// Error if not found
		if (!found) {
			Debug.LogError ("Cannot find state for transition " + trans.ToString());
		}
#endif
   
    } // PerformTransition()
	
	/// <summary>
	/// Synchronizes the state.
	/// This sets the state directly. 
	/// </summary>
	/// <param name='id'>
	/// Identifier.
	/// </param>
	public void Synchronize (S id)
	{
#if LOG_SYNCHRONIZE
		Debug.Log (string.Format ("{0}: Synchronize: {1}", npc == null ? "FSMSystem" : npc.name, id.ToString ()));
#endif
#if UNITY_EDITOR || LOG_SYNCHRONIZE
		// Do some extra error checking in the editor
		bool found = false;
#endif
        // Update the currentStateID and currentState      
        currentStateID = id;
        foreach (FSMState<T, S> state in states)
        {
            if (state.ID.CompareTo (currentStateID) == 0)
            {
				// Do the post processing of the state before setting the new one
            	currentState.DoBeforeLeaving (player, npc);
				
				// change the state
                currentState = state;

                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering (player, npc);
#if UNITY_EDITOR || LOG_SYNCHRONIZE
				found = true;
#endif
                break;
            }
        }
#if UNITY_EDITOR || LOG_SYNCHRONIZE
		// Error if not found
		if (!found) {
			Debug.LogError ("Cannot find state to synchronize: " + id.ToString());
		}
#endif
	}
	
	/// <summary>
	/// Processes the current state; calls Reason and then Act
	/// </summary>
	public void ProcessCurrentState () {
		CurrentState.Reason (player, npc);
		CurrentState.Act (player, npc);
	}
	
	/// <summary>
	/// Sets the player.
	/// </summary>
	/// <param name='_player'>
	/// _player.
	/// </param>
	public void SetPlayer (GameObject _player) {
		player = _player;
	}
	
	/// <summary>
	/// Sets the NP.
	/// </summary>
	/// <param name='_npc'>
	/// _npc.
	/// </param>
	public void SetNPC (GameObject _npc) {
		npc = _npc;
	}

} //class FSMSystem
