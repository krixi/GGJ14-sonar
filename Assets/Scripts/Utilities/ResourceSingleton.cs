/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
/// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
/// AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
/// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///

using UnityEngine;
using System.Collections;

/// <summary>
/// Mono Resource singleton.
/// This defines a class that makes it easy to implement a MonoBehavior Singleton
/// Originally from:
/// http://wiki.unity3d.com/index.php?title=Singleton#Generic_Based_Singleton_for_MonoBehaviours
/// and modified to load a configurable object from a Resources directory.
/// For more info: http://headbonecreative.com/w/index.php/ResourceSingleton
/// </summary>
[AddComponentMenu("Script Toolbox/Utility/Resource Singleton")]
public abstract class ResourceSingleton<T> : MonoBehaviour where T : ResourceSingleton<T> {

	/// <summary>
	/// The instance of the class T
	/// </summary>
	private static T _instance = null;
	
	/// <summary>
	/// The _shutdown flag indicates whether or not the application is closing.
	/// </summary>
	private static bool _shutdown = false;
	
	/// <summary>
	/// Gets the instance of the class T.
	/// This implements the singleton pattern to ensure only one of these classes is in existance at a time
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public static T instance {
		get {
			// Take a different action if this function is called in edit mode.
			if (Application.isPlaying == false) {
				T editorInst = GameObject.FindObjectOfType (typeof(T)) as T;
				if (editorInst != null) {
					return editorInst;
				} else {
					// load the resource directly in edit mode.
					return Resources.Load (typeof(T).ToString(), typeof(T)) as T;
				}
			}
			
			// Check if the instance was initialized yet.
			if (_instance == null && !_shutdown) {
				
				// Not initialized; try and find it in the scene.
				_instance = GameObject.FindObjectOfType (typeof(T)) as T;
				
				// If not found, attempt to load one from the resources directory.
				if (_instance == null) {
					// since we are playing, make a copy
					_instance = Instantiate (Resources.Load (typeof(T).ToString(), typeof(T)))  as T;
					
					// if still not found, throw an error.
					if (_instance == null) {
						throw new System.InvalidOperationException ("Cannot create instance of type: " + typeof(T).ToString());
					}
				}
				
				// Keep this object until the game closes.
				DontDestroyOnLoad (_instance.gameObject);
				// Allow implementing classes to initialize themselves.
				_instance.Init ();	
			}
			// Return the handle to the static instance
			return _instance;
		}
		// No setter; this property is read-only 
	}
	
	/// <summary>
	/// Clean up after the class when the game stops.
	/// </summary>
	public virtual void OnApplicationQuit () {
		_shutdown = true;
		if (_instance != null) {
			// Allow the instance to clean itself up
			_instance.OnAppQuit ();
			// delete it
			_instance = null;
		}
	}
	
	/// <summary>
	/// Declare a virtual function that child classes can implement to do initializations.
	/// </summary>
	public virtual void Init() { }
	
	/// <summary>
	/// Declare a virtual function that child classes can implement to prepare for application closing.
	/// </summary>
	public virtual void OnAppQuit () { }
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	public virtual void Awake () {
		if (_instance == null) {
			_instance = this as T;
			// Keep this object until the game closes.
			DontDestroyOnLoad (_instance.gameObject);
			// Allow implementing classes to initialize themselves.
			_instance.Init ();	
		}
	}
		
}
