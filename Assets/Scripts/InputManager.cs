using UnityEngine;

public static class InputManager
{
    public static bool IsTapStarted()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            return Input.GetMouseButtonDown(0);
        #elif UNITY_ANDROID || UNITY_IOS
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        #endif

        return false;
    }
}