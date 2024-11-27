using UnityEngine;

public class ShareXlsx 
{
    public void ShareFile(string filePath)
    {
        
        // Проверяем, что файл существует
        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogError("Файл не найден: " + filePath);
            return;
        }

        // Создаем intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        // Получаем контекст
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        // Устанавливаем тип файла и путь к файлу
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", filePath);
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        intentObject.Call<AndroidJavaObject>("setType", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        // Запускаем Activity для отправки файла
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Excel File");
        currentActivity.Call("startActivity", chooser);
    }
}
