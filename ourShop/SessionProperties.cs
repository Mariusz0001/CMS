using ourShop;
using System;
using System.Web.SessionState;

public static class SessionProperties
{
    public static int? GetUserId(HttpSessionState session)
    {
        if (session != null && session["UserId"] != null)
        {
            return Utils.TryParseNullableInt(session["UserId"].ToString());
        }
        return null;
    }
    public static string GetSessionString(HttpSessionState session, String sessionVariableName)
    {
        try
        {
            if (session != null && session[sessionVariableName] != null)
                return session[sessionVariableName].ToString();
        }
        catch
        {
        }
        return String.Empty;
    }
}
