﻿using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic
{
    public static class Constants
    {
        public const int NumberOfLikertQuestions = 10;
        public const int NumberOfFreeFormQuestions = 2;
        public const int TotalNumberOfQuestions = NumberOfLikertQuestions + NumberOfFreeFormQuestions;
        public const int NumberOfTopTeachers = 10;
        public static readonly IList<string> ExcelExtensions = new List<string>
        {
            ".xls", ".xlt", ".xlm", ".xlsx", ".xlsm", ".xltx", ".xltm", ".xlsb", ".xla", ".xlam", ".xll", ".xlw" 
        };
        public const int MaxNumberOfTagsInWordCloud = 150;
        public const int MinNumberOfDaysToEvaluateTeachers = 2;
    }
}
