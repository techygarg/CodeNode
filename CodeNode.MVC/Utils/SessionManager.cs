using System;
using System.Web;

namespace CodeNode.MVC.Utils
{
    public static class SessionManager
    {
        public static T GetObject<T>() where T : class
        {
            if (HttpContext.Current.Session == null)
                throw new NullReferenceException("Session is null.");

            return (T)HttpContext.Current.Session[typeof(T).GUID.ToString()];
        }

        public static void SetObject(object obj)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[obj.GetType().GUID.ToString()] = obj;
            }
        }
    }
}