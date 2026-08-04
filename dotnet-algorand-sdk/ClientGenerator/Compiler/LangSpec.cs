using System.IO;
using System.Reflection;
using Newtonsoft.Json;


namespace AVM.ClientGenerator.Compiler
{
    internal class LangSpec
    {
        
        public int EvalMaxVersion;
        public int LogicSigVersion;
        public Operation[] Ops;
        
        public static LangSpec Instance {  get; private set; }
    
        
        private static string json;

        static  LangSpec()
        {
            json = GetFromResources("langspec.json");

            Instance=JsonConvert.DeserializeObject<LangSpec>(json);

        }





        private static string GetFromResources(string resourceName)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            using (Stream stream = a.GetManifestResourceStream(a.GetName().Name + '.' + resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }



    }

}
