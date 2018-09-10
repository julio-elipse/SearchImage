using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SearchImage
{
  class ImageSearch
  {
    /// <summary>
    /// Base path to search for documentation projects (.hmxp)
    /// </summary>
    public string BasePath { get; set; }
    /// <summary>
    /// Name of an image file to search for
    /// </summary>
    public string ImageName { get; set; }
    /// <summary>
    /// List with all documentation projects found on the base path
    /// </summary>
    public List<Project> Projects { get; set; }
    /// <summary>
    /// Clock to count execution time of this application
    /// </summary>
    public Stopwatch ExecutionTime { get; }

    public ImageSearch(string p_strBasePath, string p_strImageName)
    {
      if (String.IsNullOrEmpty(p_strBasePath))
      {
        Console.WriteLine(Constants.IMG_SEARCH_MSG_ERROR_BASE_NULL_EMPTY);
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      else
      {
        if (Directory.Exists(p_strBasePath))
        {
          BasePath = p_strBasePath;
        }
        else
        {
          Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_ERROR_BASE_DOES_NOT_EXIST, p_strBasePath));
          Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
        }
      }
      if (String.IsNullOrEmpty(p_strImageName))
      {
        Console.WriteLine(Constants.IMG_SEARCH_MSG_ERROR_IMG_NULL_EMPTY);
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      else
      {
        if (IsValidImageName(p_strImageName))
        {
          ImageName = p_strImageName;
        }
        else
        {
          Console.WriteLine(Constants.IMG_SEARCH_MSG_ERROR_IMG_INVALID);
          Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
        }
      }
      Projects = new List<Project>();
      GlobalResult.NumberOfReferences = 0;
      ExecutionTime = new Stopwatch();
      ExecutionTime.Start();
    }
    private bool IsValidImageName(string p_strName)
    {
      string[] m_arrExtensions = new string[] {
        Constants.IMG_EXTENSION_BMP,
        Constants.IMG_EXTENSION_GIF,
        Constants.IMG_EXTENSION_JPEG,
        Constants.IMG_EXTENSION_JPG,
        Constants.IMG_EXTENSION_PNG,
        Constants.IMG_EXTENSION_TIF,
        Constants.IMG_EXTENSION_TIFF
      };
      string[] m_arrParts = p_strName.Split(Constants.IMG_EXTENSION_SPLIT_CHAR);
      return m_arrExtensions.Contains(m_arrParts[Constants.IMG_EXTENSION_SPLIT_INDEX]);
    }
    public void LoadProjects()
    {
      Console.WriteLine(Constants.IMG_SEARCH_MSG_START_LOADING_PROJECT);
      IEnumerable<string> m_lstProjects;
      try
      {
        m_lstProjects = Directory.EnumerateFileSystemEntries(BasePath, Constants.IMG_SEARCH_PROJECT_EXTENSION, SearchOption.AllDirectories);
        if (m_lstProjects.Count() > 0)
        {
          foreach (string m_strProject in m_lstProjects)
          {
            Project m_prjProject = new Project(m_strProject);
            Projects.Add(m_prjProject);
          }
          Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_PROJECT_FOUND, m_lstProjects.Count()));
        }
        else
        {
          Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_ERROR_NO_PROJECT, BasePath));
          Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
        }
      }
      catch(ArgumentException m_exArg)
      {
        Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_ERROR_ARG_EXCEPTION, BasePath, m_exArg.Message));
      }
      catch(DirectoryNotFoundException m_exDirNotFound)
      {
        Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_ERROR_DIR_EXCEPTION, BasePath, m_exDirNotFound.Message));
      }
      catch(IOException m_exIO)
      {
        Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_ERROR_IO_EXCEPTION, BasePath, m_exIO.Message));
      }
    }
    public void SearchForImageOnTopics()
    {
      Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_START_SEARCH_IMAGE, ImageName));
      foreach (Project m_prj in Projects)
      {
        m_prj.LoadTopics();
        m_prj.SearchImageOnTopics(ImageName);
      }
      Separator(Constants.IMG_SEARCH_MSG_SEPARATOR_SIZE);
      if (GlobalResult.NumberOfReferences > 0)
      {
        Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_REFERENCE_FOUND, GlobalResult.NumberOfReferences));
      }
      else
      {
        Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_NO_REFERENCE_FOUND, ImageName));
      }
      ExecutionTime.Stop();
      Console.WriteLine(String.Format(Constants.IMG_SEARCH_MSG_EXECUTION_TIME, ExecutionTime.Elapsed.Hours, ExecutionTime.Elapsed.Minutes, ExecutionTime.Elapsed.Seconds, ExecutionTime.Elapsed.Milliseconds));
    }

    private void Separator(int p_intSize)
    {
      string m_strSeparator = new string(Constants.IMG_SEARCH_MSG_SEPARATOR_CHAR, p_intSize);
      Console.WriteLine(m_strSeparator);
    }
  }
}