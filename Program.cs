namespace BrainfuckInterpreter
{
    public class Program
    {
        public static void Main()
        {
            string program =
                "++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.";

            var interpreter = new BrainfuckInterpreter(program);
            interpreter.Run();
        }
    }
}