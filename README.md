# NHTHEBEST DLLs
 -- *The DLL that has some usfull functions*
 
 
 ## Functions:
 - NetCat Lib
 - Log *That gets send to server*
 - JS  & CSS Minifyer
 - C# and VB complier - **BETA**
 - Convert image to on color *average color*
 - Console FX
    - Linux Style Console Log
    - Linux Style Console Hash Percent Bar
 - CPU info class
 - Task Splitter aka SuperThread

### NetCat:
``` csharp
namespace Network 
{
    class NetCat 
    {
        // client
        public void Connect(string host, int port);
        // server
        public void Listen(IPAddress host, int port);
        public void Dispose();
        // all
        public void Send(string text);
        public void SendBytes(byte[] data);
        public string ReceiveLine();
        public byte[] ReceiveBytes(int bytes);
    }
}
```
### Log:
``` csharp
namespace Loging 
{
    public class NetworkLog 
    {
        public string LogServer;
        public int LogSendSize = 10000;
        public void SendLog();
        public void Log(object text);
    }
}
```
### JS & CSS Minifyer:
``` csharp
namespace Code 
{
    public class javascript
    {
        public static string Minify(string code);
    }
    public class css
    {
        public static string Minify(string code);
    }
}
```
### VB & C# Complier **BETA**:
``` csharp
namespace Code 
{
    public class CS
    {
         public static Action Compile(string code, 
         string namespaceandclass, 
         string mainfunction, 
         string[] ReferencedAssemblies, 
         bool InMem = true, bool Exe = true);
    }
    public class VB
    {
         public static Action Compile(string code,
         string namespaceandclass, 
         string mainfunction, 
         string[] ReferencedAssemblies, 
         bool InMem = true, bool Exe = true);
    }
}
```
### Image to Color:
``` csharp
namespace Graphics
{
    public class SortImgs
    {
        public int Resolution { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Color GetColor(Image image);
        public List<Color> GetColors(List<Image> images);
        public Color[] GetColors(Image[] images);
        public event EventHandler<LineEventArgs> Line_done;
        public event EventHandler<PixalEventArgs> Pixal_done;
    }
    public class PixalEventArgs
    {
        public int Pixals { get; set; }
    }
    public class LineEventArgs
    {
        public int Lines { get; set; }
    }
}
```
### Console FX:
``` csharp
namespace Graphics
{
    public class ConsoleFX
    {
        public static void HashPrecentBar(int Value, int Off);
        public static void ColorLog(object data, LogStatus status = LogStatus.OK);
    }
    public enum LogStatus
    {
        OK, Fail, Warning
    }
}
```
### CPU Info:
``` csharp
namespace Efficiency
{
    public sealed class CPU 
    {
        public static int LogicalProcessors { get; }
        public static int PhysicalCores { get; }
        public static int PhysicalProcessors { get; }
    }
}
```
### Task Splitter:
``` csharp
namespace Efficiency
{
    public class SuperThread 
    {
        public List<Action> Code { get; set; }
        public int MaxCoresToUse { get; set; } = CPU.LogicalProcessors;
        public void Join();
        public void Start();
    }
}
```
