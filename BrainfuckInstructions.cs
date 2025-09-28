using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    public enum BrainfuckInstructionCode
    {
        MoveRight,
        MoveLeft,
        Increment,
        Decrement,
        PrintChar,
        GetChar,
        JumpLeftBracket,
        JumpRightBracket,
        SetToZero,
        MoveRightAdd,
        MoveRightSub
    }

    public class BrainfuckInstruction
    {
        public BrainfuckInstructionCode Code { get; }
        public int Arg { get; }

        public BrainfuckInstruction(BrainfuckInstructionCode code, int arg = 0)
        {
            Code = code;
            Arg = arg;
        }

        public BrainfuckInstruction(char charCode, int arg = 0)
        {
            Code = GetCode(charCode);
            Arg = arg;
        }

        public static BrainfuckInstructionCode GetCode(char code)
        {
            return code switch
            {
                '>' => BrainfuckInstructionCode.MoveRight,
                '<' => BrainfuckInstructionCode.MoveLeft,
                '+' => BrainfuckInstructionCode.Increment,
                '-' => BrainfuckInstructionCode.Decrement,
                '.' => BrainfuckInstructionCode.PrintChar,
                ',' => BrainfuckInstructionCode.GetChar,
                '[' => BrainfuckInstructionCode.JumpLeftBracket,
                ']' => BrainfuckInstructionCode.JumpRightBracket,
                _ => throw new NotImplementedException()
            };
        }
    }
}
