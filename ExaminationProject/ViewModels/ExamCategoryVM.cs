﻿using ExaminationProject.Models;

namespace ExaminationProject.ViewModels
{
    public class ExamCategoryVM
    {
        public int SelectedCategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}