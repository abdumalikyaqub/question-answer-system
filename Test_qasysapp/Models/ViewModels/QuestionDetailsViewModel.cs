namespace Test_qasysapp.Models.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public Question Question { get; set; } = new Question();
        public Answer Answer { get; set; } = new Answer(); 
        public List<Answer> AnswersList { get; set; } = new List<Answer>();
        public Vote Vote { get; set; } = new Vote();
    }
}
