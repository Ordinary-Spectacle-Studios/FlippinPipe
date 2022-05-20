using System;

namespace FlippinPipe
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new FlippinPipeEngine())
                game.Run();
        }
    }
}
