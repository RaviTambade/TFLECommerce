
namespace Transflower.ECommerce.IO;
public class FileManager{

    public static  bool ReadFile(string path)
    {
        bool status=false;
        StreamReader reader=new StreamReader(path);
        try{
            string line=null;
            do{

                line=reader.ReadLine();
                Console.WriteLine(line);
            }
            while(line!=null);
            status=true;
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
        }
        finally{
            reader.Close();
        }
        return status;
    }

    public static bool WriteFile(string path, string content){

        StreamWriter writer=new StreamWriter(path);
        writer.WriteLine(content);
        writer.Close();
        return true;
    }
}