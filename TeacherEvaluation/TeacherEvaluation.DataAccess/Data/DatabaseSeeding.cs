using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Data
{
    public class DatabaseSeeding
    {
        public static void AddDean(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("silviunegrea@gmail.com").Result == null)
            {
                ApplicationUser dean = new ApplicationUser
                {
                    Email = "silviunegrea@gmail.com",
                    FirstName = "Silviu",
                    LastName = "Negrea",
                    FathersInitial = "M.",
                    UserName = "silviunegrea@gmail.com",
                    NormalizedUserName = "SILVIUNEGREA@GMAIL.COM",
                    NormalizedEmail = "SILVIUNEGREA@GMAIL.COM",
                    PIN = "1800202431267",
                    EmailConfirmed = true
                };
                IdentityResult resultDean = userManager.CreateAsync(dean, "Dean_ucv98@").Result;
                if (resultDean.Succeeded)
                {
                    userManager.AddToRoleAsync(dean, "Dean").Wait();
                }
            }
        }

        public static void AddAdministrator(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("alexpop@gmail.com").Result == null)
            {
                ApplicationUser administrator = new ApplicationUser
                {
                    Email = "alexpop@gmail.com",
                    FirstName = "Alexandru",
                    LastName = "Pop",
                    FathersInitial = "R.",
                    UserName = "alexpop@gmail.com",
                    NormalizedUserName = "ALEXPOP@GMAIL.COM",
                    NormalizedEmail = "ALEXPOP@GMAIL.COM",
                    PIN = "1760311787654",
                    EmailConfirmed = true
                };
                IdentityResult resultAdministrator = userManager.CreateAsync(administrator, "Admin_ucv98@").Result;
                if (resultAdministrator.Succeeded)
                {
                    userManager.AddToRoleAsync(administrator, "Administrator").Wait();
                }
            }
        }

        public static void AddRoles(RoleManager<ApplicationRole> roleManager)
        {
            if(roleManager.FindByNameAsync("Administrator").Result == null)
            {
                ApplicationRole administratorRole = new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                };
                IdentityResult resultAdmin = roleManager.CreateAsync(administratorRole).Result;
            }

            if (roleManager.FindByNameAsync("Dean").Result == null)
            {
                ApplicationRole deanRole = new ApplicationRole
                {
                    Name = "Dean",
                    NormalizedName = "DEAN"
                };
                IdentityResult resultDean = roleManager.CreateAsync(deanRole).Result;
            }

            if (roleManager.FindByNameAsync("Student").Result == null)
            {
                ApplicationRole studentRole = new ApplicationRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                };
                IdentityResult resultStudent = roleManager.CreateAsync(studentRole).Result;
            }

            if (roleManager.FindByNameAsync("Teacher").Result == null)
            {
                ApplicationRole teacherRole = new ApplicationRole
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                };
                IdentityResult resultTeacher = roleManager.CreateAsync(teacherRole).Result;
            }
        }

        public static void AddStudyDomainsAndSpecializations(ApplicationDbContext context)
        {
            //study domains for license
            StudyDomain ctiLicense = new StudyDomain { Id = Guid.NewGuid(), Name = "CTI", StudyProgramme = StudyProgramme.License };
            StudyDomain ietLicense = new StudyDomain { Id = Guid.NewGuid(), Name = "IET", StudyProgramme = StudyProgramme.License };
            StudyDomain isiLicense = new StudyDomain { Id = Guid.NewGuid(), Name = "ISI", StudyProgramme = StudyProgramme.License };
            StudyDomain mrbLicense = new StudyDomain { Id = Guid.NewGuid(), Name = "MRB", StudyProgramme = StudyProgramme.License };
          
            // study domains for master
            StudyDomain ctiMaster = new StudyDomain { Id = Guid.NewGuid(), Name = "CTI", StudyProgramme = StudyProgramme.Master};
            StudyDomain ietMaster = new StudyDomain { Id = Guid.NewGuid(), Name = "IET", StudyProgramme = StudyProgramme.Master };
            StudyDomain isiMaster = new StudyDomain { Id = Guid.NewGuid(), Name = "ISI", StudyProgramme = StudyProgramme.Master };
            StudyDomain mrbMaster = new StudyDomain { Id = Guid.NewGuid(), Name = "MRB", StudyProgramme = StudyProgramme.Master };

            // study domains for doctorate
            StudyDomain ctiDoctorate = new StudyDomain { Id = Guid.NewGuid(), Name = "CTI", StudyProgramme = StudyProgramme.Doctorate };
            StudyDomain isiDoctorate = new StudyDomain { Id = Guid.NewGuid(), Name = "ISI", StudyProgramme = StudyProgramme.Doctorate };
            StudyDomain mrbDoctorate = new StudyDomain { Id = Guid.NewGuid(), Name = "MRB", StudyProgramme = StudyProgramme.Doctorate };
            if (!context.StudyDomains.Any())
            {
                context.Set<StudyDomain>().Add(ctiLicense);
                context.Set<StudyDomain>().Add(ietLicense);
                context.Set<StudyDomain>().Add(isiLicense);
                context.Set<StudyDomain>().Add(mrbLicense);

                context.Set<StudyDomain>().Add(ctiMaster);
                context.Set<StudyDomain>().Add(ietMaster);
                context.Set<StudyDomain>().Add(isiMaster);
                context.Set<StudyDomain>().Add(mrbMaster);

                context.Set<StudyDomain>().Add(ctiDoctorate);
                context.Set<StudyDomain>().Add(isiDoctorate);
                context.Set<StudyDomain>().Add(mrbDoctorate);
                context.SaveChanges();
            }

            // specializations for license
            Specialization cseLicense = new Specialization { Id = Guid.NewGuid(), Name = "CSE", StudyDomain = ctiLicense };
            Specialization csrLicense = new Specialization { Id = Guid.NewGuid(), Name = "CSR", StudyDomain = ctiLicense };

            Specialization elaLicense = new Specialization { Id = Guid.NewGuid(), Name = "ELA", StudyDomain = ietLicense };

            Specialization aiaLicense = new Specialization { Id = Guid.NewGuid(), Name = "AIA", StudyDomain = isiLicense };
            Specialization ismLicense = new Specialization { Id = Guid.NewGuid(), Name = "ISM", StudyDomain = isiLicense };

            Specialization mecLicense = new Specialization { Id = Guid.NewGuid(), Name = "MEC", StudyDomain = mrbLicense };
            Specialization robLicense = new Specialization { Id = Guid.NewGuid(), Name = "ROB", StudyDomain = mrbLicense };

            //specializations for master
            Specialization cceMaster = new Specialization { Id = Guid.NewGuid(), Name = "CCE", StudyDomain = ctiMaster };
            Specialization isbMaster = new Specialization { Id = Guid.NewGuid(), Name = "ISB", StudyDomain = ctiMaster };
            Specialization iccMaster = new Specialization { Id = Guid.NewGuid(), Name = "ICC", StudyDomain = ctiMaster };
            Specialization iswMaster = new Specialization { Id = Guid.NewGuid(), Name = "ISW", StudyDomain = ctiMaster };

            Specialization seaMaster = new Specialization { Id = Guid.NewGuid(), Name = "SEA", StudyDomain = ietMaster };

            Specialization ascMaster = new Specialization { Id = Guid.NewGuid(), Name = "ASC", StudyDomain = isiMaster };
            Specialization tiisMaster = new Specialization { Id = Guid.NewGuid(), Name = "TIIS", StudyDomain = isiMaster };

            Specialization scrMaster = new Specialization { Id = Guid.NewGuid(), Name = "SCR", StudyDomain = mrbMaster };

            //specializations for doctorate
            Specialization ctiDoctorateSpecialization = new Specialization { Id = Guid.NewGuid(), Name = "CTI", StudyDomain = ctiDoctorate };
            Specialization isiDoctorateSpecialization = new Specialization { Id = Guid.NewGuid(), Name = "ISI", StudyDomain = isiDoctorate };
            Specialization mrbDoctorateSpecialization = new Specialization { Id = Guid.NewGuid(), Name = "MRB", StudyDomain = mrbDoctorate };

            if (!context.Specializations.Any())
            {
                context.Set<Specialization>().Add(cseLicense);
                context.Set<Specialization>().Add(csrLicense);
                context.Set<Specialization>().Add(elaLicense);
                context.Set<Specialization>().Add(aiaLicense);
                context.Set<Specialization>().Add(ismLicense);
                context.Set<Specialization>().Add(mecLicense);
                context.Set<Specialization>().Add(robLicense);

                context.Set<Specialization>().Add(cceMaster);
                context.Set<Specialization>().Add(isbMaster);
                context.Set<Specialization>().Add(iccMaster);
                context.Set<Specialization>().Add(iswMaster);
                context.Set<Specialization>().Add(seaMaster);
                context.Set<Specialization>().Add(ascMaster);
                context.Set<Specialization>().Add(tiisMaster);
                context.Set<Specialization>().Add(scrMaster);

                context.Set<Specialization>().Add(ctiDoctorateSpecialization);
                context.Set<Specialization>().Add(isiDoctorateSpecialization);
                context.Set<Specialization>().Add(mrbDoctorateSpecialization);
                context.SaveChanges();
            }
        }
    }
}

