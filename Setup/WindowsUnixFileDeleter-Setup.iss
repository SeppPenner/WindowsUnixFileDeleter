; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "WindowsUnixFileDeleter"
#define MyAppVersion "1.0.0.2"
#define MyAppPublisher "H�mmer Electronics"
#define MyAppURL "www.softwareload24.de.tl"
#define MyAppExeName "WindowsUnixFileDeleter.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{211B0B52-0B42-422B-B937-591727BA7C45}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
VersionInfoProductVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
UninstallDisplayIcon={app}\{#MyAppExeName}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile=C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\bin\Debug\License.txt
OutputDir=C:\Users\tim\Desktop\WindowsUnixFileDeleter\Setup
OutputBaseFilename=WindowsUnixFileDeleter-Setup
SetupIconFile=C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\DeleteFile.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\bin\Debug\WindowsUnixFileDeleter.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\bin\Debug\Languages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\bin\Debug\License.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\tim\Desktop\WindowsUnixFileDeleter\WindowsUnixFileDeleter\bin\Debug\languages\*"; DestDir: "{app}\languages\"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

