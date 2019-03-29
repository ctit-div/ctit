<%@ Application Language="C#" %>

<script runat="server">
    protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {
        // Let's write a message to show this got fired---
        //Response.Write("SessionID: " +Session.SessionID.ToString() + "User key: " +(string)Session["user"]); 
        //if(Session["user"]!=null) // e.g. this is after an initial logon
        //{
        //    string sKey=(string)Session["user"];
        //    // Accessing the Cache Item extends the Sliding Expiration automatically
        //    string sUser=(string) HttpContext.Current.Cache[sKey];
        //}
    }
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.





    }

</script>
