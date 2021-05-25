using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommandHandler : IRequestHandler<ResponsesCommand, IDictionary<string, ResponseVm>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResponsesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, ResponseVm>> Handle(ResponsesCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);
            IDictionary<string, ResponseVm> responsesInfo = new Dictionary<string, ResponseVm>();
            IEnumerable<AnswerToQuestion> filteredResponses = new List<AnswerToQuestion>();

            if (formExists && teacherExists)
            {
                var responses = await unitOfWork.AnswerRepository.GetByFormIdAsync(request.FormId);
                if (request.TaughtSubjectId.Equals("All"))
                {
                    filteredResponses = responses.Where(r => r.Enrollment.TaughtSubject.Teacher.Id == request.TeacherId);
                }
                // if the "Please select" option is selected instead of the subject
                else if (request.TaughtSubjectId.Equals("default"))
                {
                }
                else if (await unitOfWork.TaughtSubjectRepository.ExistsAsync(ts => ts.Id == new Guid(request.TaughtSubjectId)))
                {
                    filteredResponses = responses.Where(r => r.Enrollment.TaughtSubject.Id == new Guid(request.TaughtSubjectId));
                }
                responsesInfo = GetResponsesInfo(filteredResponses);
                return responsesInfo;
            }
            throw new ItemNotFoundException("Not found");
        }

        private IDictionary<string, ResponseVm> GetResponsesInfo(IEnumerable<AnswerToQuestion> responses)
        {
            var enrollmentIds = responses.Select(r => r.Enrollment.Id).Distinct();
            IDictionary<string, ResponseVm> responsesInfo = new Dictionary<string, ResponseVm>();
            int iterator = 1;
            foreach (var enrollmentId in enrollmentIds)
            {
                var response = responses.FirstOrDefault(r => r.Enrollment.Id == enrollmentId);
                int noAttendances = response.Enrollment.NumberOfAttendances;
                int grade = response.Enrollment.Grade.Value ?? 0;
                var responseVm = new ResponseVm
                {
                    EnrollmentId = enrollmentId,
                    NoAttendances = noAttendances,
                    Grade = grade
                };
                responsesInfo.Add(string.Concat("Response ", iterator.ToString()), responseVm);
                iterator++;
            }
            return responsesInfo;
        }
    }
}
