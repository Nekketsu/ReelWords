using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;
using System;
using System.Linq;

namespace ReelWords.Services
{
    public class DrawService : IDrawService
    {
        public void Draw(GameState state)
        {
            BeginDraw();

            if (state.IsPlaying)
            {
                DrawTutorial(state.SlotMachine.Slots.Length);
                DrawSlotMachine(state.SlotMachine);

                if (state.IsWordValid == true)
                {
                    DrawWordFound(state.Word, state.WordPoints);
                }
                else if (state.IsWordValid == false)
                {
                    DrawWordNotFound(state.Word);
                }
                else
                {
                    HideWordMessage();
                }

                DrawScore(state.Score);
                DrawInput(state.Input);
            }
            else
            {
                DrawThankYou(state.Score);
            }

            EndDraw();
        }

        private void BeginDraw()
        {
            Console.CursorVisible = false;
        }

        private void EndDraw()
        {
            Console.CursorVisible = true;
        }

        private void DrawTutorial(int length)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Press a letter index between '{Gameplay.Input.FirstLetter}' and '{length}' to select the letter in this position.");
            Console.WriteLine("Press 'Backspace' to remove the last letter");
            Console.WriteLine("Press 'Enter' to confirm the word");
            Console.WriteLine("Press 'Escape' to exit the game");
        }

        private void DrawSlotMachine(SlotMachineState slotMachine)
        {
            DrawText(0, 5, "SLOT MACHINE");
            DrawSlots(6, slotMachine.Slots);
        }

        private void DrawInput(InputState input)
        {
            DrawText(0, 10, "INPUT");
            DrawSlots(11, input.Slots);
        }

        private void DrawWordFound(string word, int points)
        {
            ClearLine(15);

            var foregroundColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(word);
            Console.ForegroundColor = foregroundColor;

            Console.Write(" is a valid word, ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"+{points} points!");
            Console.ForegroundColor = foregroundColor;
        }

        private void DrawWordNotFound(string word)
        {
            ClearLine(15);

            var foregroundColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(word);
            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(" is an invalid word!");
        }

        private void HideWordMessage()
        {
            ClearLine(15);
        }

        private void DrawScore(int score)
        {
            DrawText(0, 17, $"SCORE:\t {score}");
        }

        private void DrawThankYou(int score)
        {
            Console.Clear();
            Console.WriteLine($"Your score is: {score}");
            Console.WriteLine("Thank you for playing!");
        }

        private void DrawText(int left, int top, string text)
        {
            ClearLine(top);

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

        private void DrawSlots(int top, SlotState[] slots)
        {
            var keys = string.Join(' ', slots.Select(slot => slot.Key));
            var letters = string.Join(' ', slots.Select(slot => slot.Letter));
            var points = string.Join(' ', slots.Select(slots => slots.Points));

            DrawText(0, top, $"Indices:\t{keys}");
            DrawText(0, top + 1, $"Letters:\t{letters}");
            DrawText(0, top + 2, $"Points:\t\t{points}");
        }

        private void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }
    }
}
