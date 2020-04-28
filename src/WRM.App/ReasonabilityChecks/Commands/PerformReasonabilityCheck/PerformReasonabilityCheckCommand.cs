using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WRM.Domain.Entities;

namespace WRM.App.ReasonabilityChecks.Commands.PerformReasonabilityCheck
{
    public class PerformReasonabilityCheckCommand : IRequest<bool>
    {
        public PspMeasurement Measurement { get; set; }
    }

    //public class PerformReasonabilityCheckCommandHandler : IRequestHandler<PerformReasonabilityCheckCommand, bool>
    //{
    //    private readonly IdentityInit _identityInit;

    //    public PerformReasonabilityCheckCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityInit identityInit)
    //    {
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //        _identityInit = identityInit;
    //    }

    //    public async Task<bool> Handle(PerformReasonabilityCheckCommand request, CancellationToken cancellationToken)
    //    {
    //        // seed roles
    //        await SeedUserRoles(_roleManager);
    //        // seed admin user
    //        await SeedAdminUser(_userManager);
    //        // seed guest user
    //        await SeedGuestUser(_userManager);
    //        return true;
    //    }

    //}
}
