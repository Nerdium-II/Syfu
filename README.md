# Syfu
- Developer by BlackoutDev

Syfu is a C# program that is designed to render a system useless this is a PoC and should NOT be used for malicious purposes. It is small and compact and built for effectiveness. The current payloads are as follows:

    - If terminated the program will create a BSOD
    - MBR Overwrite
    - Registry Corrupting
    - Disables Task Manager, Registry Editor and the ability to use Ctrl + Alt + Del
    - Looping/annoying message box 

As mentioned, Syfu works fast and has no fancy GDI effects or anything like so.

## [How to setup]

To start you will need to have Visual Studio on your system so you can set up and compile Syfu. Once you have this, follow the steps below:

    - Create a empty C# solution in Visual Studio
    - Once created, open up the solution and add a Class1.cs file to the Project1
    - Also, add a app.manifest file to the project and replace the priviliges with admin once in the line "asInvoker" to "requiresAdministrator"
    - Now copy and paste the code from the Syfu.cs file you have downloaded from Github
    - You can edit the message in the MessageBox if you wish to whatever you want. This can be found in line 109. However, if you are not familiar with
      C# then you should not do this.
    - Now build the file

**Note: Some of the references may need importing this can be done by right clicking the main folder in the Solution Explorer tab and then navigating to the
references section, which can be found under "Add > references", and ticking the references that have given you an error.**
