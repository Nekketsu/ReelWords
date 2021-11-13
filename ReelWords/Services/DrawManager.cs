using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;
using System;
using System.Linq;

namespace ReelWords.Services
{
    public class DrawManager : IDrawManager
    {
        public void Draw(GameState state)
        {
            BeginDraw();

            if (state.IsPlaying)
            {
                DrawTutorial(state.SlotMachineState.Letters.Length);
                DrawSlotMachine(state.SlotMachineState.Letters, state.SlotMachineState.Points);

                if (state.IsWordValid == true)
                {
                    DrawWordFound(state.Word, state.WordPoints);
                }
                else if (state.IsWordValid == false)
                {
                    DrawWordNotFound(state.Word);
                }

                DrawScore(state.Score);
                DrawInput(state.InputState.Indices, state.InputState.Letters);
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

        private void DrawSlotMachine(char[] letters, int[] points)
        {
            var indices = string.Join(' ', Enumerable.Range(1, letters.Length));
            var slots = string.Join(' ', letters);
            var pointsText = string.Join(' ', points);

            DrawText(0, 5, "SLOT MACHINE");
            DrawText(0, 6, $"Indices:\t{indices}");
            DrawText(0, 7, $"Slots:\t\t{slots}");
            DrawText(0, 8, $"Points:\t\t{pointsText}");
        }

        private void DrawInput(int[] indices, char[] letters)
        {
            var indicesText = string.Join(' ', indices.Select(index => index + 1));
            var input = string.Join(' ', letters);

            DrawText(0, 10, "INPUT");
            DrawText(0, 11, $"Indices:\t{indicesText}");
            DrawText(0, 12, $"Input:\t\t{input}");
        }

        private void DrawWordFound(string word, int points)
        {
            ClearLine(14);

            Console.Write($"\"{word}\" is a valid word, ");

            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"+{points} points!");
            Console.ForegroundColor = foregroundColor;
        }

        private void DrawWordNotFound(string word)
        {
            ClearLine(14);

            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            DrawText(0, 14, $"\"{word}\" is an invalid word!");
            Console.ForegroundColor = foregroundColor;
        }

        private void DrawScore(int score)
        {
            DrawText(0, 16, $"SCORE:\t {score}");
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

        private void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }
    }
}
