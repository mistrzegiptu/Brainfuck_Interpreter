using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    internal class BrainfuckCompiler : IBrainfuckCompiler
    {
        private readonly char[] _program;

        public BrainfuckCompiler(char[] program)
        {
            _program = program;
        }

        public IEnumerable<BrainfuckInstruction> Compile()
        {
            var compiledProgram = new List<BrainfuckInstruction>();
            
            var i = 0;
            var previousI = 0;
            while(i < _program.Length)
            {
                BrainfuckInstruction? instruction = null;
                switch (_program[i])
                {
                    case '>' or '<' or '+' or '-':
                        instruction = StackMultiple(ref i);
                        break;
                    case '.' or ',':
                        instruction = new BrainfuckInstruction(_program[i]);
                        break;
                    case '[' or ']':
                        instruction = SearchForPattern(ref i);

                        if (instruction is null)
                            instruction = new BrainfuckInstruction(_program[i]);

                        break;
                }

                if(instruction is not null)
                    compiledProgram.Add(instruction);

                if (i == previousI)
                    i++;

                previousI = i;
            }

            ResolveJumps(compiledProgram);

            return compiledProgram;
        }

        private BrainfuckInstruction? StackMultiple(ref int index)
        {
            char currentOperation = _program[index];
            int counter = 0;

            while (_program[index] == currentOperation)
            {
                counter++;
                index++;
            }

            return new BrainfuckInstruction(currentOperation, counter);
        }

        private BrainfuckInstruction? SearchForPattern(ref int i)
        {
            /*PATERNS:
             [-] - SET CELL TO ZERO
             [->+<] - MOVE VALUE RIGHT ADD
             [->-<] - MOVE VALUE RIGHT SUB
             */
            var pattern = _program.Skip(i).Take(4).ToString();
            BrainfuckInstructionCode? code = null;

            if (pattern.Substring(0, 3) == "[-]")
                code = BrainfuckInstructionCode.SetToZero;
            else if (pattern == "[->+<]")
                code = BrainfuckInstructionCode.MoveRightAdd;
            else if (pattern == "[->-<]")
                code = BrainfuckInstructionCode.MoveRightSub;

            return code != null ? new BrainfuckInstruction(code.Value) : null;
        }

        private void ResolveJumps(List<BrainfuckInstruction> compiledProgram)
        {
            var openBrackets = new Stack<int>();

            for (int i = 0; i < compiledProgram.Count; i++)
            {
                if (compiledProgram[i].Code == BrainfuckInstructionCode.JumpLeftBracket)
                {
                    openBrackets.Push(i);
                }
                else if (compiledProgram[i].Code == BrainfuckInstructionCode.JumpRightBracket)
                {
                    if (openBrackets.TryPop(out var leftIndex))
                    {
                        compiledProgram[leftIndex] =
                            new BrainfuckInstruction(BrainfuckInstructionCode.JumpLeftBracket, i);
                        compiledProgram[i] =
                            new BrainfuckInstruction(BrainfuckInstructionCode.JumpRightBracket, leftIndex);
                    }
                    else
                    {
                        throw new Exception("Program is incorrect, too many \"]\"");
                    }
                }
            }

            if (openBrackets.Count != 0)
                throw new Exception("Program is incorrect, too many \"[\"");
        }
    }
}
