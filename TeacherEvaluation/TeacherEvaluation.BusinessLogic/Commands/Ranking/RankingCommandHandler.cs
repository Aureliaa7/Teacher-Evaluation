using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Enums;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.Extensions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Ranking
{
    public class RankingCommandHandler : IRequestHandler<RankingCommand, IDictionary<TeacherVm, double>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RankingCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<TeacherVm, double>> Handle(RankingCommand request, CancellationToken cancellationToken)
        {
            bool questionExists = await unitOfWork.QuestionRepository.ExistsAsync(q => q.Id == request.QuestionId);
            if(questionExists)
            {
                IDictionary<TeacherVm, double> topTeachers = await GetTopTeachersAsync(request.QuestionId, request.RankingType);
                return topTeachers;
            }
            throw new ItemNotFoundException("The question was not found...");
        }

        private async Task<IDictionary<TeacherVm, double>> GetTopTeachersAsync(Guid questionId, RankingType rankingType)
        {
            IEnumerable<Teacher> teachers = await GetTeachersForRankingTypeAsync(rankingType);
            IDictionary<Guid, double> result = new Dictionary<Guid, double>();

            foreach (var teacher in teachers)
            {
                var responses = await unitOfWork.AnswerToQuestionWithOptionRepository
                    .GetByQuestionIdAndTeacherIdAsync(questionId, teacher.Id);
                
                if(responses.Any())
                {
                    long sum = responses.Sum(r => (long)r.Score);
                    double score = sum / responses.Count();
                    result.Add(teacher.Id, score);
                }
            }
            var orderedTeachers = (result.OrderByDescending(r => r.Value)).ToList();
            var top = orderedTeachers.Take(Constants.NumberOfTopTeachers);

            IDictionary<TeacherVm, double> topTeachers = new Dictionary<TeacherVm, double>();
            foreach (var topTeacher in top)
            {
                var teacherVm = await GetTeacherVmAsync(topTeacher.Key);
                topTeachers.Add(teacherVm, topTeacher.Value);
            }

            return topTeachers;
        }

        private async Task<IEnumerable<Teacher>> GetTeachersForRankingTypeAsync(RankingType rankingType)
        {
            IEnumerable<Teacher> teachers = new List<Teacher>();

            switch (rankingType)
            {
                case RankingType.All:
                    {
                        teachers = await unitOfWork.TeacherRepository.GetAllAsync();
                        break;
                    }
                case RankingType.Courses:
                    {
                        var taughtSubjects = (await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteriaAsync(
                            ts => ts.Type == TaughtSubjectType.Course)).DistinctBy(ts => ts.Teacher.Id);
                        teachers = taughtSubjects.Select(ts => ts.Teacher);
                        break;
                    }
                case RankingType.Laboratories:
                    {
                        var taughtSubjects = (await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteriaAsync(
                            ts => ts.Type == TaughtSubjectType.Laboratory)).DistinctBy(ts => ts.Teacher.Id);
                        teachers = taughtSubjects.Select(ts => ts.Teacher);
                        break;
                    }
            }
            return teachers;
        }

        private async Task<TeacherVm> GetTeacherVmAsync(Guid teacherId)
        {
            var teacher = await unitOfWork.TeacherRepository.GetTeacherAsync(teacherId);
            var teacherVm = new TeacherVm
            {
                Name = string.Concat(teacher.User.LastName, " ", teacher.User.FathersInitial, " ",
                        teacher.User.FirstName),
                Department = Enum.GetName(typeof(Department), teacher.Department),  
                Degree = teacher.Degree
            };
            return teacherVm;
        }
    }
}
