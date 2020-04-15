using System;
using System.IO;
using System.Drawing;

namespace mapHelper {
    class helpers {
        static Random seed = new Random();

        //Integers that can be changed easily
        static int height = 50;
        static int width = 50;

        //Set an output location for the files
        static string path = "outputs\\";

        ///<summary>
        ///Hopefully a more elegant version of playerMap.
        ///Will generate a completely random map then bring down high elements
        ///and raise lower elements to create a more fluid noise map.
        ///</summary>
        public static int[,] playerMap() {
            //The initial map array that will be modified and output
            int[,] mapArray = new int[height, width];

            //Populate the 2D array with random values
            for(int i = 0; i < mapArray.GetLength(0); i++) {
                for(int j = 0; j < mapArray.GetLength(1); j++) {
                    mapArray[i,j] = seed.Next(-255,255);
                }
            }

            //Check to any of the values in the array are blank
            arrayChecker(mapArray);

            return mapArray;
        }

        public static void arrayChecker(int[,] mapArray) {
            //Assume it needs to run again
            bool runAgain = true;

            //A loop that always occurs at least once to do the initial check
            do {
                //Iterate vertically
                for(int i = 0; i < mapArray.GetLength(0); i++) {
                    //Iterate horizontally
                    for(int j = 0; j < mapArray.GetLength(1); j++) {
                        //If the individual data slot is blank
                        if(mapArray[i,j].ToString() == "") {
                            //Change it to a value between -255 and 255
                            mapArray[i,j] = seed.Next(-255,255);
                        }
                        else {
                            runAgain = false;
                        }
                    }
                }
            } while(runAgain);
        }

        ///<summary>
        ///Averages the array that it is given based on all adjacent squares
        ///</summary>
        public static int[,] arrayAverager(int[,] array, int numRuns) {
            int[,] mapArray = array.Clone() as int[,];
            
            for(int a = 0; a < numRuns; a++) {
                //Average the values within the 2D array
                for(int i = 0; i < mapArray.GetLength(0); i++) {
                    for(int j = 0; j < mapArray.GetLength(1); j++) {
                        //If this is the top left corner
                        if(i==0 && j==0) {
                            int thisSquare = mapArray[i,j];
                            int rightOne = mapArray[i,j+1];
                            int rightDownOne = mapArray[i+1,j+1];
                            int downOne = mapArray[i+1,j];

                            int average = (thisSquare + rightOne + rightDownOne
                                           + downOne)/4;

                            mapArray[i,j] = average;
                        }
                        //If this is the bottom left corner
                        else if(i+1==mapArray.GetLength(0) && j==0) {
                            int thisSquare = mapArray[i,j];
                            int upOne = mapArray[i-1,j];
                            int upRightOne = mapArray[i-1,j+1];
                            int rightOne = mapArray[i,j+1];

                            int average = (thisSquare + upOne + upRightOne +
                                           rightOne)/4;

                            mapArray[i,j] = average;
                        }
                        //If this is the top right corner
                        else if(i==0 && j+1==mapArray.GetLength(1)) {
                            int thisSquare = mapArray[i,j];
                            int leftOne = mapArray[i,j-1];
                            int leftDownOne = mapArray[i+1,j-1];
                            int downOne = mapArray[i+1,j];

                            int average = (thisSquare + leftOne + leftDownOne +
                                           downOne)/4;

                            mapArray[i,j] = average;
                        }
                        //If this is the bottom right corner
                        else if(i+1==mapArray.GetLength(0) &&
                                j+1==mapArray.GetLength(1)) {
                            int thisSquare = mapArray[i,j];
                            int leftOne = mapArray[i,j-1];
                            int leftUpOne = mapArray[i-1,j-1];
                            int upOne = mapArray[i-1,j];

                            int average = (thisSquare + leftOne + leftUpOne +
                                           upOne)/4;

                            mapArray[i,j] = average;
                        }
                        //If this is the left-most column
                        else if(i != 0 && j == 0) {
                            int thisSquare = mapArray[i,j];
                            int upOne = mapArray[i-1,j];
                            int upOneRight = mapArray[i-1,j+1];
                            int rightOne = mapArray[i,j+1];
                            int rightDownOne = mapArray[i+1,j+1];
                            int downOne = mapArray[i+1,j];

                            int average = (thisSquare + upOne + upOneRight +
                                           rightOne + rightDownOne + downOne)/6;

                            mapArray[i,j] = average;
                        }
                        //If this is the top row
                        else if(i == 0 && j != 0) {
                            int thisSquare = mapArray[i,j];
                            int leftOne = mapArray[i,j-1];
                            int leftDownOne = mapArray[i+1,j-1];
                            int downOne = mapArray[i+1,j];
                            int downRightOne = mapArray[i+1,j+1];
                            int rightOne = mapArray[i,j+1];

                            int average = (thisSquare + leftOne + leftDownOne +
                                           downOne + downRightOne + rightOne)/6;

                            mapArray[i,j] = average;

                        }
                        //If this is the bottom row
                        else if(i + 1 == mapArray.GetLength(0) &&
                                j + 1 != mapArray.GetLength(1)) {
                            int thisSquare = mapArray[i,j];
                            int leftOne = mapArray[i,j-1];
                            int leftUpOne = mapArray[i-1,j-1];
                            int upOne = mapArray[i-1,j];
                            int upRightOne = mapArray[i-1,j+1];
                            int rightOne = mapArray[i,j+1];

                            int average = (thisSquare + leftOne + leftUpOne +
                                           upOne + upRightOne + rightOne)/6;

                            mapArray[i,j] = average;
                        }
                        //If this is the right-most column
                        else if(i + 1 != mapArray.GetLength(0) &&
                                j + 1 == mapArray.GetLength(1)) {
                            int thisSquare = mapArray[i,j];
                            int upOne = mapArray[i-1,j];
                            int upLeftOne = mapArray[i-1,j-1];
                            int leftOne = mapArray[i,j-1];
                            int leftDownOne = mapArray[i+1,j-1];
                            int downOne = mapArray[i+1,j];

                            int average = (thisSquare + upOne + upLeftOne +
                                           leftOne + leftDownOne + downOne)/6;

                            mapArray[i,j] = average;
                        }
                        //If this is one of the middle squares
                        else {
                            int thisSquare = mapArray[i,j];
                            int upOne = mapArray[i-1,j];
                            int upLeftOne = mapArray[i-1,j-1];
                            int leftone = mapArray[i,j-1];
                            int leftDownOne = mapArray[i+1,j-1];
                            int downOne = mapArray[i+1,j];
                            int downRightOne = mapArray[i+1,j+1];
                            int rightOne = mapArray[i,j+1];
                            int upRightOne = mapArray[i-1,j+1];

                            int average = (thisSquare + upOne + upLeftOne +
                                           leftone + leftDownOne + downOne +
                                           downRightOne + rightOne + upRightOne)
                                           /9;

                            mapArray[i,j] = average;
                        }
                    }
                }
            }
            return mapArray;
        }

        ///<summary>
        ///Simply outputs the 2D array to a csv so it's easy to visualize the
        ///heights numerically.
        ///</summary>
        public static void printToFile(int[,] printArray, string name) {
            if(!Directory.Exists(path + "CSV's")) {
                Directory.CreateDirectory(path + "CSV's");
            }

            FileStream F = new FileStream(path + "CSV's\\" + name + ".csv",
                                          FileMode.Create, FileAccess.Write);
         
            StreamWriter fileWriter = new StreamWriter(F);

            for(int i = 0; i < printArray.GetLength(0); i++) {
                for(int j = 0; j < printArray.GetLength(1); j++) {
                    fileWriter.Write(printArray[i,j] + ",");
                }
                fileWriter.WriteLine();
            }
            F.Close();
        }

        ///<summary>
        ///A method designed to output the contents of an integer array to a
        ///bitmap to get a proper visualization of the height map.
        ///</summary>
        public static void convertArrayToImage(int[,] array, string name) {
            int[,] testArray = array.Clone() as int[,];
            int height = testArray.GetLength(0);
            int width = testArray.GetLength(1);
            int stride = width * 4;

            for(int i = 0; i < testArray.GetLength(0); i++) {
                for(int j = 0; j < testArray.GetLength(1); j++) {
                    byte curVal = (byte)testArray[i,j];
                    byte[] bgra = new byte[] {curVal,curVal,curVal,curVal};
                    testArray[i,j] = BitConverter.ToInt32(bgra, 0);
                }
            }
            Bitmap bitmap;
            unsafe {
                fixed (int* intPtr = &testArray[0,0]) {
                       bitmap = new Bitmap(width, height, stride,
                       System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                       new IntPtr(intPtr));
                }
            }
            if(!Directory.Exists(path + "JPG's")) {
                Directory.CreateDirectory(path + "JPG's");
            }

            bitmap.Save(path  + "JPG's\\" + name + ".jpg");
        }
    }
}