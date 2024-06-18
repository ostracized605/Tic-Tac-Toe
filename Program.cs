using System;
namespace TicTacToeGame{
    internal class TTT{
        static void Main(){
            int n=3;
            while(true){
                char[,] x_o=new char[n,n];
                FillArray(ref x_o,n);
                PrintArray(x_o,n);
            
                bool isX=false,isO=false;
                ChooseASide(ref isX,ref isO);
                
                System.Console.WriteLine();
            
                Playing(ref x_o,isX,isO,n);
                Console.WriteLine("\nTry again?");
                string restartStr=Console.ReadLine();
                string restartLow=restartStr.ToLower();
                if(restartLow!="yes"){
                    break;
                }
            }
            Console.ReadKey();

        }

        static void FillArray(ref char[,] array,int n){
            for (int i=0;i<n;i++){
                for (int j=0;j<n;j++){
                    array[i,j]='*';
                }
            }
        }

        static void PrintArray(char[,] array,int n){
            for (int i=0;i<n;i++){
                for (int j=0;j<n;j++){
                    if(j==0){
                        Console.Write($"\n|{array[i,j]}|");
                    }
                    else{
                        Console.Write($"{array[i,j]}|");
                    }
                }
            }
        }

        static void ChooseASide(ref bool isX,ref bool isO){
            System.Console.WriteLine($"\nChoose the side\nPress 'X' or 'O' ");
            ConsoleKeyInfo charInfo;
            charInfo=Console.ReadKey();
            if(charInfo.KeyChar=='x'){
                isX=true;
            }
            else if(charInfo.KeyChar=='o'){
                isO=true;
            }
            else{
                ChooseASide(ref isX,ref isO);
            }
        }

        static bool CheckInteger(string[] inpStr,string startingStr){
            short x=0;
            if(startingStr.Contains(' '))
                if (inpStr[0]!=string.Empty){
                    if (inpStr[1]!=string.Empty){
                        if(short.TryParse(inpStr[0],out x)&&short.TryParse(inpStr[1],out x)){
                        return true;
                        }
                    }
                }
                
            return false;
        }

        static void Xside(ref char[,] array,short row,short col, int n){
            while(true){
                System.Console.WriteLine("Where do you want to put your X?");
                string inputStr=Console.ReadLine();
                string[] splitInput=inputStr.Split(' ');
                if(CheckInteger(splitInput,inputStr)){
                    row=Convert.ToInt16(splitInput[0]);
                    col=Convert.ToInt16(splitInput[1]);
                    if (row>0&&row<(n+1)&&col>0&&col<(n+1)){
                        if(CheckIfOccupied(array,row,col)==true){
                            break;
                        }
                        else{
                            System.Console.WriteLine($"The place is already occupied, please enter two integers between 1 and {n} with a space between them.");
                        }
                    }else{
                        System.Console.WriteLine($"Please enter two integers between 1 and {n} with a space between them.");
                    }
                }
            }
            array[row-1,col-1]='X';
        }

        static void Oside(ref char[,] array,short row,short col, int n){
            while(true){
                System.Console.WriteLine("Where do you want to put your O?");
                string inputStr=Console.ReadLine();
                string[] splitInput=inputStr.Split(' ');
                if(CheckInteger(splitInput,inputStr)){
                    row=Convert.ToInt16(splitInput[0]);
                    col=Convert.ToInt16(splitInput[1]);
                    if (row>0&&row<(n+1)&&col>0&&col<(n+1)){
                        if (CheckIfOccupied(array,row,col)==true){
                            break;
                        }
                        else {
                            System.Console.WriteLine($"The place is already occupied, please enter two integers between 1 and {n} with a space between them.");
                        }
                    }
                    else{
                        System.Console.WriteLine($"Please enter two integers between 1 and {n} with a space between them.");
                    }
                }
            }
            array[row-1,col-1]='O';
        }

        static void AIturn(ref char[,] array,bool isO,bool isX, int n){
            Random r=new Random();
            short rand_row=0, rand_col=0;
            
            if (isX){
                while(true){
                    rand_row=Convert.ToInt16(r.Next(1,n+1));
                    rand_col=Convert.ToInt16(r.Next(1,n+1));
                    if(CheckIfOccupied(array,rand_row,rand_col)){
                        break;
                    }
                }
                array[rand_row-1,rand_col-1]='O';
            }
            else if(isO){
                while(true){
                    rand_row=Convert.ToInt16(r.Next(1,n+1));
                    rand_col=Convert.ToInt16(r.Next(1,n+1));
                    if(CheckIfOccupied(array,rand_row,rand_col)){
                        break;
                    }
                }
                array[rand_row-1,rand_col-1]='X';
            }
        }

        static bool CheckIfOccupied(char[,] array,short row,short col){
            if (array[row-1,col-1]=='X'||array[row-1,col-1]=='O'){
                return false;
            }
            else{
                return true;
            }
        }

        static bool Playing(ref char[,] array,bool isX,bool isO,int n){
            bool finished=false;
            while(!finished){
                short inpChar_row=0;
                short inpChar_col=0;
                if (isX){
                    Xside(ref array,inpChar_row,inpChar_col,n);
                    PrintArray(array,n);
                    if(GameEnd(array,n)==true){
                        return true;
                    }
                    System.Console.WriteLine("\nAI's turn...");
                    string dot=".";
                    for(int i=0;i<3;i++){
                        System.Console.WriteLine(dot);
                        dot=dot+".";
                        Thread.Sleep(500);
                    }
                    AIturn(ref array,isO,isX,n);
                    PrintArray(array,n);
                    if(GameEnd(array,n)==true){
                        return true;
                    }
                } 
                            
                else if (isO){
                    System.Console.WriteLine("\nAI's turn...");
                    string dot=".";
                    for(int i=0;i<3;i++){
                        System.Console.WriteLine(dot);
                        dot=dot+".";
                        Thread.Sleep(500);
                    }
                    AIturn(ref array,isO,isX,n);
                    PrintArray(array,n);
                    if(GameEnd(array,n)==true){
                        return true;
                    }
                    Oside(ref array,inpChar_row,inpChar_col,n);
                    PrintArray(array,n);
                    if(GameEnd(array,n)==true){
                        return true;
                    }
                }

                System.Console.WriteLine();
            }
            return true;
        }

        static bool Tie(char[,] array,int n){
            short count=0;
            for (int i=0;i<n;i++){
                for (int j=0;j<n;j++){
                    if (array[i,j]!='*'){
                        count++;
                        if(count==n*n){
                            System.Console.WriteLine("\nTie!");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool GameEnd(char[,] array,int n){
            int numX=0, numO=0;
            
            //check horizontal and vertical lines
            for (short i=0;i<n;i++){
                if(CheckLine(array,i,0,0,1,n)||CheckLine(array,0,i,1,0,n)){
                    return true;
                }
            }

            for (short i=0;i<n;i++){
                if(CheckLine(array,0,0,1,1,n)||CheckLine(array,0,Convert.ToInt16(n-1),1,-1,n)){
                    return true;
                }
            }

            if(Tie(array,n)==true){
                return true;
            }

            return false;
        }

        static bool CheckLine(char[,] array, short startRow,short startCol, short stepX, short stepY, int n){
            char symb=array[startRow,startCol];
            if (symb=='*'){
                return false;
            }
            for (int i=1;i<n;i++){
                if (array[startRow+i*stepX,startCol+i*stepY]!=symb){
                    return false;
                }
            }
            System.Console.WriteLine($"\n{symb} side won!");
            return true;
        }

    }
}