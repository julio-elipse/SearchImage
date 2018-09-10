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

    public Topic(string p_strPath)
    {
      if (String.IsNullOrEmpty(p_strPath))
      {
        Console.WriteLine(Constants.IMG_TOPIC_MSG_ERROR_NULL_EMPTY);
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      else
      {
        if (File.Exists(p_strPath))
        {
          TopicPath = p_strPath;
        }
        else
        {
          Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_ERROR_DOES_NOT_EXIST, p_strPath));
          Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
        }
      }
    }
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
            Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_IMAGE_FOUND_SINGLE, m_xmlImages.Count, p_strImage, TopicPath));
          }
          else
          {
            Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_IMAGE_FOUND_MANY, m_xmlImages.Count, p_strImage, TopicPath));
          }
          //Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_IMAGE_FOUND, m_xmlImages.Count, p_strImage, TopicPath));
          GlobalResult.NumberOfReferences += m_xmlImages.Count;
        }
      }
      catch(FileNotFoundException m_exNotFound)
      {
        Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_ERROR_FILE_NOT_FOUND, TopicPath, m_exNotFound.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(IOException m_exIO)
      {
        Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_ERROR_IO_EXCEPTION, TopicPath, m_exIO.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(XmlException m_exXml)
      {
        Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_ERROR_XML_EXCEPTION, TopicPath, m_exXml.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
      catch(XPathException m_exXPath)
      {
        Console.WriteLine(String.Format(Constants.IMG_TOPIC_MSG_ERROR_XPATH_EXCEPTION, TopicPath, m_exXPath.Message));
        Environment.Exit(Constants.IMG_ENV_EXIT_FAIL);
      }
    }
  }
}