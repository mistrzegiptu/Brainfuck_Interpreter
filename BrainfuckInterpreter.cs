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

        private readonly string _program;
        private readonly char[] _array;
        private int _index = 0;
        private readonly Dictionary<int, int> _jumps = new();
        public BrainfuckInterpreter(string program)
        {
            _program = program;
            _array = new char[Size];

            PrecomputeJumps();
        }

        private void PrecomputeJumps()
        {
            var openBrackets = new Stack<int>();

            for (int i = 0; i < _program.Length; i++)
            {
                if (_program[i] == '[')
                {
                    openBrackets.Push(i);
                }
                else if (_program[i] == ']')
                {
                    if (openBrackets.TryPop(out var leftIndex))
                    {
                        _jumps.Add(leftIndex, i);
                        _jumps.Add(i, leftIndex);
                    }
                    else
                    {
                        throw new Exception("Program is incorrect, too many \"]\"");
                    }
                }
            }

            if(openBrackets.Count != 0)
                throw new Exception("Program is incorrect, too many \"[\"");
        }

        public void Run()
        {
            for (int i = 0; i < _program.Length; i++)
            {
                i = ExecuteAction(_program[i], i);
            }
        }

        private int ExecuteAction(char action, int i)
        {
            switch (_program[i])
            {
                case '>':
                    _index = (_index + 1) % Size;
                    break;
                case '<':
                    _index = (_index - 1 + Size) % Size;
                    break;
                case '+':
                    _array[_index] = (char)((_array[_index] + 1) % 256);
                    break;
                case '-':
                    _array[_index] = (char)((_array[_index] - 1 + 256) % 256);
                    break;
                case '.':
                    Console.Write(_array[_index]);
                    break;
                case ',':
                    var toPlace = Console.ReadKey(intercept: true).KeyChar;
                    _array[_index] = toPlace;
                    break;
                case '[':
                    if (_array[_index] == (char)0)
                        i = _jumps[i];
                    break;
                case ']':
                    if (_array[_index] != (char)0)
                        i = _jumps[i];
                    break;
            }

            return i;
        }
    }
}
