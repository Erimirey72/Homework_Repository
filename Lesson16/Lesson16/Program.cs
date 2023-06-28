using System.Reflection;

namespace AssemblyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyPath = "Lesson16.dll";

            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    Console.WriteLine("Class: " + type.Name);

                    MethodInfo[] methods = type.GetMethods();

                    foreach (MethodInfo method in methods)
                    {
                        Console.WriteLine("  Method: " + method.Name);

                        ParameterInfo[] parameters = method.GetParameters();
                        Console.WriteLine("    Parameters:");

                        foreach (ParameterInfo parameter in parameters)
                        {
                            Console.WriteLine("      Name: " + parameter.Name);
                            Console.WriteLine("      Type: " + parameter.ParameterType.Name);
                        }

                        Console.WriteLine("    Return Type: " + method.ReturnType.Name);
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading assembly: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}