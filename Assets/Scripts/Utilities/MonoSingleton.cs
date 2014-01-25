/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
/// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
/// AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
/// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///

using UnityEngine;

/// <summary>
/// Mono singleton.
/// This defines a class that makes it easy to implement a MonoBehavior Singleton
/// Originally from:
/// http://wiki.unity3d.com/index.php?title=Singleton#Generic_Based_Singleton_for_MonoBehaviours
/// For more info: http://headbonecreative.com/w/index.php/MonoSingleton
/// </summary>
[AddComponentMenu("Script Toolbox/Utility/Mono Singleton")]
public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T> 
{
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
			// reset the shutdown flag in the editor.
			if (Application.isEditor) {
				_shutdown = false;
			}
			// Check if the instance was initialized yet.
			if (_instance == null && !_shutdown) {
				
				// Not initialized; try and find it in the scene.
				_instance = GameObject.FindObjectOfType (typeof(T)) as T;
				
				// If not found, create a temporary game object to attach one to.
				if (_instance == null) {
					_instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
					
					// Ensure the copy was created OK
					if (_instance == null) {
						Debug.LogError ("Could not create singleton for " + typeof(T).ToString());
					}
				}
				// Allow implementing classes to initialize themselves.
				_instance.Init ();
			}
			// Return the handle to the static instance
			return _instance;
		}
		// No setter; this property is read-only 
	}
	
	/// <summary>
	/// Whether or not this object should be destroyed on load.
	/// </summary>
	public bool dontDestroyOnLoad = true;
	
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
	/// Also, this is where the object is told to not destory itself on load, so child classes
	/// should call base.Init() to make sure this functionality is run.
	/// </summary>
	protected virtual void Init() {
		if (dontDestroyOnLoad) {
			// Keep this object until the game closes.
			DontDestroyOnLoad (gameObject);
		}
	}
	
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
			// Allow implementing classes to initialize themselves.
			_instance.Init ();
		}
	}
		
}
