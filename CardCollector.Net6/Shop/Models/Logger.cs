/*namespace Shop.Models
{
    public class Logger :LogBase
    {
        private string CurrentDirectory
        {
            get; set;
        }

        private string FileName
        {
            get; set;  
        }

        private string FilePath
        {
            get; set;
        }
    }
    public Logger()
    {
        this.CurrentDirectory = Directory.GetCurrentDirectory();
        this.FileName = "Log.txt";
        this.FilePath = this.CurrentDirectory + "/" + this.Filename;
    }

    public override void Log(string Message)
    {
        using(System.IO.StreamWriter w = System.IO.File.AppendText(this.Filepath))
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", Messsage);
            w.WriteLine("-----------------------------------------------");
        }
    }
}
*/