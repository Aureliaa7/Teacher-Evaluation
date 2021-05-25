using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class EnrollmentIdsCommandHandler : IRequestHandler<EnrollmentIdsCommand, IEnumerable<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;

        public EnrollmentIdsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Guid>> Handle(EnrollmentIdsCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);

            if(formExists && teacherExists)
            {
                var responses = await unitOfWork.AnswerRepository.GetByFormIdAsync(request.FormId);
                var enrollmentIds = responses.Select(r => r.Enrollment.Id).Distinct();
                return enrollmentIds;
            }
            throw new ItemNotFoundException("Not found");
        }
    }
}
