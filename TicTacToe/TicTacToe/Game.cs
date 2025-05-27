namespace TicTacToe;

public class Game
{
    private const string X = "X";
    private const string O = "O";
    string[,] field = {
        { "1", "2", "3" },
        { "4", "5", "6" },
        { "7", "8", "9" } 
    };
       int moveCount = 0;
       bool gameEnded = false;
       string currentPlayer = X;
       int lastMoveRow = 0;
       int lastMoveColumn = 0;
       int firstCellNumberOfField = 1;
       int lastCellNumberOfField = 9;

       /// <summary>
       /// Процесс игры
       /// </summary>
       internal void Play()
       {
           while (!gameEnded)
           {
               Move();
               DrawField(field);
           }
       } 
       
       
       /// <summary>
       /// Выполнение хода игрока
       /// </summary>
       void Move(){
        int number = 0;
        while (!gameEnded)
        {
            Console.WriteLine($"Игрок {currentPlayer}, ваш ход. Введите номер ячейки от 1 до 9."); 
            string enteredValue = Console.ReadLine();
            if (int.TryParse(enteredValue, out number))
            {
                if (number >= firstCellNumberOfField && number <= lastCellNumberOfField)
                    break;
            } Console.WriteLine($"Номер ячейки указан неверно, необходимо ввести число от {firstCellNumberOfField} до {lastCellNumberOfField}");
        }
        string searchCell = $"{number}";
        
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
                        currentPlayer = (currentPlayer == X) ? O : X;
                    }
                }
            } 
       } 
        /// <summary>
        /// Отрисовка игрового поля с ходами игроков
        /// </summary>
        /// <param name="field">Игровое поле</param>
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

        /// <summary>
        /// Проверка победы
        /// </summary>
       void CheckWin()
       {
           for (int i = 0; i < 3; i++)
           {
               if (field[i, 0] == currentPlayer && field[i, 1] == currentPlayer && field[i, 2] == currentPlayer)
               {
                   Console.WriteLine($"Игрок {currentPlayer} победил");
                   gameEnded = true;
               }
           }
           
           for (int j = 0; j < 3; j++)
           {
               if (field[0, j] == currentPlayer && field[1, j] == currentPlayer && field[2, j] == currentPlayer)
               {
                   Console.WriteLine($"Игрок {currentPlayer} победил");
                   gameEnded = true;
               }
           }
 
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
        /// <summary>
        /// Проверка ничьи
        /// </summary>
       void CheckDrow()
       {
           if (!gameEnded)
           { if (moveCount == 9) 
               { 
                   Console.WriteLine($"Ничья!"); 
                   gameEnded = true;
               }
           }
       }
}