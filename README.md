# RunHidden

[![image](https://user-images.githubusercontent.com/79026235/152910441-59ba653c-5607-4f59-90c0-bc2851bf2688.png)Download the zip file](https://github.com/LesFerch/RunHidden/releases/download/1.1.0/RunHidden.zip)

## Run scripts and apps with no console window displayed

RunHidden launches (almost) any script or console application with the console window 100% suppressed.

It runs PowerShell scripts (ps1 extension), batch files (cmd and bat extensions), and Python scripts (py extension) directly via their respective interpreter (PowerShell.exe, Cmd.exe, and Python.exe). Those interpreters are assumed to be on the path.

All other script types are run via Cmd.exe (which will usually run the correct interpreter automatically). This approach may not work for everything. If you have a script type that you would like handled directly by its interpreter, please open an issue and I'll try to add it.

Obviously, this is meant for scripts and exes that do not require any console input and do something more than just produce some console output.

RunHidden passes through all command line arguments and is compatible with paths and arguments containing spaces. For PowerShell script arguments, it's not necessary to double up trailing, quoted backslashes, but it's okay to do that. For example, all of the following will work correctly:

RunHidden Script01.ps1 "C:\Some Folder"\
RunHidden Script01.ps1 "C:\Some Folder\\"\
RunHidden Script01.ps1 "C:\Some Folder\\\\"

RunHidden.exe is more convenienent than equivalent VBScript or JScript solutions. The main advantage is that it's easier to use with Windows shortcuts.

# How to Download and Run

1. Download the zip file using the link above.
2. Extract **RunHidden.exe**.
3. Right-click **RunHidden.exe**, select Properties, check **Unblock**, and click **OK**.
4. If you skipped step 3, then on first run, in the SmartScreen window, click **More info** and then **Run anyway**.
5. Move **RunHidden.exe** to the folder of your choice. For greatest convenience, place it in a folder that's on the search path.
6. Typically, you would make a shortcut to **RunHidden.exe** and edit the command line to add the path of the script you want to launch and any arguments you want to pass to that script.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen anytime you download a new executable and may require extra steps to whitelist the file.

## Usage examples

**Example 1**:\
Run a script hidden:\
`RunHidden C:\Scripts\Script01.ps1`

**Example 2**:\
Run with two arguments:\
`RunHidden C:\Scripts\Script01.cmd /theme=1 /size=1`

**Example 3**:\
Run from a folder with a space in its name:\
`RunHidden "C:\My Scripts\Script01.ps1"`

**Example 4**:\
Pass one argument that contains a space:\
`RunHidden "C:\My Scripts\Script01.ps1" "C:\Some Folder\"`

**Example 5**:\
Pass two arguments that contain spaces:\
`RunHidden "C:\My Scripts\Script01.ps1" "C:\Some Folder1\" "C:\Some Folder2\"`

## Testing Tip

Debugging can be tricky when you hide the console, but you can easily determine if your script is receiving the correct arguments from the command line, by calling this little [messagebox](https://github.com/cubiclesoft/messagebox-windows/tree/master) program from your script.

\
\
[![image](https://user-images.githubusercontent.com/79026235/153264696-8ec747dd-37ec-4fc1-89a1-3d6ea3259a95.png)](https://github.com/LesFerch/RunHidden)
