using Model.Common;
using Model.CountryInfo;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using ContactSuggestions = Model.Profile.ContactSuggestions;

namespace TekTak.iLoop.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        protected readonly Services Client;

        public ProfileRepository(Services client)
        {
            Client = client;
        }

        public virtual async Task<PersonalProfileResponse> GetProfileInformation(string targetUser,
            SystemSession session)
        {
            var serviceResponse =
                await
                    Task.Factory.StartNew(
                        () => Client.UserService.getProfile(session.UserId, 0, targetUser, session.GetSession()))
                        .ConfigureAwait(false);

            var response = new PersonalProfileResponse
            {
                BasicInformation = new BasicInformation
                {
                    UserId = Convert.ToInt32(serviceResponse.User.UserId),
                    UserName = serviceResponse.User.UserName,
                    UserTypeId = (SystemUserType)serviceResponse.UserInfo.UserTypeId,
                    Email = serviceResponse.User.Email,
                    FirstName = serviceResponse.UserInfoPerson.FirstName,
                    LastName = serviceResponse.UserInfoPerson.LastName,
                    FriendShipStatus = (SystemFriendshipStatus)serviceResponse.FriendshipStatus,
                    //(SystemFriendshipStatus)await Task.Factory.StartNew(() => _client.UserService.getFriendShipStatus(userId, targetUser, session.GetSession())),
                    PrimaryContactNumber = serviceResponse.User.PrimaryContactNumber,
                    Image = serviceResponse.UserInfo.Picture,
                    //ImageService.GetProfilePhoto(serviceResponse.UserInfo, (SystemImageSize)systemImageSize),
                    AvailableStatus = (SystemUserAvailabilityCode)Convert.ToByte(serviceResponse.User.ChatStatus),
                    Title = serviceResponse.UserInfoPerson.Title,
                    LastUpdated =
                        Convert.ToInt64(
                            (Convert.ToDateTime(serviceResponse.LastUpdated) - new DateTime(1970, 1, 1))
                                .TotalMilliseconds)
                }
            };
            return response;
        }

        public virtual async Task<BaseInfoResponse> GetBasicUserInformation(string targetUser)
        {
            BaseInfoResponse response = null;
            var result =
                await
                    Task.Factory.StartNew(() => Client.UserService.getUserInfo(targetUser, null)).ConfigureAwait(false);

            if (result.UserId > 0)
            {
                response = new BaseInfoResponse
                {
                    UserId = Convert.ToInt32(result.UserId),
                    UserName = result.UserName,
                    UserTypeId = (SystemUserType)result.UserTypeId,
                    Picture = result.Picture,
                    Title = result.Title,
                    Email = result.Email,
                    FirstName = result.FirstName,
                    LastName = result.LastName
                };
            }

            return response;
        }

        public virtual async Task<IEnumerable<BaseInfoResponse>> GetBasicUsersInformation(SingleData<List<string>> request, SystemSession session)
        {
            IEnumerable<BaseInfoResponse> response = null;
            var result =
                await
                    Task.Factory.StartNew(
                        () => Client.UserService.getUsersInfo(String.Join(",", request.Data), session.GetSession()))
                        .ConfigureAwait(false);

            if (result.Any())
            {
                response = result.Select(x => new BaseInfoResponse
                {
                    UserId = Convert.ToInt32(x.UserId),
                    UserName = x.UserName,
                    UserTypeId = (SystemUserType)x.UserTypeId,
                    Picture = x.Picture,
                    Title = x.Title,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                });
            }

            return response;
        }

        public virtual async Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonRequest request,
            SystemSession session)
        {
            var serviceRequest = new UserInfoPerson
            {
                UserId = session.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                // BirthDate = request.BirthDate.ToString(),
                //  ReligionId = new Religion { ReligionId = Convert.ToInt64(request.Religion) },
                //   Nationality = new Nationality { Name = request.Nationality },
                // Interests = request.Interests,
                Title = request.Title
                // RelationshipStatusId = new RelationshipStatus { RelationshipStatusId = request.RelationshipStatusId },
                // Gender = request.Gender.ToString()
            };

            var response =
                (await
                    Task.Factory.StartNew(
                        () => Client.UserService.setBasicContactPersion(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public virtual async Task<IEnumerable<TemporaryContactInformation>> GetContacts(string targetUser, bool allResults, long? displayOnlyContactId, SystemSession session)
        {
            var result =
                await
                    Task.Factory.StartNew(
                        () => Client.UserService.getUserContactDetails(session.UserId, targetUser, session.GetSession()))
                        .ConfigureAwait(false);

            if (!allResults)
            {
                result =
                    result.Where(
                        x =>
                            x.ContactTypeId < 3 || (x.ContactTypeId >= 3 && x.ContactId == result.Max(y => y.ContactId)))
                        .ToList();
            }
            else
            {
                if (displayOnlyContactId.HasValue)
                    result = result.Where(x => x.ContactId == displayOnlyContactId.Value).ToList();
            }

            var response = result.Select(x => new TemporaryContactInformation
            {
                ContactId = x.ContactId,
                UserId = x.UserId,
                ContactTypeId = (SystemContactType)x.ContactTypeId,
                Address = new ContactField<string> { Value = x.Address, Visibility = x.AddressVisible },
                Email = new ContactField<string> { Value = x.Email, Visibility = x.EmailVisible },
                Fax = new ContactField<string> { Value = x.Fax, Visibility = x.FaxVisible },
                Mobile = new ContactField<string> { Value = x.Mobile, Visibility = x.MobileVisible },
                Phone = new ContactField<string> { Value = x.Phone, Visibility = x.PhoneVisible },
                Website = new ContactField<string> { Value = x.Website, Visibility = x.WebsiteVisible },
                CompanyUserId =
                    new ContactFieldBasic<int?>
                    {
                        Value = x.CompanyUserId == 0 ? (int?)null : x.CompanyUserId,
                        Visibility = x.CompanyUserIdVisible
                    },
                StartDate = Convert.ToDateTime(x.StartDate),
                EndDate = Convert.ToDateTime(x.EndDate),
                SuggestionCounts = x.SuggestionCount
            }).ToList();

            foreach (var contactResponse in result)
            {
                var resp = response.First(x => x.ContactId == contactResponse.ContactId);
                if (!string.IsNullOrEmpty(contactResponse.StrFixedFieldSuggestions))
                {

                    var str = contactResponse.StrFixedFieldSuggestions.Split(new[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var id = (SystemContactMarkedType)Convert.ToByte(components[0]);
                        var value = string.Join(",",
                            Enumerable.Range(1, components.Count() - 3).Select(x => components[x]));
                        var cid = Convert.ToInt64(components[components.Count() - 2]);
                        var approved = Convert.ToByte(components.Last());
                        switch (id)
                        {
                            case SystemContactMarkedType.Address:
                                resp.Address.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                            case SystemContactMarkedType.Email:
                                resp.Email.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                            case SystemContactMarkedType.Fax:
                                resp.Fax.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                            case SystemContactMarkedType.Mobile:
                                resp.Mobile.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                            case SystemContactMarkedType.Phone:
                                resp.Phone.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                            case SystemContactMarkedType.Website:
                                resp.Website.Suggestion = new FieldSuggestion<string>
                                {
                                    Value = value,
                                    Approved = (SystemApprovalStatus)approved,
                                    ContactCustomId = cid,
                                    FieldType = id
                                };
                                break;
                        }
                    }
                }

                if (resp.ChatNetworks == null) resp.ChatNetworks = new List<ChatNetworkField>();
                if (!string.IsNullOrEmpty(contactResponse.StrChatNetWorks))
                {
                    var str = contactResponse.StrChatNetWorks.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var id = (SystemContactMarkedType)Convert.ToByte(components[0]);
                        var value = string.Join(",",
                            Enumerable.Range(1, components.Count() - 2).Select(x => components[x]));
                        var cid = Convert.ToInt32(components.Last());
                        resp.ChatNetworks.Add(new ChatNetworkField
                        {
                            Original =
                                new ContactChatNetwork { ChatNetworkId = id, Value = value, ContactChatNetworkId = cid }
                        });
                    }
                }

                if (!string.IsNullOrEmpty(contactResponse.StrContactCustoms))
                {
                    resp.CustomContacts = new List<CustomContactField>();
                    var str = contactResponse.StrContactCustoms.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var fid = string.IsNullOrEmpty(components[0])
                            ? SystemContactMarkedType.Custom
                            : (SystemContactMarkedType)Convert.ToInt32(components[0]);
                        var name = components[1];
                        var value = string.Join(",",
                            Enumerable.Range(2, components.Count() - 3).Select(x => components[x]));
                        var ccid = Convert.ToInt64(components.Last());
                        resp.CustomContacts.Add(new CustomContactField
                        {
                            Original =
                                new ContactCustom { FieldType = fid, Name = name, Value = value, ContactCustomId = ccid }
                        });
                    }
                }

                if (!string.IsNullOrEmpty(contactResponse.StrChatNetworkSuggestions))
                {
                    var str = contactResponse.StrChatNetworkSuggestions.Split(new[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var fid = (SystemContactMarkedType)Convert.ToByte(components[0]);
                        var value = string.Join(",",
                            Enumerable.Range(1, components.Count() - 4).Select(x => components[x]));
                        var contactCnid = Convert.ToInt32(components[components.Count() - 3]);
                        var cid = Convert.ToInt64(components[components.Count() - 2]);
                        var approved = Convert.ToByte(components.Last());

                        var data =
                            resp.ChatNetworks.FirstOrDefault(
                                x => x.Original != null && x.Original.ContactChatNetworkId == contactCnid);
                        if (data == null)
                        {
                            data = new ChatNetworkField();
                            resp.ChatNetworks.Add(data);
                        }
                        data.Suggestion = new ChatNetworkFieldSuggestion
                        {
                            FieldType = fid,
                            Value = value,
                            ContactChatNetworkId = contactCnid,
                            ContactCustomId = cid,
                            Approved = (SystemApprovalStatus)approved
                        };
                    }
                }

                if (resp.CustomContacts == null) resp.CustomContacts = new List<CustomContactField>();
                if (!string.IsNullOrEmpty(contactResponse.StrContactCustomSuggestions))
                {
                    var str = contactResponse.StrContactCustomSuggestions.Split(new[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var name = string.IsNullOrWhiteSpace(components[0]) ? null : components[0];
                        var value = string.Join(",",
                            Enumerable.Range(1, components.Count() - 5).Select(x => components[x]));
                        var ccid = Convert.ToInt64(components[components.Count() - 4]);
                        var refccid = string.IsNullOrWhiteSpace(components[components.Count() - 3])
                            ? 0
                            : Convert.ToInt64(components[components.Count() - 3]);
                        var fstr = components[components.Count() - 2];
                        byte fint;
                        var fid = SystemContactMarkedType.Custom;
                        if (!string.IsNullOrWhiteSpace(fstr) && byte.TryParse(fstr, out fint))
                            fid = (SystemContactMarkedType)fint;
                        var approved = Convert.ToByte(components.Last());

                        var data =
                            resp.CustomContacts.FirstOrDefault(
                                x => x.Original != null && x.Original.ContactCustomId == refccid);
                        if (data == null)
                        {
                            data = new CustomContactField();
                            resp.CustomContacts.Add(data);
                        }
                        data.Suggestion = new CustomContactFieldSuggestion
                        {
                            Name = name,
                            Value = value,
                            ContactCustomId = ccid,
                            ReferralContactCustomId = refccid == 0 ? (long?)null : refccid,
                            FieldType = fid,
                            Approved = (SystemApprovalStatus)approved
                        };
                    }
                }
                var defaultFields =
                    resp.CustomContacts.Where(
                        x =>
                            (x.Original != null && x.Original.FieldType.IsDefault()) ||
                            (x.Suggestion != null && x.Suggestion.FieldType.IsDefault()));
                foreach (var customContactField in defaultFields)
                {
                    var takeField = customContactField.Original != null
                        ? customContactField.Original.FieldType
                        : customContactField.Suggestion.FieldType;
                    switch (takeField)
                    {
                        case SystemContactMarkedType.Address:
                            resp.Address.CustomContacts.Add(customContactField);
                            break;
                        case SystemContactMarkedType.Email:
                            resp.Email.CustomContacts.Add(customContactField);
                            break;
                        case SystemContactMarkedType.Fax:
                            resp.Fax.CustomContacts.Add(customContactField);
                            break;
                        case SystemContactMarkedType.Mobile:
                            resp.Mobile.CustomContacts.Add(customContactField);
                            break;
                        case SystemContactMarkedType.Phone:
                            resp.Phone.CustomContacts.Add(customContactField);
                            break;
                        case SystemContactMarkedType.Website:
                            resp.Website.CustomContacts.Add(customContactField);
                            break;
                    }
                }
                resp.CustomContacts.RemoveAll(
                    x =>
                        (x.Original != null && x.Original.FieldType.IsDefault()) ||
                        (x.Suggestion != null && x.Suggestion.FieldType.IsDefault()));
            }

            return response;
        }

        public virtual async Task<IEnumerable<EmploymentHistoryResponse>> GetEmploymentHistory(string targetUser,
            SystemSession session)
        {
            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.UserService.viewEmploymentHistoryPerson(targetUser, session.GetSession()))
                        .ConfigureAwait(false);

            var result = response.Select(x => new EmploymentHistoryResponse
            {
                PersonEmploymentId = x.EmployeeId,
                CompanyUserName = x.CompanyUsername,
                Company = new GeneralKvPair<int?, string>
                {
                    Id = x.CompanyId,
                    Value = x.CompanyName
                },
                Picture = x.CompanyPicture,
                Position = new GeneralKvPair<int?, string>
                {
                    Id = x.PositionId,
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
                    BeginDate = x.StartDate != null ? Convert.ToDateTime(x.StartDate) : DateTime.UtcNow,
                    EndDate = x.EndDate != null ? Convert.ToDateTime(x.EndDate) : (DateTime?)null
                },
                IsApproved = x.ApprovedStatus,
                EmployeeType = (SystemEmployeeType)x.EmployeeTypeId,
                //EmployeeTypeName = x.EmployeeTypeName,
                IsFlexibleWorkingSchedule = (!x.ScheduleType) ? SystemWorkSchedule.Fixed : SystemWorkSchedule.Flexible,
                WorkDayFixed = (x.ScheduleType) ? GetWorkingDayFixedList(x.EmployeeId, session) : null,
                WorkDayFlexible = (!x.ScheduleType) ? (GetWorkingFlexiblelist(x.EmployeeId, session)) : null
            });
            return result;
        }

        private WorkingDayFixed GetWorkingDayFixedList(long personEmploymentId, SystemSession session)
        {
            var workScheduleList =
                Client.UserService.getEmployeeWorkSchedule(personEmploymentId, session.GetSession())
                    .Where(x => x.ScheduleType)
                    .ToList();
            if (!workScheduleList.Any())
                return null;
            var wkDay = new List<SystemDayOfWeek>();
            for (var i = 0; i <= 6; ++i)
                if (workScheduleList.Any(x => x.Day == i))
                    wkDay.Add((SystemDayOfWeek)i);
            var fixedSchedule = new WorkingDayFixed
            {
                WorkTime =
                    new WorkingTime { From = workScheduleList.First().StartTime, To = workScheduleList.First().EndTime },
                WeekDay = wkDay
            };

            return fixedSchedule;
        }

        private List<WorkingDayFlexible> GetWorkingFlexiblelist(long personEmploymentId, SystemSession session)
        {
            var workScheduleList =
                Client.UserService.getEmployeeWorkSchedule(personEmploymentId, session.GetSession())
                    .Where(x => !x.ScheduleType)
                    .ToList();
            if (!workScheduleList.Any())
                return null;
            var listweekday = new List<WorkingDayFlexible>();
            for (var i = 0; i <= 6; ++i)
                if (workScheduleList.Any(x => x.Day == i))
                    listweekday.Add(new WorkingDayFlexible
                    {
                        WorkTime =
                            new WorkingTime
                            {
                                From =
                                    workScheduleList.Where(x => x.Day == i).Select(x => x.StartTime).SingleOrDefault(),
                                To = workScheduleList.Where(x => x.Day == i).Select(x => x.EndTime).SingleOrDefault()
                            },
                        WeekDay = (SystemDayOfWeek)i
                    });

            return listweekday;
        }

        public virtual async Task<IEnumerable<EmployeeWorkScheduleResponse>> GetEmploymentWorkSchedule(
            SingleData<long> request, SystemSession session)
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

        public virtual async Task<ContactSuggestions> GetSuggestedContactList(SystemSession session)
        {
            var result =
                await
                    Task.Factory.StartNew(() => Client.UserService.getContactSuggestions(session.GetSession()))
                        .ConfigureAwait(false);
            var suggestions = result.Select(c => new SuggestedContactResponse
            {
                ContactCustomId = c.ContactCustomId,
                ContactId = c.ContactId,
                ContactTypeId = (SystemContactType)c.ContactTypeId,
                UserId = c.UserId,
                UserName = c.UserName,
                DisplayName = c.DisplayName,
                Picture = c.Picture,
                FieldType = (SystemContactMarkedType)c.FieldId,
                Name = c.Name,
                Value = c.Value,
                OriginalName = c.OriginalName,
                OriginalValue = c.OriginalValue,
                ContactChatNetworkId = c.ContactChatNetworkId == 0 ? (int?)null : c.ContactChatNetworkId,
                ReferralContactCustomId = c.ReferralContactCustomId == 0 ? (long?)null : c.ReferralContactCustomId,
                Added = Convert.ToDateTime(c.Added)
            }).ToList();
            var response = new ContactSuggestions
            {
                Address =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Address)
                        .OrderByDescending(x => x.Added),
                Email =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Email).OrderByDescending(x => x.Added),
                Fax = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Fax).OrderByDescending(x => x.Added),
                Mobile =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Mobile)
                        .OrderByDescending(x => x.Added),
                Phone =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Phone).OrderByDescending(x => x.Added),
                Website =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Website)
                        .OrderByDescending(x => x.Added),
                ChatNetworks =
                    suggestions.Where(x => x.FieldType.IsChatNetwork())
                        .OrderBy(x => x.FieldType)
                        .ThenByDescending(x => x.Added),
                Customs =
                    suggestions.Where(x => x.FieldType == SystemContactMarkedType.Custom)
                        .OrderByDescending(x => x.Added)
            };
            return response;
        }

        public virtual async Task<StatusData<ContactSuggestionResponse>> AddSuggestedContact(
            AddSuggestContactRequest request, SystemSession session)
        {
            var chatNetworks = string.Empty;
            if (request.ChatNetworks != null && request.ChatNetworks.Any())
            {
                var sb = new StringBuilder();
                foreach (var chatNetwork in request.ChatNetworks)
                    sb.Append(chatNetwork).Append("|");
                chatNetworks = sb.ToString().Substring(0, sb.Length - 1);
            }
            var customContacts = string.Empty;
            if (request.CustomContacts != null && request.CustomContacts.Any())
            {
                var sb = new StringBuilder();
                foreach (var customContact in request.CustomContacts)
                    sb.Append(customContact).Append("|");
                customContacts = sb.ToString().Substring(0, sb.Length - 1);
            }

            var serviceRequest = new UserContactDetails
            {
                StrChatNetWorks = chatNetworks,
                StrContactCustoms = customContacts,
                Address = request.Address,
                Email = request.Email,
                Fax = request.Fax,
                Mobile = request.Mobile,
                Phone = request.Phone,
                Website = request.Website,
                ContactId = request.ContactId,
                Mode = (byte)SystemDbStatus.Updated
            };
            var response =
                await
                    Task.Factory.StartNew(
                        () =>
                            Client.UserService.upsertSuggestContacts(request.TargetUserId, serviceRequest,
                                session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<ContactSuggestionResponse>
            {
                Status = (SystemDbStatus)response.DbStatus.DbStatusCode,
                Message = response.DbStatus.DbStatusMsg,
                SubStatus = response.DbStatus.DbSubStatusCode,
                Data = new ContactSuggestionResponse()
            };
            if (result.Status.IsOperationSuccessful())
            {
                var simpleFields =
                    response.ContactCustomIds.Split(new[] { "," }, StringSplitOptions.None)
                        .Select(x => Convert.ToInt64(x))
                        .ToArray();
                result.Data.Address = simpleFields[0] == 0 ? (long?)null : simpleFields[0];
                result.Data.Email = simpleFields[1] == 0 ? (long?)null : simpleFields[1];
                result.Data.Fax = simpleFields[2] == 0 ? (long?)null : simpleFields[2];
                result.Data.Mobile = simpleFields[3] == 0 ? (long?)null : simpleFields[3];
                result.Data.Phone = simpleFields[4] == 0 ? (long?)null : simpleFields[4];
                result.Data.Website = simpleFields[5] == 0 ? (long?)null : simpleFields[5];
                if (!string.IsNullOrWhiteSpace(response.ContactChatNetworkCustomIds))
                    result.Data.ChatNetworks =
                        response.ContactChatNetworkCustomIds.Split(new[] { "," }, StringSplitOptions.None)
                            .Select(x => Convert.ToInt64(x));
                if (!string.IsNullOrWhiteSpace(response.CustomContactsCustomIds))
                    result.Data.CustomContacts =
                        response.CustomContactsCustomIds.Split(new[] { "," }, StringSplitOptions.None)
                            .Select(x => Convert.ToInt64(x));
            }
            return result;
        }

        public virtual async Task<StatusData<ContactResponse>> UpdateSuggestedContact(
            UpdateSuggestContactRequest request, SystemSession session)
        {
            var chatNetworks = string.Empty;
            if (request.ChatNetworks != null && request.ChatNetworks.Any())
            {
                var sb = new StringBuilder();
                foreach (var chatNetwork in request.ChatNetworks)
                    sb.Append(chatNetwork).Append("|");
                chatNetworks = sb.ToString().Substring(0, sb.Length - 1);
            }

            var customContacts = string.Empty;
            if (request.CustomContacts != null && request.CustomContacts.Any())
            {
                var sb = new StringBuilder();
                foreach (var customContact in request.CustomContacts)
                    sb.Append(customContact).Append("|");
                customContacts = sb.ToString().Substring(0, sb.Length - 1);
            }
            var serviceRequest = new UserContactDetails
            {
                StrChatNetWorks = chatNetworks,
                StrContactCustoms = customContacts,
                Address = request.Address,
                Email = request.Email,
                Fax = request.Fax,
                Mobile = request.Mobile,
                Phone = request.Phone,
                Website = request.Website,
                ContactId = request.ContactId,
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Updated
            };

            var response =
                await
                    Task.Factory.StartNew(
                        () =>
                            Client.UserService.upsertSuggestContacts(request.TargetUserId, serviceRequest,
                                session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<ContactResponse>
            {
                Status = (SystemDbStatus)response.DbStatus.DbStatusCode,
                Message = response.DbStatus.DbStatusMsg,
                SubStatus = response.DbStatus.DbSubStatusCode
            };
            if (result.Status.IsOperationSuccessful())
            {
                result.Data = new ContactResponse
                {
                    ContactId = request.ContactId,
                    ContactCustoms =
                        string.IsNullOrWhiteSpace(response.CustomContactsCustomIds)
                            ? null
                            : response.CustomContactsCustomIds.Split(new[] { "," }, StringSplitOptions.None)
                                .Select(x => Convert.ToInt64(x))
                                .ToList(),
                    ContactChatNetworks =
                        string.IsNullOrWhiteSpace(response.ContactChatNetworkCustomIds)
                            ? null
                            : response.ContactChatNetworkCustomIds.Split(new[] { "," }, StringSplitOptions.None)
                                .Select(x => Convert.ToInt32(x))
                                .ToList()
                };
            }
            return result;
        }

        public virtual async Task<StatusData<string>> DeleteSuggestedContact(
            SingleData<GeneralKvPair<long, List<long>>> request, SystemSession session)
        {
            var serviceRequest = new UserContactDetails
            {
                ContactId = request.Data.Id,
                ContactCustomIds = string.Join(",", request.Data.Value),
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Deleted
            };

            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.UserService.upsertSuggestContacts(0, serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatus.DbStatusCode,
                Message = response.DbStatus.DbStatusMsg,
                SubStatus = response.DbStatus.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<StatusData<string>> SuggestedContactOperation(
            SuggestedContactOperationRequest request, SystemSession session)
        {
            var response =
                await
                    Task.Factory.StartNew(
                        () =>
                            Client.UserService.respondContactSuggestion(request.ContactCustomId, request.Mode,
                                session.GetSession())).ConfigureAwait(false);
            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatusCode,
                Message = response.DbStatusMsg,
                SubStatus = response.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<StatusData<ContactResponse>> AddContact(ContactRequest request, SystemSession session)
        {

            var chatNetworks = string.Empty;
            if (request.ChatNetworks != null && request.ChatNetworks.Any())
            {
                var sb = new StringBuilder();
                foreach (var chatNetwork in request.ChatNetworks)
                    sb.Append(chatNetwork).Append("|");
                chatNetworks = sb.ToString().Substring(0, sb.Length - 1);
            }

            var customContacts = string.Empty;
            if (request.CustomContacts != null && request.CustomContacts.Any())
            {
                var sb = new StringBuilder();
                foreach (var customContact in request.CustomContacts)
                    sb.Append(customContact).Append("|");
                customContacts = sb.ToString().Substring(0, sb.Length - 1);
            }

            var serviceRequest = new UserContactDetails
            {
                StrChatNetWorks = chatNetworks,
                StrContactCustoms = customContacts,
                Address = request.Address,
                Email = request.Email,
                Fax = request.Fax,
                Mobile = request.Mobile,
                Phone = request.Phone,
                Website = request.Website,
                CompanyUserId = request.TemporaryContact.CompanyUserId ?? 0,
                StartDate = request.TemporaryContact.StartDate.ToString(),
                EndDate = request.TemporaryContact.EndDate.ToString(),
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Inserted
            };

            var response =
                await
                    Task.Factory.StartNew(() => Client.UserService.upsertContacts(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var data = new StatusData<ContactResponse>
            {
                Status = (SystemDbStatus)response.Dbstatus.DbStatusCode,
                Message = response.Dbstatus.DbStatusMsg,
                SubStatus = response.Dbstatus.DbSubStatusCode
            };

            if (data.Status.IsOperationSuccessful())
            {
                data.Data = new ContactResponse
                {
                    ContactId = response.ContactId,
                    ContactChatNetworks =
                        response.ContactChatNetworkId != null
                            ? response.ContactChatNetworkId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => Convert.ToInt32(x))
                                .ToList()
                            : null,
                    ContactCustoms =
                        response.CustomContactsCustomeIds != null
                            ? response.CustomContactsCustomeIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => Convert.ToInt64(x))
                                .ToList()
                            : null
                };
            }
            return data;
        }

        public virtual async Task<StatusData<ContactResponse>> UpdateContact(UpdateContactRequest request,
            SystemSession session)
        {
            var chatNetworks = string.Empty;
            if (request.ChatNetworks != null && request.ChatNetworks.Any())
            {
                var sb = new StringBuilder();
                foreach (var chatNetwork in request.ChatNetworks)
                    sb.Append(chatNetwork).Append("|");
                chatNetworks = sb.ToString().Substring(0, sb.Length - 1);
            }

            var customContacts = string.Empty;
            if (request.CustomContacts != null && request.CustomContacts.Any())
            {
                var sb = new StringBuilder();
                foreach (var customContact in request.CustomContacts)
                    sb.Append(customContact).Append("|");
                customContacts = sb.ToString().Substring(0, sb.Length - 1);
            }

            var serviceRequest = new UserContactDetails
            {
                ContactId = request.ContactId,
                StrChatNetWorks = chatNetworks,
                StrContactCustoms = customContacts,
                Address = request.Address,
                Email = request.Email,
                Fax = request.Fax,
                Mobile = request.Mobile,
                Phone = request.Phone,
                Website = request.Website,
                CompanyUserId =
                    (int)
                        (request.TemporaryContact != null && request.TemporaryContact.CompanyUserId != null
                            ? request.TemporaryContact.CompanyUserId
                            : 0),
                StartDate = request.TemporaryContact != null ? request.TemporaryContact.StartDate.ToString() : null,
                EndDate = (request.TemporaryContact != null) ? request.TemporaryContact.EndDate.ToString() : null,
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Updated
            };

            var response =
                await
                    Task.Factory.StartNew(() => Client.UserService.upsertContacts(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);

            var data = new StatusData<ContactResponse>
            {
                Status = (SystemDbStatus)response.Dbstatus.DbStatusCode,
                Message = response.Dbstatus.DbStatusMsg,
                SubStatus = response.Dbstatus.DbSubStatusCode
            };

            if (data.Status.IsOperationSuccessful())
            {
                data.Data = new ContactResponse
                {
                    ContactId = response.ContactId,
                    ContactChatNetworks =
                        response.ContactChatNetworkId != null
                            ? response.ContactChatNetworkId.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => Convert.ToInt32(x))
                                .ToList()
                            : null,
                    ContactCustoms =
                        response.CustomContactsCustomeIds != null
                            ? response.CustomContactsCustomeIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => Convert.ToInt64(x))
                                .ToList()
                            : null
                };
            }
            return data;
        }

        public virtual async Task<StatusData<string>> DeleteContact(SingleData<long> request, SystemSession session)
        {
            var serviceRequest = new UserContactDetails
            {
                ContactId = request.Data,
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Deleted
            };
            var response =
                await
                    Task.Factory.StartNew(() => Client.UserService.upsertContacts(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.Dbstatus.DbStatusCode,
                Message = response.Dbstatus.DbStatusMsg,
                SubStatus = response.Dbstatus.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<StatusData<string>> FlushContact(SingleData<GeneralKvPair<long, List<int>>> chatNetworks,
            SingleData<GeneralKvPair<long, List<long>>> customContacts, SystemSession session)
        {
            var serviceRequest = new UserContactDetails
            {
                ContactId = chatNetworks == null ? customContacts.Data.Id : chatNetworks.Data.Id,
                ContactChatNetworkId =
                    chatNetworks == null ? DBNull.Value.ToString() : string.Join(",", chatNetworks.Data.Value),
                CustomContactsCustomIds =
                    customContacts == null ? DBNull.Value.ToString() : string.Join(",", customContacts.Data.Value),
                UserId = session.UserId,
                Username = session.UserName,
                Mode = (byte)SystemDbStatus.Flushed
            };
            var response =
                await
                    Task.Factory.StartNew(() => Client.UserService.upsertContacts(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.Dbstatus.DbStatusCode,
                Message = response.Dbstatus.DbStatusMsg,
                SubStatus = response.Dbstatus.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<IEnumerable<ContactSettingResponse>> GetContactSettings(long contactId,
            SystemSession session)
        {
            var serviceRequest = new UserContactSettings
            {
                UserId = session.UserId,
                ContactId = contactId
            };
            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.SettingService.getContactSettings(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var result = response.Select(x => new ContactSettingResponse
            {
                ContactCustomId = x.ContactCustomId,
                ContactChatNetworkId = x.ContactChatNetworkId,
                FieldType = (SystemContactMarkedType)x.FieldId,
                IsDefault = x.IsDefault,
                IsBasicPrimary = x.IsBasicPrimary,
                Value = (SystemSettingValue)x.Value
            });
            return result;
        }

        public virtual async Task<IEnumerable<ContactSettingCategoriesResponse>> GetContactSettingCategory(
            ContactSettingCategoriesRequest request, SystemSession session)
        {
            var serviceRequest = new UserContactSettings
            {
                UserId = session.UserId,
                ContactId = request.ContactId,
                ContactCustomId = request.ContactCustomId
            };
            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.SettingService.getContactSettingsCategories(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var result = response.Select(x => new ContactSettingCategoriesResponse
            {
                UserCategoryTypeId = x.UserCategory.UserCategoryTypeId,
                Name = x.UserCategory.Name,
                Description = x.UserCategory.Description
            });
            return result;
        }

        public virtual async Task<IEnumerable<ContactSettingFriendsResponse>> GetContactSettingFriend(
            ContactSettingCategoriesRequest request, SystemSession session)
        {
            var serviceRequest = new UserContactSettings
            {
                UserId = session.UserId,
                ContactId = request.ContactId,
                ContactCustomId = request.ContactCustomId
            };
            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.SettingService.getContactSettingsFriends(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var result = response.Select(x => new ContactSettingFriendsResponse
            {
                UserId = x.UserId,
                //UserName = x,
                UserTypeId = (SystemUserType)x.UserInfo.UserTypeId,
                Name = x.UserInfoPerson.FirstName + " " + x.UserInfoPerson.LastName,
                Picture = x.UserInfo.Picture,
                Title = x.UserInfoPerson.Title
            });
            return result;
        }

        public virtual async Task<StatusData<string>> UpsertContactSetting(ContactSettingRequest request, byte mode,
            SystemSession session)
        {
            var serviceRequest = new UserContactSettings
            {
                UserId = session.UserId,
                ContactId = request.ContactId,
                FieldId = (int)request.FieldType,
                ContactChatNetworkId = request.ContactChatNetworkId,
                ContactCustomId = request.ContactCustomId,
                Mode = mode,
                Value = request.Value,
                EntryList = string.Join(",", request.Entries),
                EntriesInPage = null

            };
            var response =
                await
                    Task.Factory.StartNew(
                        () => Client.SettingService.upsertContactSetting(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false);
            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatusCode,
                Message = response.DbStatusMsg,
                SubStatus = response.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<StatusData<bool>> SaveProfilePicture(string username, SingleData<string> request)
        {
            var result = new StatusData<bool>
            {
                Data = await
                    Task.Factory.StartNew(() => Client.UserService.saveUserProfilePic(username, request.Data))
                        .ConfigureAwait(false)
            };
            result.Status = result.Data ? SystemDbStatus.Updated : SystemDbStatus.NotModified;
            return result;
        }

        public virtual async Task<StatusData<string>> RespondEmploymentRequest(RespondEmploymentRequest request,
            SystemSession session)
        {
            var serviceRequest = new EmploymentRequest
            {
                UserId = session.UserId,
                TargetUsers = request.TargetUser,
                Accepted = request.IsAccepted
            };

            var response =
                (await
                    Task.Factory.StartNew(
                        () => Client.UserService.respondEmpoymentRequest(serviceRequest, session.GetSession()))
                        .ConfigureAwait(false));

            var data = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatus.DbStatusCode,
                Message = response.DbStatus.DbStatusMsg,
                SubStatus = response.DbStatus.DbSubStatusCode
            };
            return data;
        }

        public virtual async Task<UserStatusResponse> GetUserAvailability(string targetUser, SystemSession session)
        {
            UserStatusResponse response = null;
            var result = await Task.Factory.StartNew(() => Client.UserService.getUserAvailability(targetUser, session.GetSession())).ConfigureAwait(false);

            if (result != null)
            {
                response = new UserStatusResponse { StatusTypeId = (SystemUserAvailabilityCode)Convert.ToByte(result.Status.StatusTypeId) };
                if (response.StatusTypeId == SystemUserAvailabilityCode.OutOfOffice)
                {
                    response.OutOfOffice = new OutOfOfficeResponse
                    {
                        BeginDateAndTime =
                            new DateAndTime
                            {
                                Date =
                                    !string.IsNullOrWhiteSpace(result.Status.BeginDate)
                                        ? Convert.ToDateTime(result.Status.BeginDate)
                                        : (DateTime?)null,
                                Time =
                                    result.Status.BeginTime != null
                                        ? TimeSpan.Parse(result.Status.BeginTime)
                                        : (TimeSpan?)null
                            },
                        EndDateAndTime =
                            new DateAndTime
                            {
                                Date =
                                    !string.IsNullOrWhiteSpace(result.Status.EndDate)
                                        ? Convert.ToDateTime(result.Status.EndDate)
                                        : (DateTime?)null,
                                Time =
                                    result.Status.EndTime != null
                                        ? TimeSpan.Parse(result.Status.EndTime)
                                        : (TimeSpan?)null
                            },
                        Location = result.Status.Location,
                        ReceptionMode = (SystemStatusReceptionMode)result.Status.ReceptionMode,
                        Assignee = result.UserId > 0
                            ? new Assignee
                            {
                                UserId = result.UserId,
                                UserName = result.UserName,
                                FirstName = result.FirstName,
                                LastName = result.LastName,
                                UserTypeId = (SystemUserType)result.UserTypeId,
                                Image = result.Picture,
                                Title = result.PositionName,
                                CallNumber = result.CallNumber,
                                AvailableForMessage = result.AvailableForMsg,
                                StatusTypeId = (SystemUserAvailabilityCode)result.StatusTypeId,
                                ReceptionMode = (SystemStatusReceptionMode)result.ReceptionMode
                            }
                            : null
                    };
                }
            }

            return response;
        }

        public async Task<StatusData<string>> UpdateUserAvailability(StatusSetRequest request, SystemSession session)
        {
            var serviceRequest = new Status
            {
                UserId = session.UserId.ToString(),
                StatusTypeId = ((byte)request.StatusTypeId).ToString()
            };
            if (request.StatusTypeId == SystemUserAvailabilityCode.OutOfOffice)
            {
                serviceRequest.BeginDate = request.OutOfOfficeRequest.BeginDateAndTime.Date.HasValue ? request.OutOfOfficeRequest.BeginDateAndTime.Date.ToString() : null;
                serviceRequest.BeginTime = request.OutOfOfficeRequest.BeginDateAndTime.Time.HasValue ? request.OutOfOfficeRequest.BeginDateAndTime.Time.ToString() : null;
                serviceRequest.EndDate = request.OutOfOfficeRequest.EndDateAndTime.Date.HasValue ? request.OutOfOfficeRequest.EndDateAndTime.Date.ToString() : null;
                serviceRequest.EndTime = request.OutOfOfficeRequest.EndDateAndTime.Time.HasValue ? request.OutOfOfficeRequest.EndDateAndTime.Time.ToString() : null;
                serviceRequest.Location = request.OutOfOfficeRequest.Location;
                serviceRequest.FriendId = request.OutOfOfficeRequest.AssigneeUserId.HasValue ? request.OutOfOfficeRequest.AssigneeUserId.ToString() : null;
                serviceRequest.StatusContactType = (short)request.OutOfOfficeRequest.ReceptionMode;
            }

            var response = await Task.Factory.StartNew(() => Client.UserService.setStatus(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatusCode,
                Message = response.DbStatusMsg,
                SubStatus = response.DbSubStatusCode
            };

            return result;
        }

        public async Task<StatusData<string>> SignalView(SignalViewRequest request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.signalView(request.UserId.ToString(), request.TargetUser, request.GroupId.GetValueOrDefault(), (byte)request.ViewType, request.TypeId.GetValueOrDefault(), session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string>
            {
                Status = (SystemDbStatus)response.DbStatusCode,
                Message = response.DbStatusMsg,
                SubStatus = response.DbSubStatusCode
            };

            return result;
        }
    }

}