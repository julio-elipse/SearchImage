using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace SearchImage
{
  class Topic
  {
    /// <summary>
    /// Path to a topic file on a documentation project
    /// </summary>
    public string TopicPath { get; set; }
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="p_strPath">Full path of a topic file</param>
    public Topic(string p_strPath)
    {
      if (String.IsNullOrEmpty(p_strPath))
      {
        GlobalResult.LogErrorAndQuit(Constants.IMG_TOPIC_MSG_ERROR_NULL_EMPTY);
      }
      else
      {
        if (File.Exists(p_strPath))
        {
          TopicPath = p_strPath;
        }
        else
        {
          GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_TOPIC_MSG_ERROR_DOES_NOT_EXIST, p_strPath));
        }
      }
    }
    /// <summary>
    /// Method to search for an image file on a topic file
    /// </summary>
    /// <param name="p_strImage">Name of an image file, without a path</param>
    public void SearchForImage(string p_strImage)
    {
      XmlDocument m_xmlTopic = new XmlDocument();
      try
      {
        m_xmlTopic.Load(TopicPath);
        XmlNodeList m_xmlImages = m_xmlTopic.SelectNodes(String.Format(Constants.IMG_TOPIC_XPATH_IMAGE_SRC, p_strImage));
        if (m_xmlImages.Count > 0)
        {
          if (m_xmlImages.Count == 1)
          {
            GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_TOPIC_MSG_IMAGE_FOUND_SINGLE, m_xmlImages.Count, p_strImage, TopicPath));
          }
          else
          {
            GlobalResult.LogGeneralMessage(String.Format(Constants.IMG_TOPIC_MSG_IMAGE_FOUND_MANY, m_xmlImages.Count, p_strImage, TopicPath));
          }
          GlobalResult.NumberOfReferences += m_xmlImages.Count;
        }
      }
      catch(FileNotFoundException m_exNotFound)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_TOPIC_MSG_ERROR_FILE_NOT_FOUND, TopicPath, m_exNotFound.Message));
      }
      catch(IOException m_exIO)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_TOPIC_MSG_ERROR_IO_EXCEPTION, TopicPath, m_exIO.Message));
      }
      catch(XmlException m_exXml)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_TOPIC_MSG_ERROR_XML_EXCEPTION, TopicPath, m_exXml.Message));
      }
      catch(XPathException m_exXPath)
      {
        GlobalResult.LogErrorAndQuit(String.Format(Constants.IMG_TOPIC_MSG_ERROR_XPATH_EXCEPTION, TopicPath, m_exXPath.Message));
      }
    }
  }
}