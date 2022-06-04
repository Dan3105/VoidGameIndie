using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickController : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;

    public Vector2 joystickDir;
    private Vector2 joystickTouchPos;
    private float radiusJoystick;

    public bool isDash;
    public float dashTime;
    public bool callDash;
    // Start is called before the first frame update
    void Start()
    {
        radiusJoystick = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 5;
        joystick.SetActive(false);
        joystickBG.SetActive(false);
        //dash Detection
        isDash = false;
        callDash = false;
        dashTime = 0;
    }

    public void Update()
    {
        if(callDash)
            dashTime += Time.deltaTime; 
    }
    public void OnPointerDown()
    {
        joystick.SetActive(true);
        joystickBG.SetActive(true);

        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;

        //detect dash calling
        callDash = true;
        isDash = false;
        dashTime = 0;
    }

    public void OnPointerDrag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        
        //calculate direction
        joystickDir = (pointerEventData.position - joystickTouchPos).normalized;
        
        float dragDistance = Vector2.Distance(pointerEventData.position, joystickTouchPos);
        if (dragDistance < radiusJoystick)
            joystick.transform.position = joystickTouchPos + joystickDir * dragDistance;
        else
            joystick.transform.position = joystickTouchPos + joystickDir * radiusJoystick;
    }

    public void OnPointerUp()
    {
        if(dashTime < 0.25f)
        {
            isDash = true;
            dashTime = 0.25f;
        }
            
        else
            joystickDir = Vector2.zero;

        callDash = false;

        joystick.SetActive(false);
        joystickBG.SetActive(false);
    }
}
