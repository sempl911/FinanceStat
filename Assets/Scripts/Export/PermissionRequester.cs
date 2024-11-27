using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionRequester : MonoBehaviour
{
    void Start()
    {
        RequestStoragePermissions();
    }

    void RequestStoragePermissions()
    {
        // Для Android 6.0 и выше требуется проверка и запрос разрешений
        if (Application.platform == RuntimePlatform.Android)
        {
            // Получаем текущую активность Android
            AndroidJavaObject activity = GetUnityActivity();
            AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
            string packageName = activity.Call<string>("getPackageName");

            // Проверяем, есть ли уже разрешения
            int permissionGranted = packageManager.Call<int>("checkPermission", "android.permission.WRITE_EXTERNAL_STORAGE", packageName);

            if (permissionGranted != 0) // PERMISSION_DENIED
            {
                // Запрос разрешения через Android system
                string[] permissions = new string[] { "android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.READ_EXTERNAL_STORAGE" };
                activity.Call("requestPermissions", permissions, 0);
            }
        }
    }

    AndroidJavaObject GetUnityActivity()
    {
        // Получаем текущую активность Unity
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }
}
