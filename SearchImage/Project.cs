using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace SearchImage
{
  class Project
  {
    /// <summary>
    /// Path to a project file (.hmxp)
    /// </summary>
    public string ProjectPath { get; set; }
    /// <summary>
    /// Path to a map file (an XML file with all topics) on a documentation project
    /// </summary>
    public string ProjectMap { get; set; }
    /// <summary>
    /// Path to a folder with topic files on a documentation project
    /// </summary>
    public string ProjectTopics { get; set; }
    /// <summary>
    /// List with all topics on a documentation project
    /// </summary>
    public List<Topic> Topics { get; set; }
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="p_strPath">Project path</param>
    public Project(string p_strPath)
    {
      if (String.IsNullOrEmpty(p_strPath))
      {
        GlobalResult.LogErrorAndQuit(Constants.IMG_PROJECT_MSG_ERROR_NULL_EMPTY);
      }
      else
      {
        if (File.Exists(p_strPath))
        {
          ProjectPath = p_strPath;
          ProjectMap = String.Format(Constants.IMG_PROJECT_MAP_FULL_PATH, Directory.GetParent(ProjectPath).FullName);
          ProjectTopics = String.Format(Constants.IMG_PROJECT_TOPIC_PATH, Directory.GetParent(ProjectPath).FullName);
        }
        else
        {
          GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_DOES_NOT_EXIST, p_strPath));
        }
      }
      Topics = new List<Topic>();
    }
    /// <summary>
    /// Method to load all topics from this project
    /// </summary>
    public void LoadTopics()
    {
      XmlDocument m_xmlToc = new XmlDocument();
      try
      {
        m_xmlToc.Load(ProjectMap);
        XmlNodeList m_xmlTopics = m_xmlToc.SelectNodes(Constants.IMG_PROJECT_XPATH_TOPICREF);
        if (m_xmlTopics.Count > 0)
        {
          foreach (XmlNode m_xmlTopic in m_xmlTopics)
          {
            string m_strHref = m_xmlTopic.Attributes.GetNamedItem(Constants.IMG_PROJECT_TOPIC_ATTR_HREF).Value;
            string m_strTopicPath = String.Format(Constants.IMG_PROJECT_TOPIC_FULL_PATH, ProjectTopics, m_strHref);
            Topic m_tpcTopic = new Topic(m_strTopicPath);
            Topics.Add(m_tpcTopic);
          }
        }
      }
      catch(FileNotFoundException m_exNotFound)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_MAP_NOT_FOUND, ProjectMap, m_exNotFound.Message));
      }
      catch(IOException m_exIO)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_IO_EXCEPTION, ProjectMap, m_exIO.Message));
      }
      catch(XmlException m_exXml)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_XML_EXCEPTION, ProjectMap, m_exXml.Message));
      }
      catch(XPathException m_exXPath)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_XPATH_EXCEPTION, ProjectMap, m_exXPath.Message));
      }
    }
    /// <summary>
    /// Method to check whether a search path on a project
    /// contains the image path passed as a parameter to the application
    /// </summary>
    /// <param name="p_strPath">Path of the image folder to search for</param>
    /// <returns></returns>
    public bool CheckSearchFolder(string p_strPath)
    {
      XmlDocument m_xmlProj = new XmlDocument();
      try
      {
        m_xmlProj.Load(ProjectPath);
        XmlNode m_xmlSearchPath = m_xmlProj.SelectSingleNode(Constants.IMG_PROJECT_XPATH_SEARCHPATH);
        if (m_xmlSearchPath != null)
        {
          string[] m_strFolders = m_xmlSearchPath.InnerText.Split(new[] { Constants.IMG_SEPARATOR_SPLIT_CHAR }, StringSplitOptions.RemoveEmptyEntries);
          if (m_strFolders.Length > 0)
          {
            foreach (string m_strSearchPath in m_strFolders)
            {
              string m_strCombinedPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ProjectPath), m_strSearchPath));
              if (m_strCombinedPath.Equals(p_strPath, StringComparison.OrdinalIgnoreCase))
              {
                return true;
              }
            }
          }
          else
          {
            return false;
          }
        }
        else
        {
          return false;
        }
      }
      catch(FileNotFoundException p_exNotFound)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_SEARCH_NOT_FOUND, ProjectPath, p_exNotFound.Message));
      }
      catch (IOException p_exIO)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_SEARCH_IO_EXCEPTION, ProjectPath, p_exIO.Message));
      }
      catch (XmlException p_exXml)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_SEARCH_XML_EXCEPTION, ProjectPath, p_exXml.Message));
      }
      catch (XPathException p_exXPath)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_PROJECT_MSG_ERROR_SEARCH_XPATH_EXCEPTION, ProjectPath, p_exXPath.Message));
      }
      return false;
    }
    /// <summary>
    /// Method to search for an image file on the list of topics of this project.
    /// </summary>
    /// <param name="p_strImage">Name of an image file, without a path</param>
    public void SearchImageOnTopics(string p_strImage)
    {
      foreach (Topic m_topic in Topics)
      {
        m_topic.SearchForImage(p_strImage);
      }
    }
  }
}