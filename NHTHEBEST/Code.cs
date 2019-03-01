using System;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.IO;

namespace NHTHEBEST
{
    namespace Code
    {
        public class CompilerOptions
        {
            string Code;
           // string NamespaceAndClass;
            string Main;
            string[] ReferencedAssemblies;
        }
        public class javascript
        {
            private static string mModifiedData = "";
            private static BinaryReader GenerateStreamFromString(string s)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                var output = new BinaryReader(stream);
                return output;
            }

            public static string Minify(string code)
            {
                BinaryReader mReader = GenerateStreamFromString(code);
                int lastChar = 1;                   // current byte read
                int thisChar;// = -1;                  // previous byte read
                int nextChar = -1;                  // byte read in peek()
                bool endProcess = false;            // loop control
                bool ignore = false;                // if false then add byte to final output
                bool inComment = false;             // true when current bytes are part of a comment
                bool isDoubleSlashComment = false;  // '//' comment


                // main processing loop
                while (!endProcess)
                {
                    endProcess = (mReader.PeekChar() == -1);    // check for EOF before reading
                    if (endProcess)
                        break;

                    ignore = false;
                    thisChar = mReader.ReadByte();

                    if (thisChar == '\t')
                    {
                        thisChar = ' ';
                    }
/*                    else if (thisChar == '\t')
                    {
                        thisChar = '\n';
                    }*/
                    else if (thisChar == '\r')
                    {
                        thisChar = '\n';
                    }

                    if (thisChar == '\n')
                    {
                        ignore = true;
                    }

                    if (thisChar == ' ')
                    {
                        if ((lastChar == ' ') || IsDelimiter(lastChar) == 1)
                            ignore = true;
                        else
                        {
                            endProcess = (mReader.PeekChar() == -1); // check for EOF
                            if (!endProcess)
                            {
                                nextChar = mReader.PeekChar();
                                if (IsDelimiter(nextChar) == 1)
                                    ignore = true;
                            }
                        }
                    }


                    if (thisChar == '/')
                    {
                        nextChar = mReader.PeekChar();
                        if (nextChar == '/' || nextChar == '*')
                        {
                            ignore = true;
                            inComment = true;
                            if (nextChar == '/')
                                isDoubleSlashComment = true;
                            else
                                isDoubleSlashComment = false;
                        }


                    }

                    // ignore all characters till we reach end of comment
                    if (inComment)
                    {
                        while (true)
                        {
                            thisChar = mReader.ReadByte();
                            if (thisChar == '*')
                            {
                                nextChar = mReader.PeekChar();
                                if (nextChar == '/')
                                {
                                    thisChar = mReader.ReadByte();
                                    inComment = false;
                                    break;
                                }
                            }
                            if (isDoubleSlashComment && thisChar == '\n')
                            {
                                inComment = false;
                                break;
                            }

                        } // while (true)
                        ignore = true;
                    } // if (inComment) 


                    if (!ignore)
                        addToOutput(thisChar);

                    lastChar = thisChar;
                } // while (!endProcess) 
                return mModifiedData;
            }


            private static void addToOutput(int c)
            {
                mModifiedData += (char)c;
            }
            private static int IsAlphanumeric(int c)
            {
                int retval = 0;

                if ((c >= 'a' && c <= 'z') ||
                    (c >= '0' && c <= '9') ||
                    (c >= 'A' && c <= 'Z') ||
                    c == '_' || c == '$' || c == '\\' || c > 126)
                    retval = 1;

                return retval;

            }
            private static int IsDelimiter(int c)
            {
                int retval = 0;

                if (c == '(' || c == ',' || c == '=' || c == ':' ||
                    c == '[' || c == '!' || c == '&' || c == '|' ||
                    c == '?' || c == '+' || c == '-' || c == '~' ||
                    c == '*' || c == '/' || c == '{' || c == '\n' ||
                    c == ','
                )
                {
                    retval = 1;
                }

                return retval;

            }
        }
        public class css
        {
            private static string mModifiedData = "";
            private static BinaryReader GenerateStreamFromString(string s)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                var output = new BinaryReader(stream);
                return output;
            }
            public static string Minify(string code)
            {
                BinaryReader mReader = GenerateStreamFromString(code);
                int lastChar = 1;                   // current byte read
                int thisChar = -1;                  // previous byte read
                int nextChar = -1;                  // byte read in peek()
                bool endProcess = false;            // loop control
                bool ignore = false;                // if false then add byte to final output
                bool inComment = false;             // true when current bytes are part of a comment
                bool isDoubleSlashComment = false;  // '//' comment


                // main processing loop
                while (!endProcess)
                {
                    endProcess = (mReader.PeekChar() == -1);    // check for EOF before reading
                    if (endProcess)
                        break;

                    ignore = false;
                    thisChar = mReader.ReadByte();

                    if (thisChar == '\t')
                        thisChar = ' ';
                    else if (thisChar == '\t')
                        thisChar = '\n';
                    else if (thisChar == '\r')
                        thisChar = '\n';

                    if (thisChar == '\n')
                        ignore = true;

                    if (thisChar == ' ')
                    {
                        if ((lastChar == ' ') || isDelimiter(lastChar) == 1)
                            ignore = true;
                        else
                        {
                            endProcess = (mReader.PeekChar() == -1); // check for EOF
                            if (!endProcess)
                            {
                                nextChar = mReader.PeekChar();
                                if (isDelimiter(nextChar) == 1)
                                    ignore = true;
                            }
                        }
                    }


                    if (thisChar == '/')
                    {
                        nextChar = mReader.PeekChar();
                        if (nextChar == '/' || nextChar == '*')
                        {
                            ignore = true;
                            inComment = true;
                            if (nextChar == '/')
                                isDoubleSlashComment = true;
                            else
                                isDoubleSlashComment = false;
                        }
                        if (nextChar == '/')
                        {
                            int x = 0;
                            x = x + 1;
                        }

                    }

                    // ignore all characters till we reach end of comment
                    if (inComment)
                    {
                        while (true)
                        {
                            thisChar = mReader.ReadByte();
                            if (thisChar == '*')
                            {
                                nextChar = mReader.PeekChar();
                                if (nextChar == '/')
                                {
                                    thisChar = mReader.ReadByte();
                                    inComment = false;
                                    break;
                                }
                            }
                            if (isDoubleSlashComment && thisChar == '\n')
                            {
                                inComment = false;
                                break;
                            }

                        } // while (true)
                        ignore = true;
                    } // if (inComment) 


                    if (!ignore)
                        addToOutput(thisChar);

                    lastChar = thisChar;
                } // while (!endProcess) 
                return mModifiedData;
            }
            private static void addToOutput(int c)
            {
                mModifiedData += (char)c;
            }
            private static int isAlphanumeric(int c)
            {
                int retval = 0;

                if ((c >= 'a' && c <= 'z') ||
                    (c >= '0' && c <= '9') ||
                    (c >= 'A' && c <= 'Z') ||
                    c == '_' || c == '$' || c == '\\' || c > 126)
                    retval = 1;

                return retval;

            }
            private static int isDelimiter(int c)
            {
                int retval = 0;

                if (c == '(' || c == ',' || c == '=' || c == ':' ||
                    c == '[' || c == '!' || c == '&' || c == '|' ||
                    c == '?' || c == '+' || c == '-' || c == '~' ||
                    c == '*' || c == '/' || c == '{' || c == '\n' ||
                    c == ';'
                )
                {
                    retval = 1;
                }

                return retval;

            }
        }
        public static class CS
        {
            public static Action Compile(string code, string namespaceandclass, string mainfunction, string[] ReferencedAssemblies)
            {
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();
                foreach (string Ref in ReferencedAssemblies)
                    parameters.ReferencedAssemblies.Add(Ref);
                parameters.GenerateExecutable = true;
                parameters.GenerateInMemory = true;
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                    }

                    throw new InvalidOperationException(sb.ToString());
                }
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType(namespaceandclass);
                MethodInfo main = program.GetMethod(mainfunction);
                return (Action)Delegate.CreateDelegate(typeof(Action), main);
            }
        }
        public static class VB
        {
            public static Action Compile(string code, string namespaceandclass, string mainfunction, string[] ReferencedAssemblies)
            {
                bool InMem = true;
                bool Exe = true;
                VBCodeProvider provider = new VBCodeProvider();
                CompilerParameters parameters = new CompilerParameters();
                foreach (string Ref in ReferencedAssemblies)
                    parameters.ReferencedAssemblies.Add(Ref);
                parameters.GenerateExecutable = Exe;
                parameters.GenerateInMemory = InMem;
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                    }

                    throw new InvalidOperationException(sb.ToString());
                }
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType(namespaceandclass);
                MethodInfo main = program.GetMethod(mainfunction);
                return (Action)Delegate.CreateDelegate(typeof(Action), main);
            }
        }
    }
}
