using UnityEngine;

public class BuildConstants
{
    public static bool isExpo = false;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public static bool isDebug = true;
#else
    public static bool isDebug = false;
#endif

#if UNITY_WEBGL
    public static bool isWebGL = true;
#else
    public static bool isWebGL = false;
#endif

#if UNITY_ANDROID
    public static bool isMobile = true;
#else
    public static bool isMobile = Application.isMobilePlatform;
#endif
}
