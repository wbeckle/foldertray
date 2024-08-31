# Summary
A simple Windows form app that presents folders and their files as a menu with submenus in the systray. Similar to pinning a folder to the taskbar in Windows 10. It was written because this feature is not available in Windows 11.<br/>
When a file is double-clicked it executes the file with the appropriate handler.
- ps1 files are executed with pwsh.exe
- js files executed with node.exe
- sh files use WSL.
- lnk and all other files are handled by the normal Windows process start functionality.

# Rewrite (Tray2)
- Rewritten to use app config to store the paths, and TreeNode to display the folders and files.
- Added other minor convenience features like right click to edit, and refresh folder functionality.
- Editor is vscode (code).

<br/>

The program reads the "paths" entry in app.config with the list of folders that make up the main menu. Paths are separated by a semicolon.
The folders are expected to contain files that you want to execute when double-clicked.

<br/>
