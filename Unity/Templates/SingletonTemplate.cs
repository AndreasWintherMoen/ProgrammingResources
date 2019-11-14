public class CLASSNAME : MonoBehaviour
{
    private static CLASSNAME s_Instance = null;
    public static CLASSNAME Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(CLASSNAME)) as CLASSNAME;
            }

            if (s_Instance == null)
            {
                GameObject obj = new GameObject("CLASSNAME");
                s_Instance = obj.AddComponent(typeof(CLASSNAME)) as CLASSNAME;
                Debug.Log("Could not locate a CLASSNAME object. CLASSNAME was generated automatically.");
            }
            return s_Instance;
        }
    }
}
