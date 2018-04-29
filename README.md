# ElDewCon
CLI Rcon Tool on .NETCore with support for auto messages.
Can be used on Linux with tmux or similar programs to keep it running in the background.

# Requirements
**Linux:** [.NET Core Runtime 2.0+](https://www.microsoft.com/net/download/linux-package-manager/ubuntu17-10/runtime-2.0.5)

**Windows:** [.NET Core Runtime 2.0+](https://www.microsoft.com/net/download/windows/run)

# How To Download
**Linux:** `wget https://github.com/Swiggies/ElDewCon/releases/download/0.2.5/ElDewCon_0.2.5.zip`

**Windows:** Download the zip from [here](https://github.com/Swiggies/ElDewCon/releases/latest).

# How to Use
Type quit/exit to quit/exit.
## Linux
1. Please double check you have .NET Core 2.0+
2. Navigate to the folder you extracted ElDewCon to.
3. Run the command `dotnet ./ElDewCon.dll`
4. Enter the details that are prompted.
5. Enjoy!

## Windows
1. Open a CMD window in the folder you extracted ElDewCon to
2. Run the command `dotnet ElDewCon.dll`

## Auto-Messages
The Messages.json file comes with 3 default messages to show how it works. 
The "message" line is what the server will announce. The "time" field is how long a message will take to appear in minutes.

# Issues & Feature Requests
Please submit these in the issues section.

## Known issues
### When starting with arguments my password doesn't work
You may have some escape characters in your password. Make sure to wrap your password in double quotations `"password"` and if you have a % symbol in your password you will have to double it up. E.g `pass%word` would become `pas%%word`.
