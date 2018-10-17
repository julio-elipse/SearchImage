# SearchImage
This project is a command line tool developed to search for image files used on documentation projects created using [Help & Manual](https://www.helpandmanual.com).

Although **Help & Manual** contains a **Report Tool** that shows all image files used by a single project or by its merged projects, there is no way to perform a reverse search, that is, given an image file, perform a search to find which projects use that file.

So, **SearchImage** provides a way to detect where an image file is used on a folder with **Help & Manual** projects (`.hmxp` files).

To use it, start a **Command Prompt** on Windows and type the following command:

  `> searchimage [project_folder] [image_path]`

**NOTES**

> The `project_folder` parameter is a path to a folder with `.hmxp` files.

> The `image_path` parameter is the full path of an image file.

> The `project_folder` parameter is a path without an `\` at the end.

> The `project_folder` and `image_path` parameters are case-insensitive.

> The `image_path` parameter **must** contain a filename with a valid image extension. The acceptable extensions are `.bmp`, `.gif`, `.png`, `.jpg`, `.jpeg`, `.tif`, or `.tiff`.

> After its execution, a log file (`.log`) is saved to the **Documents** folder of the current user, which is usually `C:\Users\user_name\Documents`. This file is saved in the format `searchimage_yyyyMMddHHmmssfff.log` and contains all messages displayed on the system console.

## Requirements

+ This application was built using **C#** and **.NET Framework 4.6.1**.
+ This application was tested using **Help & Manual 7.3.5 Build 4434**.
