namespace SearchImage
{
  static class Constants
  {
    //Message when this application does not receive the expected arguments
    public const string IMG_PROG_MSG_INVALID_ARG = "Please provide a directory path and an image filename path to start a search.";

    public const string IMG_TOPIC_MSG_ERROR_DOES_NOT_EXIST = "Topic file '{0}' does not exist";
    public const string IMG_TOPIC_MSG_ERROR_NULL_EMPTY = "Topic file is null or empty";
    public const string IMG_TOPIC_MSG_ERROR_FILE_NOT_FOUND = "Topic file '{0}' not found. Message: {1}";
    public const string IMG_TOPIC_MSG_ERROR_IO_EXCEPTION = "I/O exception on topic file '{0}'. Message: {1}";
    public const string IMG_TOPIC_MSG_ERROR_XML_EXCEPTION = "XML exception on topic file '{0}'. Message: {1}";
    public const string IMG_TOPIC_MSG_ERROR_XPATH_EXCEPTION = "XPath exception on topic file '{0}'. Message: {1}";
    public const string IMG_TOPIC_MSG_IMAGE_FOUND_SINGLE = "Found one reference to image filename '{1}'\r\n  On topic file '{2}'";
    public const string IMG_TOPIC_MSG_IMAGE_FOUND_MANY = "Found {0} references to image filename '{1}'\r\n  On topic file '{2}'";
    public const string IMG_TOPIC_XPATH_IMAGE_SRC = "//image[@src='{0}']";

    public const string IMG_PROJECT_MSG_ERROR_NULL_EMPTY = "Project path is null or empty";
    public const string IMG_PROJECT_MSG_ERROR_DOES_NOT_EXIST = "Project path '{0}' does not exist";
    public const string IMG_PROJECT_MSG_ERROR_MAP_NOT_FOUND = "Map file '{0}' was not found. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_IO_EXCEPTION = "I/O exception on map file '{0}'. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_XML_EXCEPTION = "XML exception on map file '{0}'. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_XPATH_EXCEPTION = "XPath exception on map file '{0}'. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_SEARCH_NOT_FOUND = "Project file '{0}' not found. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_SEARCH_IO_EXCEPTION = "I/O exception on project file '{0}'. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_SEARCH_XML_EXCEPTION = "XML exception on project file '{0}'. Message: {1}";
    public const string IMG_PROJECT_MSG_ERROR_SEARCH_XPATH_EXCEPTION = "XPath exception on project file '{0}'. Message: {1}";

    public const string IMG_PROJECT_MAP_FULL_PATH = "{0}\\Maps\\table_of_contents.xml";
    public const string IMG_PROJECT_TOPIC_FULL_PATH = "{0}{1}.xml";
    public const string IMG_PROJECT_TOPIC_PATH = "{0}\\Topics\\";
    public const string IMG_PROJECT_TOPIC_ATTR_HREF = "href";
    public const string IMG_PROJECT_XPATH_TOPICREF = "//topicref";
    public const string IMG_PROJECT_XPATH_SEARCHPATH = "//config/config-group[@name='project']/config-value[@name='searchpath']";

    public const int IMG_ENV_EXIT_FAIL = 1;
    public const int IMG_EXTENSION_SPLIT_INDEX = 1;
    public const int IMG_ARG_PATH_NAME_INDEX = 0;
    public const int IMG_ARG_IMAGE_NAME_INDEX = 1;
    public const int IMG_ARG_VALID_COUNT = 2;
    public const char IMG_EXTENSION_SPLIT_CHAR = '.';
    public const char IMG_SEPARATOR_SPLIT_CHAR = ';';

    public const string IMG_SEARCH_MSG_ERROR_BASE_NULL_EMPTY = "Base path is null or empty";
    public const string IMG_SEARCH_MSG_ERROR_BASE_DOES_NOT_EXIST = "Base path '{0}' does not exist";
    public const string IMG_SEARCH_MSG_ERROR_BASE_IMG_DOES_NOT_EXIST = "Image file '{0}' does not exist";
    public const string IMG_SEARCH_MSG_ERROR_IMG_NULL_EMPTY = "Image filename is null or empty";
    public const string IMG_SEARCH_MSG_ERROR_IMG_INVALID = "Image filename is not a valid filename. Please provide a filename with a valid extension (.bmp, .gif, .jpeg, .jpg, .tif, or .tiff)";
    public const string IMG_SEARCH_MSG_ERROR_ARG_EXCEPTION = "Argument exception on base path {0}. Message: {1}";
    public const string IMG_SEARCH_MSG_ERROR_DIR_EXCEPTION = "Directory not found exception on base path {0}. Message: {1}";
    public const string IMG_SEARCH_MSG_ERROR_IO_EXCEPTION = "I/O exception found on base path {0}. Message: {1}";
    
    public const string IMG_SEARCH_MSG_START_LOADING_PROJECT = "Starting to load projects...";
    public const string IMG_SEARCH_MSG_PROJECT_FOUND = "Number of projects found: {0}\r\nNumber of projects loaded: {1}";
    public const string IMG_SEARCH_MSG_REFERENCE_FOUND = "Number of references found: {0}";
    public const string IMG_SEARCH_MSG_NO_REFERENCE_FOUND = "No references found for '{0}'";
    public const string IMG_SEARCH_MSG_ERROR_NO_PROJECT = "Directory '{0}' does not contain any Help & Manual project (.hmxp)";
    public const string IMG_SEARCH_MSG_START_SEARCH_IMAGE = "Starting to search topics for image filename '{0}'...";
    public const string IMG_SEARCH_PROJECT_EXTENSION = "*.hmxp";
    public const string IMG_SEARCH_PROJECT_IMAGE_FOLDER = "{0}\\";
    public const string IMG_SEARCH_MSG_EXECUTION_TIME = "Total time of this search: {0:00}:{1:00}:{2:00}.{3:000}";
    public const char IMG_SEARCH_MSG_SEPARATOR_CHAR = '=';
    public const int IMG_SEARCH_MSG_SEPARATOR_SIZE = 80;
    //Valid extensions for image files
    public const string IMG_EXTENSION_BMP = "bmp";
    public const string IMG_EXTENSION_GIF = "gif";
    public const string IMG_EXTENSION_PNG = "png";
    public const string IMG_EXTENSION_JPG = "jpg";
    public const string IMG_EXTENSION_JPEG = "jpeg";
    public const string IMG_EXTENSION_TIF = "tif";
    public const string IMG_EXTENSION_TIFF = "tiff";
  }
}