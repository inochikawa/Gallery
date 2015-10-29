;------------------------------------------------------------------------------
;   Определяем некоторые константы
;------------------------------------------------------------------------------

; Имя приложения
#define   Name       GetStringFileInfo("..\..\..\GraphicEditor\bin\Release\GraphicEditor.exe", "ProductName")
; Версия приложения
#define   Version    GetStringFileInfo("..\..\..\GraphicEditor\bin\Release\GraphicEditor.exe", "FileVersion")
; Фирма-разработчик
#define   Publisher  GetFileCompany("..\..\..\GraphicEditor\bin\Release\GraphicEditor.exe")
; Имя исполняемого модуля
#define   ExeName    "GraphicEditor.exe"

;------------------------------------------------------------------------------
;   Параметры установки
;------------------------------------------------------------------------------
[Setup]

; Уникальный идентификатор приложения, 
;сгенерированный через Tools -> Generate GUID
AppId={{6BD4D045-DB09-4186-9BF7-D4D8047D31E4}

; Прочая информация, отображаемая при установке
AppName={#Name}
AppVersion={#Version}
AppPublisher={#Publisher}

; Путь установки по-умолчанию
DefaultDirName={pf}\{#Name}
; Имя группы в меню "Пуск"
DefaultGroupName={#Name}

; Каталог, куда будет записан собранный setup и имя исполняемого файла
OutputDir=..\..\..\Install
OutputBaseFileName=Setup

; Параметры сжатия
Compression=lzma
SolidCompression=yes


;------------------------------------------------------------------------------
;   Опционально - некоторые задачи, которые надо выполнить при установке
;------------------------------------------------------------------------------
[Tasks]
; Создание иконки на рабочем столе
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked


;------------------------------------------------------------------------------
;   Файлы, которые надо включить в пакет установщика
;------------------------------------------------------------------------------
[Files]

; Исполняемый файл
Source: "..\..\..\GraphicEditor\bin\Release\GraphicEditor.exe"; DestDir: "{app}"; Flags: ignoreversion

; Прилагающиеся ресурсы
Source: "..\..\..\GraphicEditor\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; .NET Framework 4.0
Source: "..\..\..\Install\dotNetFx40_Full_x86_x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsRequiredDotNetDetected

;------------------------------------------------------------------------------
;   Указываем установщику, где он должен взять иконки
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"

Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon


;------------------------------------------------------------------------------
;   Секция кода включенная из отдельного файла
;------------------------------------------------------------------------------
[Code]
#include "dotnet.pas"


[Run]
;------------------------------------------------------------------------------
;   Секция запуска после инсталляции
;------------------------------------------------------------------------------
Filename: {tmp}\dotNetFx40_Full_x86_x64.exe; Parameters: "/q:a /c:""install /l /q"""; Check: not IsRequiredDotNetDetected; StatusMsg: Microsoft Framework 4.0 is installed. Please wait...