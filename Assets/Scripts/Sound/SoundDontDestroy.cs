using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDontDestroy : MonoBehaviour
{
    private static SoundDontDestroy instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующий объект
        }
    }

    
}
