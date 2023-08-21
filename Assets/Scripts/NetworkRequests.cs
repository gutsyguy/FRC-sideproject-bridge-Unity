using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;


public class NetworkRequests : MonoBehaviour
{

    private string robotURL = "localhost:8080";
    
    private void Start(){
        robotAction(1); 
    }

    private void robotAction(int input) {

        string command = null;
        string endpoint; 
	     
        //Decides the action based on the number input 
    	switch (input) {
            case 0:
                command = "Forward";
                endpoint = "/api/robot/moveForward";
		        StartCoroutine(SendRequest(command, endpoint)); 
	        	break;
            case 1:
                command = "Backward";
                endpoint = "/api/robot/moveBackwards";
                StartCoroutine(SendRequest(command, endpoint));
                break; 
	        case 2:
                command = "Turn Left";
                endpoint = "/api/robot/turnLeft";
		        StartCoroutine(SendRequest(command, endpoint));    	
		break;
            case 3:
                command = "Turn Right";
                endpoint = "/api/robot/turnRight"; 
	            StartCoroutine(SendRequest(command, endpoint));       	
		break;
            case 4:
                command = "Stop";
                endpoint = "/api/robot/stop";        
		        StartCoroutine(SendRequest(command, endpoint));        
		break;
		    default:
                Debug.Log("Invalid command");
                break;
			      		
    	}
        if (command == null) Debug.Log("command is null"); else Debug.Log(command);
    }

    IEnumerator SendRequest(string json, string endpoint) {
        
        //The "using" block prevents memory leaks Unity web request"	
	    using (UnityWebRequest request = new UnityWebRequest(robotURL + endpoint, "GET")) { 
	
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(request.error);
                Debug.Log("_____________________");
                Debug.LogError(request.method);
            }
            else {
            Debug.Log(request.downloadHandler.text);
	        }
        }
    } 
}
