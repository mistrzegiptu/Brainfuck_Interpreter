using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    internal class BrainfuckInterpreter
    {
        private const int Size = 30000;

        private readonly char[] _array;
        private int _index = 0;
        private readonly IBrainfuckCompiler _compiler;

        public BrainfuckInterpreter(IBrainfuckCompiler compiler)
        {
            _array = new char[Size];
            _compiler = compiler;
        }

        public void Run()
        {
            var instructions = _compiler.Compile().ToList();

            for (int i = 0; i < instructions.Count; i++)
            {
                var action = instructions[i];
                i = ExecuteAction(action, i);
            }
        }

        private int ExecuteAction(BrainfuckInstruction action, int i)
        {
            switch (action.Code)
            {
                case BrainfuckInstructionCode.MoveRight:
                    _index = (_index + action.Arg) % Size;
                    break;
                case BrainfuckInstructionCode.MoveLeft:
                    _index = (_index - action.Arg + Size) % Size;
                    break;
                case BrainfuckInstructionCode.Increment:
                    _array[_index] = (char)((_array[_index] + action.Arg) % 256);
                    break;
                case BrainfuckInstructionCode.Decrement:
                    _array[_index] = (char)((_array[_index] - action.Arg + 256) % 256);
                    break;
                case BrainfuckInstructionCode.PrintChar:
                    Console.Write(_array[_index]);
                    break;
                case BrainfuckInstructionCode.GetChar:
                    var toPlace = Console.ReadKey(intercept: true).KeyChar;
                    _array[_index] = toPlace;
                    break;
                case BrainfuckInstructionCode.JumpLeftBracket:
                    if (_array[_index] == (char)0)
                        i = action.Arg;
                    break;
                case BrainfuckInstructionCode.JumpRightBracket:
                    if (_array[_index] != (char)0)
                        i = action.Arg;
                    break;
                case BrainfuckInstructionCode.SetToZero:
                    _array[_index] = (char)0;
                    break;
                case BrainfuckInstructionCode.MoveRightAdd:
                    var rightIndex = (_index + 1) % Size;
                    _array[_index] = (char)0;
                    _array[rightIndex] = (char)((_array[rightIndex] + action.Arg) % 256);
                    break;
                case BrainfuckInstructionCode.MoveRightSub:
                    rightIndex = (_index + 1) % Size;
                    _array[_index] = (char)0;
                    _array[rightIndex] = (char)((_array[rightIndex] - action.Arg + 256) % 256);
                    break;
            }

            return i;
        }
    }
}
