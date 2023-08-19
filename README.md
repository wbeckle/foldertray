# Summary
A simple Windows form app that presents folders and their files as a menu with submenus in the systray. Similar to pinning a folder to the taskbar in Windows 10. It was written because this feature is not available in Windows 11.<br/>
When a file is clicked it executes the file with the appropriate handler.
- ps1 files are executed with pwsh.exe
- js files executed with node.exe
- lnk and all other files are handled by the normal Windows process start functionality.

<br/>

The program reads a file called FolderTray.txt in the same folder as the executable. It contains a list of folders that make up the main menu. One folder per line, see the sample file.
The folders are expected to contain files that you want to execute when clicked.

<br/>

## Known bugs
In recent builds of Windows 11, sometimes the folders menus are not generated. I have no idea why this happens. A trace of the code shows the filenames are added to the menu items but they just don't show up. It hardly happens, so I haven't bothered to look at it in detail.
