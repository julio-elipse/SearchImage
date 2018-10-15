using System;
namespace SearchImage
{
  static class GlobalResult
  {
    /// <summary>
    /// Global variable to store the number of references to an image file name found on documentation projects
    /// </summary>
    public static int NumberOfReferences;
    /// <summary>
    /// Method to log an error message to system console
    /// and then quits the application
    /// </summary>
    /// <param name="p_strMessage">Message to log to system console</param>
    public static void LogErrorAndQuit(string p_strMessage)
    {
      Console.WriteLine(p_strMessage);
      Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
    }
    /// <summary>
    /// Method to log a general message
    /// to system console
    /// </summary>
    /// <param name="p_strMessage">Message to log to system console</param>
    public static void LogGeneralMessage(string p_strMessage)
    {
      Console.WriteLine(p_strMessage);
    }
    /// <summary>
    /// Method to log a separator line
    /// to system console
    /// </summary>
    public static void LogSeparator()
    {
      string m_strSeparator = new string(Constants.IMG_SEARCH_MSG_SEPARATOR_CHAR, Constants.IMG_SEARCH_MSG_SEPARATOR_SIZE);
      LogGeneralMessage(m_strSeparator);
    }
  }
}