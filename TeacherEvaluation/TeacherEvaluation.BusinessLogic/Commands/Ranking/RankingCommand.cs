using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.Enums;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Ranking
{
    public class RankingCommand : IRequest<IDictionary<TeacherVm, long>>
    {
        public Guid QuestionId { get; set; }
        public RankingType RankingType { get; set; }
    }
}
