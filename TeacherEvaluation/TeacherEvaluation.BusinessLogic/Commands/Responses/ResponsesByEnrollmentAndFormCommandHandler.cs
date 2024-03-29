﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesByEnrollmentAndFormCommandHandler : IRequestHandler<ResponsesByEnrollmentAndFormCommand, IDictionary<string, string>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResponsesByEnrollmentAndFormCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, string>> Handle(ResponsesByEnrollmentAndFormCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.FormId);
            bool enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(e => e.Id == request.EnrollmentId);
            if(formExists && enrollmentExists)
            {
                var freeFormResponses = await unitOfWork.AnswerToQuestionWithTextRepository
                    .GetByEnrollmentAndFormIdAsync(request.EnrollmentId, request.FormId);
                var responsesForLikertQuestions = await unitOfWork.AnswerToQuestionWithOptionRepository
                    .GetByEnrollmentAndFormIdAsync(request.EnrollmentId, request.FormId);

                var questionsAndResponses = new Dictionary<string, string>();
                foreach(var response in freeFormResponses)
                {
                    questionsAndResponses.Add(response.Question.Text, response.FreeFormAnswer);
                }
                foreach(var response in responsesForLikertQuestions)
                {
                    questionsAndResponses.Add(response.Question.Text, response.Score.ToString());
                }
                return questionsAndResponses;
            }

            throw new ItemNotFoundException("Not found");
        }
    }
}
