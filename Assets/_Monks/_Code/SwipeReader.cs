using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ShapeController))]//cannot attatch this script without shape controller class
public class SwipeReader : MonoBehaviour
{
    #region Variables
    //for swiping
    [SerializeField] Vector2 startTouchPos, endTouchPos;//ToDO:public for testing
    [SerializeField][Range(50, 300)] float upSwipeThreshold;
    [SerializeField][Range(-50, -300)] float downSwipeThreshold;
    [SerializeField][Range(50, 300)] float rightSwipeThreshold;
    [SerializeField][Range(-50, -300)] float leftSwipeThreshold;
    //events
    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;
    #endregion
    #region Singleton Pattern
    public static SwipeReader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    #endregion

    
    private void Update()
    {
        Touch_Process();
    }

    private void Touch_Process()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("start touch");
                startTouchPos = touch.position;

            }
            touch = Swipe_Process(touch);
            Release_Finger(touch);
        }
    }

    private Touch Swipe_Process(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            if (touch.deltaPosition.x < leftSwipeThreshold)//[Solved] ToDo:fix magic number
            {
                Debug.Log("Moving Left");
                OnSwipeLeft?.Invoke();

            }
            if (touch.deltaPosition.x > rightSwipeThreshold)
            {
                Debug.Log("Moving right");
                OnSwipeRight.Invoke();
            }
            if (touch.deltaPosition.y <= downSwipeThreshold)
            {
                Debug.Log("Moving Down");

            }
            if (touch.deltaPosition.y > upSwipeThreshold)
            {
                Debug.Log("Moving Up");

            }
        }

        return touch;
    }

    private void Release_Finger(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended)
        {
            endTouchPos = touch.position;
        }
    }
}
