using Model.Base;
using Model.Common;
using Model.CountryInfo;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using TekTak.iLoop.Profile;

namespace TekTak.iLoop.Sealed.Profile
{
    public sealed class ProfilePersonalRepositorySealed : ProfilePersonalRepository, IProfilePersonalRepositorySealed
    {
        public ProfilePersonalRepositorySealed(Services client)
            : base(client)
        { }

        public async Task<BasicInformationWeb> GetBasicInformationWeb(int userId, string targetUser, SystemSession session)
        {
            var serviceResponse = await Task.Factory.StartNew(() => Client.UserService.getProfile(userId, 0, targetUser, session.GetSession())).ConfigureAwait(false);

            //TODO: Change implementation once privacy setting for DOB year is implemented.
            var dob = Convert.ToDateTime(serviceResponse.UserInfoPerson.BirthDate);
            var response = new BasicInformationWeb
                {
                    UserId = Convert.ToInt32(serviceResponse.User.UserId),
                    UserName = serviceResponse.User.UserName,
                    UserTypeId = (SystemUserType)serviceResponse.UserInfo.UserTypeId,
                    Email = serviceResponse.User.Email,
                    FirstName = serviceResponse.UserInfoPerson.FirstName,
                    LastName = serviceResponse.UserInfoPerson.LastName,
                    FriendShipStatus = (SystemFriendshipStatus)serviceResponse.FriendshipStatus,
                    PrimaryContactNumber = serviceResponse.User.PrimaryContactNumber,
                    Image = serviceResponse.UserInfo.Picture,
                    AvailableStatus = (SystemUserAvailabilityCode)Convert.ToByte(serviceResponse.User.ChatStatus),
                    Title = serviceResponse.UserInfoPerson.Title,
                    Gender = (SystemGender)Convert.ToByte(serviceResponse.UserInfoPerson.Gender),
                    RelationshipStatusId = (SystemRelationshipStatus)Convert.ToByte(serviceResponse.UserInfoPerson.RelationshipStatusId.RelationshipStatusId),
                    DateOfBirthDayAndMonth = new DateTime(DateTime.UtcNow.Year, dob.Month, dob.Day),
                    DateOfBirthYear = dob.Year,
                    Nationality = serviceResponse.UserInfoPerson.Nationality == null ? null : new GeneralKvPair<int, string>
                    {
                        Id = serviceResponse.UserInfoPerson.Nationality.NationalityId,
                        Value = serviceResponse.UserInfoPerson.Nationality.Name
                    },
                    Religion = serviceResponse.UserInfoPerson.ReligionId == null ? null : new GeneralKvPair<int, string>
                    {
                        Id = Convert.ToInt32(serviceResponse.UserInfoPerson.ReligionId.ReligionId),
                        Value = serviceResponse.UserInfoPerson.ReligionId.Name
                    },
                    Interests = serviceResponse.UserInfoPerson.Interests

                };
            return response;
        }

        public async Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonWebRequest request, SystemSession session)
        {
            var serviceRequest = new UserInfoPerson
            {
                UserId = session.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate.ToString(),
                ReligionId = new Religion { Name = request.Religion },
                Nationality = new Nationality { Name = request.Nationality },
                Interests = request.Interests,
                Title = request.Title,
                RelationshipStatusId = new RelationshipStatus { RelationshipStatusId = (int)request.RelationshipStatusId },
                Gender = ((byte)request.Gender).ToString()
            };

            var response =
                (await
                    Task.Factory.StartNew(
                        () => Client.UserService.setBasicContactPersion(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<IEnumerable<EducationViewResponse>> GetEducationHistory(string targetUser, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getUserAcademics(targetUser, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new EducationViewResponse
            {
                DisplayOrderId = x.DisplayOrderId,
                AcademicInstitute = new GeneralKvPair<int, string>
                {
                    Id = x.AcademicId,
                    Value = x.AcademicInstituteName
                },
                AcademicInstituteId = x.AcademicInstituteId,
                Location = new UserCity
                {
                    Id = x.City.CityId,
                    Name = x.City.Name,
                    CountryCode = x.Country.CountryCode,
                    CountryName = x.Country.Name
                },
                JoinedYear = Convert.ToInt32(x.JoinedYear),
                HasGraduated = x.HasGraduated,
                GraduatedYear = x.GraduatedYear != null ? Convert.ToInt32(x.GraduatedYear) : (int?)null,
                Degree = new GeneralKvPair<int, string>
                {
                    Id = x.Degree.DegreeTypeId,
                    Value = x.Degree.DegreeTypeName
                },
                Concentration = new GeneralKvPair<int, string>
                {
                    Id = x.Concentration.ConcentrationId,
                    Value = x.Concentration.ConcentrationName
                }

            });
            return result;
        }

        public async Task<StatusData<long>> InsertEducationHistory(AddAcademicRequest request, SystemSession session)
        {
            var serviceRequest = new UserAcademics
            {
                UserId = session.UserId,
                Mode = (byte)SystemDbStatus.Inserted,
                DisplayOrderId = 0,
                AcademicInstitute = request.AcademicInstitute,
                City = new City { CityId = request.CityId > 0 ? request.CityId : 0 },
                JoinedYear = request.JoinedYear.ToString(),
                GraduatedYear = request.GraduatedYear.ToString(),
                HasGraduated = request.HasGraduated,
                Degree = null,
                Concentration = new Concentration { ConcentrationName = request.Concentration },
                AcademicId = 0,
                UserIdOrName = session.UserName
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAcademic(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<long> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (result.Status.IsOperationSuccessful())
                result.Data = response.AcademicId;
            return result;
        }

        public async Task<StatusData<string>> UpdateEducationHistory(UpdateAcademicRequest request, SystemSession session)
        {
            var serviceRequest = new UserAcademics
            {
                UserId = session.UserId,
                Mode = (byte)SystemDbStatus.Updated,
                DisplayOrderId = 0,
                AcademicInstitute = request.AcademicInstitute,
                City = new City { CityId = request.CityId > 0 ? request.CityId : 0 },
                JoinedYear = request.JoinedYear.ToString(),
                GraduatedYear = request.GraduatedYear.ToString(),
                HasGraduated = request.HasGraduated,
                Degree = null,
                Concentration = new Concentration { ConcentrationName = request.Concentration },
                AcademicId = (int)request.AcademicId,
                UserIdOrName = session.UserName
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAcademic(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            return result;
        }

        public async Task<StatusData<string>> DeleteEducationHistory(SingleData<int> request, SystemSession session)
        {
            var serviceRequest = new UserAcademics
            {
                UserId = session.UserId,
                Mode = (byte)SystemDbStatus.Deleted,
                AcademicId = request.Data
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAcademic(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            return result;
        }

        public async Task<IEnumerable<Model.Profile.Personal.AwardAndHonorResponse>> GetAwardAndHonor(string targetUser,
            SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getUserAwards(targetUser, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new Model.Profile.Personal.AwardAndHonorResponse
            {
                AwardAndHonorId = x.AwardAndHonorId,
                Title = x.Title,
                Issuer = x.Issuer,
                AwardedDate = x.Date != null ? Convert.ToDateTime(x.Date) : DateTime.UtcNow,
                Description = x.Description,
                Picture = x.PictureUrl
            });
            return result;
        }

        public async Task<StatusData<long>> InsertAwardAndHonor(AddAwardAndHonorRequest request, SystemSession session)
        {
            var serviceRequest = new AwardAndHonor
            {
                UserId = session.UserId,
                Username = session.UserName,
                Title = request.Title,
                Issuer = request.Issuer,
                Date = request.Date.ToString(),
                Description = request.Description,
                AwardAndHonorId = 0,
                Mode = (byte)SystemDbStatus.Inserted,
                UserIdOrName = session.UserName
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAwardAndHonor(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<long> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (result.Status.IsOperationSuccessful())
                result.Data = response.AwardAndHonorId;
            return result;
        }

        public async Task<StatusData<string>> UpdateAwardAndHonor(UpdateAwardAndHonorRequest request, SystemSession session)
        {
            var serviceRequest = new AwardAndHonor
            {
                UserId = session.UserId,
                Username = session.UserName,
                Title = request.Title,
                Issuer = request.Issuer,
                Date = request.Date.ToString(),
                Description = request.Description,
                AwardAndHonorId = request.AwardAndHonorId,
                Mode = (byte)SystemDbStatus.Updated,
                UserIdOrName = session.UserName
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAwardAndHonor(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            return result;
        }

        public async Task<StatusData<string>> DeleteAwardAndHonor(SingleData<long> request, SystemSession session)
        {
            var serviceRequest = new AwardAndHonor
            {
                UserId = session.UserId,
                Username = session.UserName,
                AwardAndHonorId = request.Data,
                Mode = (byte)SystemDbStatus.Deleted
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertAwardAndHonor(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            return result;
        }

        public async Task<IEnumerable<LanguageResponse>> GetLanguage(string targetUser, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getUserLanguages(targetUser, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new LanguageResponse
            {
                UserLanguage = new GeneralKvPair<int, string>
                {
                    Id = x.LanguageId,
                    Value = x.Name
                },
                ProficiencyId = x.ProficiencyId
            });
            return result;
        }

        public async Task<StatusData<string>> UpsertLanguage(SingleData<List<LanguageRequest>> request, SystemSession session)
        {
            var data = string.Empty;
            if (request.Data != null && request.Data.Any())
            {
                var sb = new StringBuilder();
                foreach (var rule in request.Data)
                    sb.Append(rule).Append("|");
                data = sb.ToString().Substring(0, sb.Length - 1);
            }
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertLanguage(session.UserName, data, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatusCode,
                Message = response.DbStatusMsg,
                SubStatus = response.DbSubStatusCode
            };
            return result;
        }

        public async Task<StatusData<string>> UpsertSkill(SkillRequest request, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                UserId = session.UserId,
                Text = string.Join(",", request.Skills),
                UserIdOrName = session.UserName
                //  ProfileUserId = request.ProfileUserId,
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.upsertSkill(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<StatusData<string>> UpsertSuggestSkill(SkillSuggestionRequest request, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                //UserId = request.Suggestor,
                Text = string.Join(",", request.Skills),
                SuggestorId = session.UserId,
                UserIdOrName = request.Suggestor,//session.UserName,
                ProfileUser = request.Suggestor,
                Suggestor = session.UserName
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.suggestSkills(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<StatusData<string>> AcceptSkill(SkillAcceptanceRequest request, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                Accept = request.IsAccepted,
                UserIdOrName = session.UserName,
                Suggestor = request.Suggestor,
                Text = string.Join(",", request.Skills)
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.acceptSkill(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<PaginatedResponse<IEnumerable<UserSkillResponse>>> GetUserSkills(PaginatedRequest<string> request, SystemSession session)
        {
            var serviceRequest = new Skill
            {
                UserIdOrName = session.UserName,
                ProfileUser = request.Data,
                Offset = request.PageIndex,
                PageSize = request.PageSize
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.getSkillSummary(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var data = response.Skills.Select(x => new UserSkillResponse { Name = x.SkillName, SkillTypeId = x.SkillTypeId, ThumbsCount = x.ThumbsCount, YourThumb = x.YourThumb, UsersString = x.Users }).ToList();
            var allUsers = response.Skills.Where(x => x.Users != null).SelectMany(x => x.Users.Split(',')).Distinct().ToList();

            if (allUsers.Any())
            {
                var users = await Task.Factory.StartNew(() => Client.UserService.getUsersInfo(string.Join(",", allUsers), session.GetSession())).ConfigureAwait(false);
                var usersInformation = users.Select(x => new UserInformationBaseExtendedResponse { UserId = x.UserId, UserName = x.UserName, UserTypeId = (SystemUserType)x.UserTypeId, Image = x.Picture, FirstName = x.FirstName, LastName = x.LastName, Title = x.Title }).ToList();
                foreach (var userSkillResponse in data.Where(userSkillResponse => !string.IsNullOrEmpty(userSkillResponse.UsersString)))
                {
                    var skillResponse = userSkillResponse;
                    userSkillResponse.Users = usersInformation.Where(x => skillResponse.UsersString.Split(',').ToList().ConvertAll(Convert.ToInt32).Contains(x.UserId));
                }
            }
            var result = new PaginatedResponse<IEnumerable<UserSkillResponse>>
            {
                Page = data,
                HasNextPage = response.HasNextPage
            };

            return result;
        }


        public async Task<IEnumerable<UnApprovedSkillSuggestionResponse>> GetUnApprovedUserSkillSuggestion(string targetUser, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                UserId = session.UserId,
                Username = session.UserName,
                UserIdOrName = session.UserName,
                Suggestor = targetUser
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.getUnApprovedSkillSuggestions(serviceRequest, session.GetSession())).ConfigureAwait(false);
            if (response.Skills == null) return null;
            var result = response.Skills.Select(x => new UnApprovedSkillSuggestionResponse
            {
                SkillGuid = Guid.Parse(x.SkillGuid),
                SkillTypeId = x.SkillTypeId,
                Skill = x.SkillName,
                User = new UserResponse
                {
                    UserId = x.SuggestorId,
                    UserName = x.Username,
                    Name = x.Name,
                    Picture = x.Picture
                }
            });
            return result;
        }

        public async Task<StatusData<ThumbsForSkillResponse>> UpsertThumbsForSkill(ThumbsForSkillRequest request, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                UserId = session.UserId,
                Username = session.UserName,
                SkillTypeId = request.SkillTypeId,
                ThumbsUpOrDown = request.ThumbsUporDown,
                ProfileUser = request.ProfileUser,
                UserIdOrName = session.UserName
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertThumbsForSkill(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<ThumbsForSkillResponse> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (result.Status.IsOperationSuccessful())
            {
                result.Data = new ThumbsForSkillResponse
                {
                    ThumbsUp = response.ThumbsUp,
                    ThumbsDown = response.ThumbsDown,
                    YourThumb = response.YourThumb
                };
            }
            return result;
        }

        public async Task<PaginatedResponse<IEnumerable<ThumbsForSkillDetailResponse>>> GetThumbsForSkillDetail(ThumbsForSkillDetailRequest request, int pageIndex, int pageSize, SystemSession session)
        {
            var serviceRequest = new Kauwa.Skill
            {
                UserIdOrName = request.TargetUser,
                SkillTypeId = request.SkillTypeId,
                Offset = pageIndex,
                PageSize = pageSize
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.thumbsForSkillDetails(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<ThumbsForSkillDetailResponse>>
            {
                HasNextPage = response.HasNextPage,
                Page = response.Skills.Select(
                 x => new ThumbsForSkillDetailResponse
                 {
                     User = new UserResponse
                       {
                           UserId = x.UserId,
                           UserName = x.Username,
                           Name = x.Name,
                           Picture = x.Picture
                       },
                     UporDown = x.ThumbsUpOrDown
                 })
            };
            return result;
        }

        public async Task<IEnumerable<EmploymentWebResponse>> GetEmployment(string targetUser, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.viewEmploymentHistoryPerson(targetUser, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new EmploymentWebResponse
            {
                PersonEmploymentId = x.EmploymentId,
                CompanyUserName = x.CompanyUsername,
                Company = new GeneralKvPair<int?, string>
                {
                    Id = x.CompanyId == 0 ? (int?)null : x.CompanyId,
                    Value = x.CompanyName
                },
                Picture = x.CompanyPicture,
                Position = new GeneralKvPair<int?, string>
                {
                    Id = x.PositionId == 0 ? (int?)null : x.PositionId,
                    Value = x.Position
                },
                Location = new UserCity
                {
                    Id = x.City.CityId,
                    Name = x.City.Name,
                    CountryCode = x.Country.CountryCode,
                    CountryName = x.Country.Name
                    //Latitude = x.City.Latitude,
                    //Longitude = x.City.Longitude
                },
                EmploymentDate = new BeginEndDate
                {
                    BeginDate = Convert.ToDateTime(x.StartDate),
                    EndDate = x.EndDate != null ? Convert.ToDateTime(x.EndDate) : (DateTime?)null
                },
                IsApproved = x.ApprovedStatus,
                EmployeeType = (SystemEmployeeType)x.EmployeeTypeId,
                //EmployeeTypeName = x.EmployeeTypeName,
                IsFlexibleWorkingSchedule = (x.ScheduleType) ? SystemWorkSchedule.Fixed : SystemWorkSchedule.Flexible,
                WorkDayFixed = (x.ScheduleType) ? GetWorkingDayFixedList(x.EmployeeId, session) : null,
                WorkDayFlexible = (!x.ScheduleType) ? (GetWorkingFlexiblelist(x.EmployeeId, session)) : null
            });
            return result;
        }

        private WorkingDayFixed GetWorkingDayFixedList(long personEmploymentId, SystemSession session)
        {
            var workScheduleList = Client.UserService.getEmployeeWorkSchedule(personEmploymentId, session.GetSession()).Where(x => x.ScheduleType).ToList();
            if (!workScheduleList.Any())
                return null;
            var wkDay = new List<SystemDayOfWeek>();
            for (var i = 0; i <= 6; ++i)
                if (workScheduleList.Any(x => x.Day == i))
                    wkDay.Add((SystemDayOfWeek)i);
            var fixedSchedule = new WorkingDayFixed { WorkTime = new WorkingTime { From = workScheduleList.First().StartTime, To = workScheduleList.First().EndTime }, WeekDay = wkDay };

            return fixedSchedule;
        }

        private List<WorkingDayFlexible> GetWorkingFlexiblelist(long personEmploymentId, SystemSession session)
        {
            var workScheduleList = Client.UserService.getEmployeeWorkSchedule(personEmploymentId, session.GetSession()).Where(x => !x.ScheduleType).ToList();
            if (!workScheduleList.Any())
                return null;
            var listweekday = new List<WorkingDayFlexible>();
            for (var i = 0; i <= 6; ++i)
                if (workScheduleList.Any(x => x.Day == i))
                    listweekday.Add(new WorkingDayFlexible { WorkTime = new WorkingTime { From = workScheduleList.Where(x => x.Day == i).Select(x => x.StartTime).SingleOrDefault(), To = workScheduleList.Where(x => x.Day == i).Select(x => x.EndTime).SingleOrDefault() }, WeekDay = (SystemDayOfWeek)i });

            return listweekday;
        }

        public async Task<IEnumerable<EmployeeWorkScheduleResponse>> GetEmploymentWorkSchedule(SingleData<long> request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getEmployeeWorkSchedule(request.Data, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new EmployeeWorkScheduleResponse
            {
                EmployeeType = (SystemEmployeeType)Convert.ToInt32(x.EmployeeType),
                Day = (SystemDayOfWeek)x.Day,
                StartTime = TimeSpan.Parse(x.StartTime),
                EndTime = TimeSpan.Parse(x.EndTime),
                ScheduleType = x.ScheduleType ? SystemWorkSchedule.Fixed : SystemWorkSchedule.Flexible
            });

            return result;
        }
    }
}
