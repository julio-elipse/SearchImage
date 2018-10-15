using System;

namespace SearchImage
{
  class Program
  {
    static void Main(string[] args)
    {
      //This program must receive two arguments,
      //a path to a folder to search for documentation projects
      //and a full path to an image file
      if (args.Length == Constants.IMG_ARG_VALID_COUNT)
      {
        ImageSearch m_isSearch = new ImageSearch(args[Constants.IMG_ARG_PATH_NAME_INDEX], args[Constants.IMG_ARG_IMAGE_NAME_INDEX]);
        m_isSearch.LoadProjects();
        m_isSearch.SearchForImageOnTopics();
      }
      else
      {
        GlobalResult.LogErrorAndQuit(Constants.IMG_PROG_MSG_INVALID_ARG);
      }
    }
  }
}