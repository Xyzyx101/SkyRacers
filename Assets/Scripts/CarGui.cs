using UnityEngine;
using System.Collections;

public class CarGui : MonoBehaviour {

    private float m_speed;

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

	// Use this for initialization
	void Start () {
        m_speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 200, 50), "Speed KPH:" + m_speed * 3.6f);
        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a title");
    }
}
