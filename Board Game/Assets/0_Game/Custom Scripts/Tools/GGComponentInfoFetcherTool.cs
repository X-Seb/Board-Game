using UnityEngine;

public class GGComponentInfoFetcherTool : MonoBehaviour
{
    public string GetStringInfoFromComponent(GameObject gameObject, string componentName, string methodName)
    {
        Component component = gameObject.GetComponent(componentName);
        if (component == null)
        {
            Debug.LogError($"Component not found: {componentName}");
            return "";
        }

        System.Type type = component.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        if (method == null)
        {
            Debug.LogError($"Method not found: {methodName} on component {componentName}");
            return "";
        }

        // Invoke the method on the component and return the result
        return (string)method.Invoke(component, null);
    }

    public float GetFloatInfoFromComponent(GameObject gameObject, string componentName, string methodName)
    {
        Component component = gameObject.GetComponent(componentName);
        if (component == null)
        {
            Debug.LogError($"Component not found: {componentName}");
            return 0.0f;
        }

        System.Type type = component.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        if (method == null)
        {
            Debug.LogError($"Method not found: {methodName} on component {componentName}");
            return 0.0f;
        }

        // Invoke the method on the component and return the result
        return (float)method.Invoke(component, null);
    }

    public int GetIntInfoFromComponent(GameObject gameObject, string componentName, string methodName)
    {
        Component component = gameObject.GetComponent(componentName);
        if (component == null)
        {
            Debug.LogError($"Component not found: {componentName}");
            return 0;
        }

        System.Type type = component.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        if (method == null)
        {
            Debug.LogError($"Method not found: {methodName} on component {componentName}");
            return 0;
        }

        // Invoke the method on the component and return the result
        return (int)method.Invoke(component, null);
    }

    public bool GetBoolInfoFromComponent(GameObject gameObject, string componentName, string methodName)
    {
        Component component = gameObject.GetComponent(componentName);
        if (component == null)
        {
            Debug.LogError($"Component not found: {componentName}");
            return false;
        }

        System.Type type = component.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);

        if (method == null)
        {
            Debug.LogError($"Method not found: {methodName} on component {componentName}");
            return false;
        }

        // Invoke the method on the component and return the result
        return (bool)method.Invoke(component, null);
    }
}