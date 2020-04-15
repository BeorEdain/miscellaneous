using System;
using System.IO;
using static mapHelper.helpers;

namespace mapBuilder {
    class Program {
        static void Main(string[] args) {
            int[,] testArray = playerMap();
            printToFile(testArray, "x0Test");
            printToFile(arrayAverager(testArray,1), "x1Test");
            printToFile(arrayAverager(testArray,2), "x2Test");
            printToFile(arrayAverager(testArray,4), "x4Test");
            printToFile(arrayAverager(testArray,8), "x8Test");
            printToFile(arrayAverager(testArray,16), "x16Test");
            printToFile(arrayAverager(testArray,24), "x24Test");
            convertArrayToImage(testArray, "x0Test");
            convertArrayToImage(arrayAverager(testArray,1), "x1Test");
            convertArrayToImage(arrayAverager(testArray,2), "x2Test");
            convertArrayToImage(arrayAverager(testArray,4), "x4Test");
            convertArrayToImage(arrayAverager(testArray,8), "x8Test");
            convertArrayToImage(arrayAverager(testArray,16), "x16Test");
            convertArrayToImage(arrayAverager(testArray,24), "x24Test");
        }
    }
}