using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticController : MonoBehaviour
{
    public void TriggerHapticFeedback()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                // Получаем текущую активность в Unity
                using (AndroidJavaObject unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                                                       .GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // Получаем декор окна
                    using (AndroidJavaObject window = unityActivity.Call<AndroidJavaObject>("getWindow"))
                    {
                        using (AndroidJavaObject decorView = window.Call<AndroidJavaObject>("getDecorView"))
                        {
                            // Вызов performHapticFeedback с параметром 1 (VIRTUAL_KEY)
                            decorView.Call("performHapticFeedback", 1);  // 1 - это VIRTUAL_KEY
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Haptic Feedback failed: " + e.Message);
            }
        }
        else
        {
            Debug.Log("Haptic feedback not supported on this platform");
        }
    }
}
