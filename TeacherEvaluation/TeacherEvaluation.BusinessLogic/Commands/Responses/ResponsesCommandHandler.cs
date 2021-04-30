using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommandHandler : IRequestHandler<ResponsesCommand, IDictionary<string, Guid>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResponsesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, Guid>> Handle(ResponsesCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.Exists(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            if (formExists && teacherExists)
            {
                var responses = await unitOfWork.AnswerRepository.GetByFormIdAsync(request.FormId);
                if (request.TaughtSubjectId.Equals("All"))
                {
                    return GetResponsesOverall(request.TeacherId, responses);
                }
                else if (await unitOfWork.TaughtSubjectRepository.Exists(ts => ts.Id == new Guid(request.TaughtSubjectId)))
                {
                    return GetResponsesForTaughtSubject(new Guid(request.TaughtSubjectId), responses);
                }
            }
            throw new ItemNotFoundException("Not found");
        }

        private IDictionary<string, Guid> GetResponsesOverall(Guid teacherId, IEnumerable<AnswerToQuestion> responses)
        {
            var filteredResponses = responses.Where(r => r.Enrollment.TaughtSubject.Teacher.Id == teacherId);
            var enrollmentIds = filteredResponses.Select(r => r.Enrollment.Id).Distinct();

            IDictionary<string, Guid> enrollmentIdsInfo = new Dictionary<string, Guid>();
            int iterator = 1;
            foreach (var enrollmentId in enrollmentIds)
            {
                enrollmentIdsInfo.Add(string.Concat("Response ", iterator.ToString()), enrollmentId);
                iterator++;
            }

            return enrollmentIdsInfo;
        }

        private IDictionary<string, Guid> GetResponsesForTaughtSubject(Guid taughtSubjectId, IEnumerable<AnswerToQuestion> responses)
        {
            IDictionary<string, Guid> enrollmentIdsInfo = new Dictionary<string, Guid>();

            var filteredResponses = responses.Where(r => r.Enrollment.TaughtSubject.Id == taughtSubjectId);
            var enrollmentIds = filteredResponses.Select(r => r.Enrollment.Id).Distinct();

            int iterator = 1;
            foreach (var enrollmentId in enrollmentIds)
            {
                enrollmentIdsInfo.Add(string.Concat("Response ", iterator.ToString()), enrollmentId);
                iterator++;
            }

            return enrollmentIdsInfo;
        }
    }
}
