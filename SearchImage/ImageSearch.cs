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
    /// Directory of the image file
    /// </summary>
    public string ImagePath { get; set; }
    /// <summary>
    /// List with all documentation projects found on the base path
    /// </summary>
    public List<Project> Projects { get; set; }
    /// <summary>
    /// Clock to count execution time of this application
    /// </summary>
    public Stopwatch ExecutionTime { get; }
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="p_strBasePath">A base path to start searching for</param>
    /// <param name="p_strImageName">A full path to an image file to search for</param>
    public ImageSearch(string p_strBasePath, string p_strImageName)
    {
      if (String.IsNullOrEmpty(p_strBasePath))
      {
        GlobalResult.LogErrorAndQuit(Constants.IMG_SEARCH_MSG_ERROR_BASE_NULL_EMPTY);
      }
      else
      {
        if (Directory.Exists(p_strBasePath))
        {
          BasePath = p_strBasePath;
        }
        else
        {
          GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_BASE_DOES_NOT_EXIST, p_strBasePath));
        }
      }
      if (String.IsNullOrEmpty(p_strImageName))
      {
        GlobalResult.LogErrorAndQuit(Constants.IMG_SEARCH_MSG_ERROR_IMG_NULL_EMPTY);
      }
      else
      {
        if (File.Exists(p_strImageName))
        {
          if (IsValidImageName(Path.GetFileName(p_strImageName)))
          {
            ImageName = Path.GetFileName(p_strImageName);
            ImagePath = String.Format(Constants.IMG_SEARCH_PROJECT_IMAGE_FOLDER,Path.GetDirectoryName(p_strImageName));
          }
          else
          {
            GlobalResult.LogErrorAndQuit(Constants.IMG_SEARCH_MSG_ERROR_IMG_INVALID);
          }
        }
        else
        {
          GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_BASE_IMG_DOES_NOT_EXIST, p_strImageName));
        }
      }
      Projects = new List<Project>();
      GlobalResult.NumberOfReferences = 0;
      ExecutionTime = new Stopwatch();
      ExecutionTime.Start();
    }
    /// <summary>
    /// Method to check whether an image file is valid, that is,
    /// whether its extension is a valid one
    /// </summary>
    /// <param name="p_strName">Image filename</param>
    /// <returns></returns>
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
    /// <summary>
    /// Method to load all projects found on the base path
    /// </summary>
    public void LoadProjects()
    {
      GlobalResult.LogGeneralMessage(Constants.IMG_SEARCH_MSG_START_APP);
      GlobalResult.LogGeneralMessage(Constants.IMG_SEARCH_MSG_START_LOADING_PROJECT);
      IEnumerable<string> m_lstProjects;
      try
      {
        m_lstProjects = Directory.EnumerateFileSystemEntries(BasePath, Constants.IMG_SEARCH_PROJECT_EXTENSION, SearchOption.AllDirectories);
        if (m_lstProjects.Count() > 0)
        {
          foreach (string m_strProject in m_lstProjects)
          {
            Project m_prjProject = new Project(m_strProject);
            if (m_prjProject.CheckSearchFolder(ImagePath))
            {
              Projects.Add(m_prjProject);
            }            
          }
          GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_SEARCH_MSG_PROJECT_FOUND, m_lstProjects.Count(), Projects.Count()));
        }
        else
        {
          GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_NO_PROJECT, BasePath));
        }
      }
      catch(ArgumentException m_exArg)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_ARG_EXCEPTION, BasePath, m_exArg.Message));
      }
      catch(DirectoryNotFoundException m_exDirNotFound)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_DIR_EXCEPTION, BasePath, m_exDirNotFound.Message));
      }
      catch(IOException m_exIO)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_SEARCH_MSG_ERROR_IO_EXCEPTION, BasePath, m_exIO.Message));
      }
    }
    /// <summary>
    /// Method to search for an image file on all topics
    /// of all projects found on the base path
    /// </summary>
    public void SearchForImageOnTopics()
    {
      GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_SEARCH_MSG_START_SEARCH_IMAGE, ImageName));
      foreach (Project m_prj in Projects)
      {
        m_prj.LoadTopics();
        m_prj.SearchImageOnTopics(ImageName);
      }
      GlobalResult.LogSeparator();
      if (GlobalResult.NumberOfReferences > 0)
      {
        GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_SEARCH_MSG_REFERENCE_FOUND, GlobalResult.NumberOfReferences));
      }
      else
      {
        GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_SEARCH_MSG_NO_REFERENCE_FOUND, ImageName));
      }
      ExecutionTime.Stop();
      GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_SEARCH_MSG_EXECUTION_TIME, ExecutionTime.Elapsed.Hours, ExecutionTime.Elapsed.Minutes, ExecutionTime.Elapsed.Seconds, ExecutionTime.Elapsed.Milliseconds));
      GlobalResult.SaveLogFile();
    }
  }
}