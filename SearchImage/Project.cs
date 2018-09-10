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

    public Project(string p_strPath)
    {
      if (String.IsNullOrEmpty(p_strPath))
      {
        Console.WriteLine(Constants.IMG_PROJECT_MSG_ERROR_NULL_EMPTY);
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
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
          Console.WriteLine(String.Format(Constants.IMG_PROJECT_MSG_ERROR_DOES_NOT_EXIST, p_strPath));
          Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
        }
      }
      Topics = new List<Topic>();
    }
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
        Console.WriteLine(String.Format(Constants.IMG_PROJECT_MSG_ERROR_MAP_NOT_FOUND, ProjectMap, m_exNotFound.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(IOException m_exIO)
      {
        Console.WriteLine(String.Format(Constants.IMG_PROJECT_MSG_ERROR_IO_EXCEPTION, ProjectMap, m_exIO.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(XmlException m_exXml)
      {
        Console.WriteLine(String.Format(Constants.IMG_PROJECT_MSG_ERROR_XML_EXCEPTION, ProjectMap, m_exXml.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(XPathException m_exXPath)
      {
        Console.WriteLine(String.Format(Constants.IMG_PROJECT_MSG_ERROR_XPATH_EXCEPTION, ProjectMap, m_exXPath.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
    }
    public void SearchImageOnTopics(string p_strImage)
    {
      foreach (Topic m_topic in Topics)
      {
        m_topic.SearchForImage(p_strImage);
      }
    }
  }
}