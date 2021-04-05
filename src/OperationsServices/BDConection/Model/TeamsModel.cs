using BDConection.BDModel;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BDConection.Model
{
    public class TeamsModel
    {
        public GetTeamsResponse GetTeams()
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    var result = context.St_GetTeams().ToList();
                    if (result?.Count == 0)
                    {
                        return new GetTeamsResponse
                        {
                            isSaved = false,
                            Message = "0 Teams or Error"
                        };
                    }
                    List<Models.Common.Team> teams = result.Select(t => new Models.Common.Team
                    {
                        Name = t.TeamName,
                        Teamid = t.Teamid
                    }).ToList();

                    return new GetTeamsResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        Teams = teams
                    };

                }
            }
            catch (Exception ex)
            {
                return new GetTeamsResponse
                {
                    isSaved = false,
                    Message = ex.Message
                };
            }
        }

        public GetTeamsResponse GetTeam(int Teamid)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    var result = context.St_GetTeamsUsers(Teamid).ToList();
                    if (result?.Count == 0)
                    {
                        return new GetTeamsResponse
                        {
                            isSaved = false,
                            Message = "0 Teams or Error"
                        };
                    }
                    //Distinct a los teams
                    List<Models.Common.Team> teams = result.Select(t => new Models.Common.Team
                    {
                        Name = t.TeamName,
                        Teamid = t.Teamid,
                    }).GroupBy(r => r.Teamid).Select(a => a.First()).ToList();

                    //Obtener los usuarios ligados a los teams
                    teams = teams.Select(t => new Models.Common.Team
                    {
                        Teamid = t.Teamid,
                        Name = t.Name,
                        Users = result.Where(r => r.Teamid == t.Teamid) //Filtrar por team
                        .Select(u => new Models.Common.User { Userid = u.UserId, UserLogin = u.UserLogin, FullName = u.FullName }).ToList() //Obtener lista de user
                    }).ToList();

                    return new GetTeamsResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        Teams = teams
                    };

                }
            }
            catch (Exception ex)
            {
                return new GetTeamsResponse
                {
                    isSaved = false,
                    Message = ex.Message
                };
            }
        }

        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    if (request.Teamid > 0) //Usuario existente
                    {
                        var t = context.St_GetTeamsUsers(request.Teamid).ToList();
                        if (t?.Count == 0)
                        {
                            return new BasicResponse
                            {
                                isSaved = false,
                                Message = "Team not Found for update"
                            };
                        }
                    }

                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);

                    var xml = new XElement("Users", request.Users.Select(i => new XElement("user", new XAttribute("id", i)))).ToString();
                    
                    context.St_InsertUpdateTeams(request.Teamid, request.TeamName, xml, HasError);

                    if (Convert.ToBoolean(HasError.Value))
                    {
                        return new BasicResponse
                        {
                            isSaved = false,
                            Message = "Insert error"
                        };
                    }

                    return new BasicResponse
                    {
                        isSaved = true,
                        Message = request.Teamid > 0 ? "Update Success" : "Insert Success",
                    };

                }
            }
            catch (Exception ex)
            {
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.Message
                };
            }
        }

    }
}
