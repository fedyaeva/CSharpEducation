namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {
        string[,] field = {
            { "1", "2", "3" },
            { "4", "5", "6" },
            { "7", "8", "9" }
        };
       int moveCount = 0;
       bool gameEnded = false;
       string currentPlayer = "X";
       int lastMoveRow = 0;
       int lastMoveColumn = 0;
       while (!gameEnded){
        Move();
        DrawField(field);
       }
        void Move(){
        //Ввод ячейки
        int number;
        while (true)
        {
            Console.WriteLine($"Игрок {currentPlayer}, ваш ход. Введите номер ячейки от 1 до 9."); 
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out number))
            {
                if (number >= 1 && number <= 9)
                    break;
            } Console.WriteLine("Номер ячейки указан некорректно");
        }
        string searchCell = $"{number}";
        
        //Поиск ячейки и выполнение хода
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                { 
                    if (field[i, j] == searchCell)
                    {
                        lastMoveRow = i;
                        lastMoveColumn = j;
                        field[i, j] = currentPlayer;
                        moveCount++;
                        Console.WriteLine($"Ход № {moveCount} выполнен.");
                        CheckWin();
                        CheckDrow();
                        currentPlayer = (currentPlayer == "X") ? "O" : "X";
                    }
                }
            } 
        } 
        
        void DrawField(string[,] field)
        {
            int rows = field.GetLength(0);
            int columns = field.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (lastMoveRow == i && lastMoveColumn == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" {field[i, j]} ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    { 
                        Console.Write($" {field[i, j]} ");  
                    }
                    if (j < columns - 1)
                        Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        void CheckWin()
        {
            // Провека строк
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == currentPlayer && field[i, 1] == currentPlayer && field[i, 2] == currentPlayer)
                {
                    Console.WriteLine($"Игрок {currentPlayer} победил");
                    gameEnded = true;
                }
            }

            // Проверка стлбцов
            for (int j = 0; j < 3; j++)
            {
                if (field[0, j] == currentPlayer && field[1, j] == currentPlayer && field[2, j] == currentPlayer)
                {
                    Console.WriteLine($"Игрок {currentPlayer} победил");
                    gameEnded = true;
                }
            }
            // Проверка диагоналей
            if (field[0, 0] == currentPlayer && field[1, 1] == currentPlayer && field[2, 2] == currentPlayer)
            { 
                Console.WriteLine($"Игрок {currentPlayer} победил"); 
                gameEnded = true; 
            }

            if (field[0, 2] == currentPlayer && field[1, 1] == currentPlayer && field[2, 0] == currentPlayer)
            {
                Console.WriteLine($"Игрок {currentPlayer} победил");
                gameEnded = true;    
            }
        }
        void CheckDrow()
        {
           if (moveCount == 9) {
               Console.WriteLine($"Ничья!"); 
               gameEnded = true; 
           }
        }
}
}