namespace BrainfuckInterpreter
{
    public class Program
    {
        public static void Main()
        {
            string program =
                "++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.";

            var interpreter = new BrainfuckInterpreter(new BrainfuckCompiler(program.ToCharArray()));
            interpreter.Run();
        }
    }
}