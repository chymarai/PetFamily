namespace PetFamily.Web;

//public static class DotEnv
//{
//    public static string Load(string filePath, string lineName)
//    {
//        if (!File.Exists(filePath))
//            return;



//        foreach (var line in File.ReadAllLines(filePath))
//        {
//            var parts = line.Split(
//                '=',
//                StringSplitOptions.RemoveEmptyEntries);

//            if (parts.Length != 2)
//                continue;

//            Environment.SetEnvironmentVariable(parts[0], parts[1]);
//        }
//    }
//}
