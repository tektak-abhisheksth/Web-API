using DAL.DbEntity;
using Entity;
using Model.Base;
using Model.Common;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Profile
{
    public class ProfileRepository : GenericRepository<UserInfo>, IProfileRepository
    {
        public ProfileRepository(iLoopEntity context) : base(context) { }

        public async Task<List<UserInformation>> GetProfiles(int userId, IEnumerable<GeneralKvPair<int, long>> profileTags)
        {
            var str = string.Join("|", profileTags.Select(x => string.Format("{0},{1}", x.Id, x.Value)));
            var basicInfo = (await Task.Factory.StartNew(() => Context.FNGETPROFILEINFORMATIONS(userId, str)).ConfigureAwait(false)).ToList();
            return basicInfo.Select(x => new UserInformation { UserId = x.UserId, FirstName = x.FirstName, LastName = x.LastName, FriendShipStatus = (SystemFriendshipStatus)x.FriendshipStatus, Title = x.Title, PrimaryContactNumber = x.PrimaryContactNumber, LastUpdated = Convert.ToInt64((x.LastUpdate - new DateTime(1970, 1, 1)).TotalMilliseconds) }).ToList();
        }

        public async Task<List<TemporaryContactInformation>> GetProfilesContacts(int userId, IEnumerable<int> targetUsers)
        {
            var users = string.Join(",", targetUsers);
            var contactInfo = await Task.Factory.StartNew(() => Context.FNCONTACTSDETAILS(userId, users).ToList()).ConfigureAwait(false);


            var response = contactInfo.Select(x => new TemporaryContactInformation
            {
                ContactId = x.ContactID,
                UserId = x.UserID,
                ContactTypeId = (SystemContactType)x.ContactTypeID,
                Address = new ContactField<string> { Value = x.Address, Visibility = x.AddressVisible },
                Email = new ContactField<string> { Value = x.Email, Visibility = x.EmailVisible },
                Fax = new ContactField<string> { Value = x.Fax, Visibility = x.FaxVisible },
                Mobile = new ContactField<string> { Value = x.Mobile, Visibility = x.MobileVisible },
                Phone = new ContactField<string> { Value = x.Phone, Visibility = x.PhoneVisible },
                Website = new ContactField<string> { Value = x.Website, Visibility = x.WebsiteVisible },
                CompanyUserId = new ContactFieldBasic<int?> { Value = x.CompanyUserID == 0 ? (int?)null : x.CompanyUserID, Visibility = x.CompanyUserIDVisible },
                StartDate = Convert.ToDateTime(x.StartDate),
                EndDate = Convert.ToDateTime(x.EndDate)
            }).ToList();

            foreach (var contactResponse in contactInfo)
            {
                var resp = response.First(x => x.ContactId == contactResponse.ContactID);
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
                                resp.Address.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                            case SystemContactMarkedType.Email:
                                resp.Email.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                            case SystemContactMarkedType.Fax:
                                resp.Fax.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                            case SystemContactMarkedType.Mobile:
                                resp.Mobile.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                            case SystemContactMarkedType.Phone:
                                resp.Phone.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                            case SystemContactMarkedType.Website:
                                resp.Website.Suggestion = new FieldSuggestion<string> { Value = value, Approved = (SystemApprovalStatus)approved, ContactCustomId = cid, FieldType = id };
                                break;
                        }
                    }
                }

                if (resp.ChatNetworks == null) resp.ChatNetworks = new List<ChatNetworkField>();
                if (!string.IsNullOrEmpty(contactResponse.StrChatNetworks))
                {
                    var str = contactResponse.StrChatNetworks.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var id = (SystemContactMarkedType)Convert.ToByte(components[0]);
                        var value = string.Join(",", Enumerable.Range(1, components.Count() - 2).Select(x => components[x]));
                        var cid = Convert.ToInt32(components.Last());
                        resp.ChatNetworks.Add(new ChatNetworkField
                        {
                            Original = new ContactChatNetwork { ChatNetworkId = id, Value = value, ContactChatNetworkId = cid }
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
                        var fid = string.IsNullOrEmpty(components[0]) ? SystemContactMarkedType.Custom : (SystemContactMarkedType)Convert.ToInt32(components[0]);
                        var name = components[1];
                        var value = string.Join(",", Enumerable.Range(2, components.Count() - 3).Select(x => components[x]));
                        var ccid = Convert.ToInt64(components.Last());
                        resp.CustomContacts.Add(new CustomContactField
                        {
                            Original = new ContactCustom { FieldType = fid, Name = name, Value = value, ContactCustomId = ccid }
                        });
                    }
                }

                if (!string.IsNullOrEmpty(contactResponse.StrChatNetworkSuggestions))
                {
                    var str = contactResponse.StrChatNetworkSuggestions.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var fid = (SystemContactMarkedType)Convert.ToByte(components[0]);
                        var value = string.Join(",", Enumerable.Range(1, components.Count() - 4).Select(x => components[x]));
                        var contactCnid = Convert.ToInt32(components[components.Count() - 3]);
                        var cid = Convert.ToInt64(components[components.Count() - 2]);
                        var approved = Convert.ToByte(components.Last());

                        var data = resp.ChatNetworks.FirstOrDefault(x => x.Original != null && x.Original.ContactChatNetworkId == contactCnid);
                        if (data == null)
                        {
                            data = new ChatNetworkField();
                            resp.ChatNetworks.Add(data);
                        }
                        data.Suggestion = new ChatNetworkFieldSuggestion { FieldType = fid, Value = value, ContactChatNetworkId = contactCnid, ContactCustomId = cid, Approved = (SystemApprovalStatus)approved };
                    }
                }

                if (resp.CustomContacts == null) resp.CustomContacts = new List<CustomContactField>();
                if (!string.IsNullOrEmpty(contactResponse.StrContactCustomSuggestions))
                {
                    var str = contactResponse.StrContactCustomSuggestions.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        var components = item.Split(new[] { "," }, StringSplitOptions.None);
                        var name = string.IsNullOrWhiteSpace(components[0]) ? null : components[0];
                        var value = string.Join(",", Enumerable.Range(1, components.Count() - 5).Select(x => components[x]));
                        var ccid = Convert.ToInt64(components[components.Count() - 4]);
                        var refccid = string.IsNullOrWhiteSpace(components[components.Count() - 3]) ? 0 : Convert.ToInt64(components[components.Count() - 3]);
                        var fstr = components[components.Count() - 2];
                        byte fint;
                        var fid = SystemContactMarkedType.Custom;
                        if (!string.IsNullOrWhiteSpace(fstr) && byte.TryParse(fstr, out fint))
                            fid = (SystemContactMarkedType)fint;
                        var approved = Convert.ToByte(components.Last());

                        var data = resp.CustomContacts.FirstOrDefault(x => x.Original != null && x.Original.ContactCustomId == refccid);
                        if (data == null)
                        {
                            data = new CustomContactField();
                            resp.CustomContacts.Add(data);
                        }
                        data.Suggestion = new CustomContactFieldSuggestion { Name = name, Value = value, ContactCustomId = ccid, ReferralContactCustomId = refccid == 0 ? (long?)null : refccid, FieldType = fid, Approved = (SystemApprovalStatus)approved };
                    }
                }
                var defaultFields = resp.CustomContacts.Where(x => (x.Original != null && x.Original.FieldType.IsDefault()) || (x.Suggestion != null && x.Suggestion.FieldType.IsDefault()));
                foreach (var customContactField in defaultFields)
                {
                    var takeField = customContactField.Original != null ? customContactField.Original.FieldType : customContactField.Suggestion.FieldType;
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
                resp.CustomContacts.RemoveAll(x => (x.Original != null && x.Original.FieldType.IsDefault()) || (x.Suggestion != null && x.Suggestion.FieldType.IsDefault()));
            }

            return response;
        }

        public async Task<BaseInfoResponse> GetBasicUserInformation(int userId, string userName)
        {
            BaseInfoResponse response = null;
            int otherUser;
            var isUserId = int.TryParse(userName, out otherUser);
            var result = await Task.Factory.StartNew(() => Context.UserLogins.FirstOrDefault(x => isUserId ? x.UserId == otherUser : x.UserName == userName)).ConfigureAwait(false);

            if (result != null)
            {
                response = new BaseInfoResponse
                {
                    UserId = result.UserInfo.UserID,
                    UserName = result.UserInfo.UserLogin.UserName,
                    UserTypeId = (SystemUserType)result.UserInfo.UserTypeID,
                    Picture = result.UserInfo.Picture
                    //FriendShipStatus = (SystemFriendshipStatus)await Task.Factory.StartNew(() => Context.FNGETFRIENDSHIPSTATUS(userId, result.UserId).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false)
                };

                if (result.UserInfo.UserTypeID == (byte)SystemUserType.Person)
                {
                    var userInfoPerson = result.UserInfo.UserInfoPerson;
                    response.FirstName = userInfoPerson.FirstName;
                    response.LastName = userInfoPerson.LastName;
                }
                else
                {
                    var userInfoCompany = result.UserInfo.UserInfoCompany;
                    response.FirstName = userInfoCompany.Name;
                }
            }

            return response;
        }

        public async Task UpdateLocation(int userId, double latitude, double longitude, DateTimeOffset offset)
        {
            var user = await Context.UserLocations.FirstOrDefaultAsync(x => x.UserID == userId).ConfigureAwait(false);
            if (user != null)
            {
                user.Location = DbGeography.FromText(String.Format("POINT({0} {1})", latitude, longitude));
                user.Offset = offset;
                user.Updated = DateTime.UtcNow;
            }
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<StatusData<string>> FlushContact(int userId, SingleData<GeneralKvPair<long, List<int>>> chatNetworks, SingleData<GeneralKvPair<long, List<long>>> customContacts)
        {
            var contactId = new ObjectParameter("CONTACTID", chatNetworks == null ? customContacts.Data.Id : chatNetworks.Data.Id);
            var contactChatNetworkIds = new ObjectParameter("CONTACTCHATNETWORKIDS", chatNetworks == null ? DBNull.Value.ToString() : string.Join(",", chatNetworks.Data.Value));
            var contactCustomIds = new ObjectParameter("CONTACTCUSTOMIDS", customContacts == null ? DBNull.Value.ToString() : string.Join(",", customContacts.Data.Value));

            var response = await Task.Factory.StartNew(() => Context.PROC_UPSERT_CONTACTS(userId, (byte)SystemDbStatus.Flushed, null, null, null, null, null, null, null, null, null, null, null, contactId, contactChatNetworkIds, contactCustomIds).First()).ConfigureAwait(false);

            var data = new StatusData<string> { Status = (SystemDbStatus)response.DBSTATUS, Message = response.DBMESSAGE, SubStatus = response.DBSUBSTATUS.GetValueOrDefault() };
            return data;
        }

        public async Task<ContactSuggestions> GetSuggestedContactList(int targetUserId)
        {
            var result = await Task.Factory.StartNew(() => Context.FNGETCONTACTSUGGESTIONS(targetUserId)).ConfigureAwait(false);
            var suggestions = result.Select(c => new SuggestedContactResponse
            {
                ContactCustomId = c.ContactCustomID,
                ContactId = c.ContactID,
                ContactTypeId = (SystemContactType)c.ContactTypeID,
                UserId = c.UserID,
                UserName = c.UserName,
                DisplayName = c.DisplayName,
                Picture = c.Picture,
                FieldType = (SystemContactMarkedType)c.FieldID,
                Name = c.Name,
                Value = c.Value,
                OriginalName = c.OriginalName,
                OriginalValue = c.OriginalValue,
                ContactChatNetworkId = c.ContactChatNetworkID,
                ReferralContactCustomId = c.ReferralContactCustomID,
                Added = c.Added
            }).ToList();
            var response = new ContactSuggestions
            {
                Address = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Address).OrderByDescending(x => x.Added),
                Email = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Email).OrderByDescending(x => x.Added),
                Fax = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Fax).OrderByDescending(x => x.Added),
                Mobile = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Mobile).OrderByDescending(x => x.Added),
                Phone = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Phone).OrderByDescending(x => x.Added),
                Website = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Website).OrderByDescending(x => x.Added),
                ChatNetworks = suggestions.Where(x => x.FieldType.IsChatNetwork()).OrderBy(x => x.FieldType).ThenByDescending(x => x.Added),
                Customs = suggestions.Where(x => x.FieldType == SystemContactMarkedType.Custom).OrderByDescending(x => x.Added)
            };
            return response;
        }

        public async Task<StatusData<string>> SuggestedContactOperation(SuggestedContactOperationRequest request)
        {
            var response = await Task.Factory.StartNew(() => Context.PROC_RESPOND_CONTACT_SUGGESTION(request.UserId, (byte?)SystemDbStatus.Updated, request.ContactCustomId).First()).ConfigureAwait(false);
            var data = new StatusData<string> { Status = (SystemDbStatus)response.DBSTATUS, Message = response.DBMESSAGE, SubStatus = response.DBSUBSTATUS.GetValueOrDefault() };
            return data;
        }

        public async Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request)
        {
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", typeof(bool));
            var totalViews = new ObjectParameter("TOTALVIEWS", typeof(int));
            var response = await Task.Factory.StartNew(() => Context.PROC_PROFILE_VIEW_DETAIL(request.UserId, (byte?)request.Data.Id, request.Data.Value, request.PageIndex, request.PageSize, hasNextPage, totalViews).ToList()).ConfigureAwait(false);
            var data = response.Select(x => new ViewerDetailResponse
            {
                UserId = x.USERID,
                UserTypeId = (SystemUserType)x.USERTYPEID,
                UserName = x.USERNAME,
                FirstName = x.FIRSTNAME,
                LastName = x.LASTNAME,
                Image = x.PICTURE,
                FriendshipStatus = (SystemFriendshipStatus)x.ISCONNECTED,
                Observed = x.OBSERVED,
                Title = x.TITLE,
                ViewedDate = x.VIEWEDDATE
            });
            var result = new PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>
            {
                Page = data,
                HasNextPage = Convert.ToBoolean(hasNextPage.Value),
                Information = Convert.ToInt32(totalViews.Value)
            };
            return result;
        }

        public async Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(int userId)
        {
            var totalTypes = new ObjectParameter("TOTALTYPES", typeof(int));
            var response = await Task.Factory.StartNew(() => Context.PROC_PROFILE_VIEW_PANEL(userId, totalTypes).ToList()).ConfigureAwait(false);
            var data = response.Select(x => new ViewerPanelResponse
            {
                UserId = x.UserId,
                UserTypeId = (SystemUserType)x.UserTypeID,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Image = x.Picture,
                Observed = x.Observed,
                Title = x.Title,
                ViewedDate = x.ViewedDate,
                Position = x.POSITIONNAME,
                ViewerCount = x.VIEWERSCOUNT.GetValueOrDefault()
            });
            var result = new InformationResponse<IEnumerable<ViewerPanelResponse>, int>
            {
                Page = data,
                Information = Convert.ToInt32(totalTypes.Value)
            };
            return result;
        }

        public async Task<InformationResponse<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request)
        {
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", typeof(bool));
            var totalViews = new ObjectParameter("TOTALVIEWCOUNT", typeof(int));
            var response = await Task.Factory.StartNew(() => Context.PROC_PROFILE_VIEW_SUMMARY(request.UserId, request.Data, request.PageIndex, request.PageSize, hasNextPage, totalViews).ToList()).ConfigureAwait(false);
            var data = response.Select(x => new ViewSummaryResponse
            {
                TypeId = x.TYPEID,
                Name = x.NAME,
                ViewCount = x.TOTALVIEWS,
                NewViewCount = x.NEWVIEWS,
                LastViewed = x.LASTVIEWED.GetValueOrDefault()
            });
            var result = new InformationResponse<IEnumerable<ViewSummaryResponse>, int>
            {
                Page = data,
                Information = Convert.ToInt32(totalViews.Value)
            };
            return result;
        }

        public async Task<PaginatedResponse<IEnumerable<UserSkillResponse>>> GetUserSkills(PaginatedRequest<string> request)
        {
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", typeof(bool));
            var response = await Task.Factory.StartNew(() => Context.PROC_THUMBS_FOR_SKILL_SUMMARY(request.UserId.ToString(), request.Data, request.PageIndex, request.PageSize, hasNextPage).ToList()).ConfigureAwait(false);
            var data = response.Select(x => new UserSkillResponse { Name = x.SKILLNAME, SkillTypeId = x.SKILLTYPEID, ThumbsCount = x.THUMBSCOUNT, YourThumb = x.YOURTHUMB, UsersString = x.USERS }).ToList();
            var allUsers = response.Where(x => x.USERS != null).SelectMany(x => x.USERS.Split(',')).Distinct().ToList();

            if (allUsers.Any())
            {
                var users = await Task.Factory.StartNew(() => Context.FNGETUSERSINFO(string.Join(",", allUsers)).ToList()).ConfigureAwait(false);
                var usersInformation = users.Select(x => new UserInformationBaseExtendedResponse { UserId = x.UserId, UserName = x.UserName, UserTypeId = (SystemUserType)x.UserTypeID, Image = x.Picture, FirstName = x.FirstName, LastName = x.LastName, Title = x.Title }).ToList();
                foreach (var userSkillResponse in data.Where(userSkillResponse => !string.IsNullOrEmpty(userSkillResponse.UsersString)))
                {
                    var skillResponse = userSkillResponse;
                    userSkillResponse.Users = usersInformation.Where(x => skillResponse.UsersString.Split(',').ToList().ConvertAll(Convert.ToInt32).Contains(x.UserId));
                }
            }
            var result = new PaginatedResponse<IEnumerable<UserSkillResponse>>
            {
                Page = data,
                HasNextPage = Convert.ToBoolean(hasNextPage.Value)
            };

            return result;
        }



        #region Old Codes
        //public PersonalProfileResponse GetPersonalProfileInformation(int userId, int targetId)
        //{
        //    PersonalProfileResponse response = null;
        //    var userInfo = this.FirstOrDefault(x => x.UserID == targetId);

        //    if (userInfo != null)
        //    {
        //        var userInfoPerson = userInfo.UserInfoPerson;
        //        if (userInfoPerson != null)
        //        {
        //            response = new PersonalProfileResponse();

        //            #region Basic Information
        //            response.BasicInformation = new BasicInformation
        //                                              {
        //                                                  AvailableStatus = userInfoPerson.UserInfo.UserLogin.ChatStatus,
        //                                                  DateOfBirth = userInfoPerson.BirthDate.GetValueOrDefault(),
        //                                                  FirstName = userInfoPerson.FirstName,
        //                                                  LastName = userInfoPerson.LastName,
        //                                                  Gender = userInfoPerson.Gender,
        //                                                  Interests = userInfoPerson.Interests,
        //                                                  Nationality = userInfoPerson.Nationality == null ? null :
        //                                                      new GeneralKvPair<int, string>
        //                                                          {
        //                                                              Id = userInfoPerson.NationalityID.GetValueOrDefault(),
        //                                                              Value = userInfoPerson.Nationality.Name
        //                                                          },
        //                                                  FriendShipStatus = (byte)this.Context.FNGETFRIENDSHIPSTATUS(userId, targetId).FirstOrDefault().GetValueOrDefault(),
        //                                                  PrimaryContactNumber =
        //                                                      userInfoPerson.UserInfo.UserLogin.PrimaryContactNumber,
        //                                                  Profession = userInfoPerson.UserInfo.Headline,
        //                                                  RelationshipStatus = userInfoPerson.RelationshipStatu == null ? null :
        //                                                      new GeneralKvPair<byte, string>
        //                                                          {
        //                                                              Id = userInfoPerson.RelationshipStatusID.GetValueOrDefault(),
        //                                                              Value = userInfoPerson.RelationshipStatu.Name
        //                                                          },
        //                                                  Religion = userInfoPerson.Religion == null ? null :
        //                                                      new GeneralKvPair<int, string>
        //                                                          {
        //                                                              Id = userInfoPerson.ReligionID.GetValueOrDefault(-1),
        //                                                              Value = userInfoPerson.Religion.Name
        //                                                          },
        //                                                  Rating = userInfoPerson.CompanyUserRatings.Any() ? userInfoPerson.CompanyUserRatings.Average(x => x.Rating) : 0,
        //                                                  Picture = ImageRepository.GetProfilePhoto(userInfoPerson.UserInfo)

        //                                              };
        //            #endregion

        //            #region Contact Details
        //            var homeContact = userInfoPerson.UserInfo.Contacts.FirstOrDefault(x => x.ContactTypeID == (byte)SystemContactType.Home);
        //            var officeContact = userInfoPerson.UserInfo.Contacts.FirstOrDefault(x => x.ContactTypeID == (byte)SystemContactType.Office);
        //            response.ContactDetails = new Contact();
        //            if (homeContact != null)
        //                response.ContactDetails.Home = new ContactInformation
        //                {
        //                    Address = homeContact.Address,
        //                    ContactId = homeContact.ContactID,
        //                    ContactTypeId = homeContact.ContactTypeID,
        //                    Email = homeContact.Email,
        //                    Fax = homeContact.Fax,
        //                    Mobile = homeContact.Mobile,
        //                    Phone = homeContact.Phone,
        //                    Website = homeContact.Website,
        //                    ChatNetworks = homeContact.ContactChatNetworks.Select(x => new GeneralKvPair<byte, string>
        //                                {
        //                                    Id = x.ChatNetworkID,
        //                                    Value = x.Value
        //                                }),
        //                    CustomFields = homeContact.ContactCustoms.Select(x => new GeneralKvPair<string, string>
        //                               {
        //                                   Id = x.Name,
        //                                   Value = x.Value
        //                               })
        //                };
        //            if (officeContact != null)
        //                response.ContactDetails.Office = new ContactInformation
        //                {
        //                    Address = officeContact.Address,
        //                    ContactId = officeContact.ContactID,
        //                    ContactTypeId = officeContact.ContactTypeID,
        //                    Email = officeContact.Email,
        //                    Fax = officeContact.Fax,
        //                    Mobile = officeContact.Mobile,
        //                    Phone = officeContact.Phone,
        //                    Website = officeContact.Website,
        //                    ChatNetworks = officeContact.ContactChatNetworks.Select(x => new GeneralKvPair<byte, string>
        //                    {
        //                        Id = x.ChatNetworkID,
        //                        Value = x.Value
        //                    }),
        //                    CustomFields = officeContact.ContactCustoms.Select(x => new GeneralKvPair<string, string>
        //                    {
        //                        Id = x.Name,
        //                        Value = x.Value
        //                    })
        //                };
        //            #endregion

        //            #region Skills & Expertise
        //            response.SkillsAndExpertise =
        //                userInfoPerson.UserInfo.UserSkills.Select(
        //                    x =>
        //                    new Skill
        //                        {
        //                            SkillType =
        //                                new GeneralKvPair<int, string>
        //                                    {
        //                                        Id = x.SkillTypeID,
        //                                        Value = x.SkillType.Name
        //                                    },
        //                            ThumbsDownCount = this.Context.ThumbsForUserSkills.Count(y => y.SkillGUID == x.SkillGUID && y.UpOrDown),
        //                            ThumbsUpCount = this.Context.ThumbsForUserSkills.Count(y => y.SkillGUID == x.SkillGUID && !y.UpOrDown)
        //                        }).OrderByDescending(x => x.ThumbsUpCount).ThenByDescending(x => x.ThumbsDownCount).ThenBy(x => x.SkillType.Value);
        //            #endregion

        //            #region Education History
        //            response.EducationHistory = userInfoPerson.Academics.Select(
        //                    x =>
        //                    new Education
        //                        {
        //                            AcademicInstitute = new GeneralKvPair<int, string>
        //                                    {
        //                                        Id = x.AcademicInstituteID.GetValueOrDefault(),
        //                                        Value = x.AcademicInstitute.Name
        //                                    },
        //                            Concentration = x.AcademicConcentration != null
        //                                    ? new GeneralKvPair<short, string>
        //                                          {
        //                                              Id = x.ConcentrationID.GetValueOrDefault(),
        //                                              Value = x.AcademicConcentration.Name
        //                                          }
        //                                    : null,
        //                            DisplayOrderId = x.DisplayOrderID,
        //                            JoinedYear = x.JoinedYear.GetValueOrDefault(),
        //                            GraduatedYear = x.GraduatedYear,
        //                            Location = this.Context.Cities.Where(y => y.CityID == x.CityID).Select(
        //                                    y =>
        //                                    new UserCity
        //                                        {
        //                                            Id = y.CityID,
        //                                            Name = y.Name,
        //                                            CountryCode = y.CountryCode,
        //                                            Latitude = y.Latitude,
        //                                            Longitude = y.Longitude
        //                                        }).FirstOrDefault()
        //                        }).OrderBy(x => x.DisplayOrderId);
        //            #endregion

        //            #region Employment History
        //            response.EmploymentHistory = this.Context.VWEmploymentHistory_Person.Where(x => x.USERID == targetId).ToList().Select(x => new Employment
        //                {
        //                    Company = new GeneralKvPair<int?, string>
        //                    {
        //                        Id = x.COMPANYID,
        //                        Value = x.COMPANYNAME
        //                    },
        //                    Picture = ImageRepository.GetProfilePhoto(userInfoPerson.UserInfo),
        //                    Position = new GeneralKvPair<int?, string>
        //                    {
        //                        Id = x.PositionID,
        //                        Value = x.POSITION
        //                    },
        //                    Location = this.Context.Cities.Where(y => y.CityID == x.CityID).Select(
        //                                    y =>
        //                                    new UserCity
        //                                    {
        //                                        Id = y.CityID,
        //                                        Name = y.Name,
        //                                        CountryCode = y.CountryCode,
        //                                        Latitude = y.Latitude,
        //                                        Longitude = y.Longitude
        //                                    }).FirstOrDefault(),
        //                    EmploymentDate = new BeginEndDate
        //                    {
        //                        BeginDate = x.STARTDATE,
        //                        EndDate = x.ENDDATE
        //                    },
        //                    EmployeeType = x.EMPLOYEETYPEID,
        //                    IsApproved = x.APPROVEDSTATUS
        //                }).OrderBy(x => x.IsApproved).ThenBy(x => x.EmploymentDate.EndDate);
        //            #endregion

        //            #region Award and honour History
        //            response.AwardAndHonourHistory =
        //                userInfoPerson.UserInfo.AwardAndHonors.Select(
        //                    x => new Award
        //                        {
        //                            AwardAndHonorId = x.AwardAndHonorID,
        //                            AwardedDate = x.Date,
        //                            Description = x.Description,
        //                            Issuer = x.Issuer,
        //                            Title = x.Title
        //                        }).OrderBy(x => x.AwardAndHonorId);
        //            #endregion

        //            #region Language
        //            response.Language = userInfoPerson.UserInfo.UserLanguages.Select(
        //                    x => new Language
        //                        {
        //                            UserLanguage = new GeneralKvPair<int, string>
        //                                    {
        //                                        Id = x.LanguageID,
        //                                        Value = x.Language.Name
        //                                    },
        //                            Proficiency = new GeneralKvPair<byte, string>
        //                                    {
        //                                        Id = x.ProficiencyID.GetValueOrDefault(),
        //                                        Value = x.Proficiency.Name
        //                                    }
        //                        }).OrderByDescending(x => x.Proficiency.Id).ThenBy(x => x.Proficiency.Value);
        //            #endregion

        //            #region Biography
        //            response.Biography = userInfoPerson.UserInfo.About;
        //            #endregion
        //        }
        //    }
        //    return response;
        //}

        //public PersonalProfileResponse GetPersonalProfileInformation(int userId, int targetId, byte? systemImageSize)
        //{
        //    PersonalProfileResponse response = null;
        //    var userInfo = this.FirstOrDefault(x => x.UserID == targetId);

        //    if (userInfo != null)
        //    {
        //        var userInfoPerson = userInfo.UserInfoPerson;
        //        if (userInfoPerson != null)
        //        {
        //            response = new PersonalProfileResponse
        //            {
        //                BasicInformation = new BasicInformation
        //                {
        //                    AvailableStatus = userInfoPerson.UserInfo.UserLogin.ChatStatus,
        //                    FirstName = userInfoPerson.FirstName,
        //                    LastName = userInfoPerson.LastName,
        //                    FriendShipStatus = (byte)this.Context.FNGETFRIENDSHIPSTATUS(userId, targetId).FirstOrDefault().GetValueOrDefault(),
        //                    PrimaryContactNumber = userInfoPerson.UserInfo.UserLogin.PrimaryContactNumber,
        //                    Picture = ImageRepository.GetProfilePhoto(userInfo, (SystemImageSize)systemImageSize),
        //                    Email = userInfo.UserLogin.LoweredEmail
        //                }
        //            };

        //            #region Basic Information

        //            #endregion
        //        }
        //    }
        //    return response;
        //}
        #endregion
    }
}
