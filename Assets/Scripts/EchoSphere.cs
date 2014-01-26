using UnityEngine;
using System.Collections;

public class EchoSphere : MonoBehaviour {

	public Material echoMaterial = null;
	
	// Echo sphere Properties
	public float sphereMaxRadius = 10.0f;		//Final size of the echo sphere.
	private float sphereCurrentRadius = 0.0f;	//Current size of the echo sphere
	
	public float fadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float fadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 1.0f;			//Speed of the sphere growth.
	public bool is_manual = false;			//Is pulse manual.  if true, pulse triggered by left-mouse click
	
	private bool is_animated = false;		//If true, pulse is currently running.
	public float fade = 0.0f;
	public float pulse_frequency = 5.0f;
	private float deltaTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		if(!is_manual)is_animated = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(echoMaterial == null)return;
		
		// If manual selection is disabled, automatically trigger a pulse at the given freq.
		if(!is_manual){
			deltaTime += Time.deltaTime;
			if(deltaTime >= pulse_frequency && !is_animated){
				TriggerPulse();
			}
		} else {
			deltaTime += Time.deltaTime;
		}
		
		UpdateRayCast();
		UpdateEcho();
		UpdateShader();
	}
	
	// Called to trigger an echo pulse
	void TriggerPulse(){
		deltaTime = 0.0f;
		sphereCurrentRadius = 0.0f;
		fade = 0.0f;
		is_animated = true;
	}
	
	// Called to halt an echo pulse.
	void HaltPulse(){
		is_animated = false;	
	}
	
	void ClearPulse(){
		fade = 0.0f;
		sphereCurrentRadius = 0.0f;
		is_animated = false;
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() {
		if(!is_manual)return;
		if (Input.GetButtonDown("Fire1")){
			Debug.Log("Mouse Click");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		  	RaycastHit hit;
	        	if (Physics.Raycast(ray,out hit, 10000)) {
	            		Debug.Log("Hit Something");
				TriggerPulse();
				transform.position = hit.point;
			}
		}
	        
	
	}
	
	// Called to update the echo front edge
	void UpdateEcho(){
		if(!is_animated)return;
		
		if(sphereCurrentRadius >= sphereMaxRadius){
			HaltPulse();
		} else {
			sphereCurrentRadius += Time.deltaTime * echoSpeed;  
		}
	}
	
	// Called to update the actual shader values (some of which only chance once but are included here
	// for illustrative purposes)
	void UpdateShader(){
		float radius = sphereCurrentRadius;
		float max_radius = sphereMaxRadius;
		float max_fade = sphereMaxRadius / echoSpeed;
		
		if(deltaTime > fadeDelay)
			fade += Time.deltaTime * fadeRate;
		
		// Update our shader properties (requires Echo.shader)
		echoMaterial.SetVector("_Position",transform.position);
		echoMaterial.SetFloat("_Radius",radius);
		echoMaterial.SetFloat("_MaxRadius",max_radius);
		echoMaterial.SetFloat("_Fade",fade);
		echoMaterial.SetFloat("_MaxFade",max_fade);
	}
}
