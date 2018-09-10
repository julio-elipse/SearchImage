using System;

namespace SearchImage
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == Constants.IMG_ARG_VALID_COUNT)
      {
        ImageSearch m_isSearch = new ImageSearch(args[Constants.IMG_ARG_PATH_NAME_INDEX], args[Constants.IMG_ARG_IMAGE_NAME_INDEX]);
        m_isSearch.LoadProjects();
        m_isSearch.SearchForImageOnTopics();
      }
      else
      {
        Console.WriteLine(Constants.IMG_PROG_MSG_INVALID_ARG);
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
    }
  }
}