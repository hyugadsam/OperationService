using BdConectionCore.BDConection;
using BdConectionCore.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelsCore.Common;
using ModelsCore.Request;
using ModelsCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BdConectionCore.Services
{
    public class TeamsService
    {
        private string _conection;
        public TeamsService(string con)
        {
            this._conection = con;
        }

        public GetTeamsResponse GetTeams()
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    var result = context.Teams.ToList();
                    if (result?.Count == 0)
                    {
                        return new GetTeamsResponse
                        {
                            isSaved = false,
                            Message = "0 Teams or Error"
                        };
                    }
                    List<Team> teams = result.Select(t => new Team
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
                throw ex;
            }
        }

        public GetTeamsResponse GetTeam(int Teamid)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pTeamid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Teamid",
                        Value = Teamid
                    };

                    #endregion

                    var result = context.SpGetTeamsUsers.FromSqlRaw("EXEC [dbo].[St_GetTeamsUsers] @Teamid", pTeamid).ToList();
                    if (result?.Count == 0)
                    {
                        return new GetTeamsResponse
                        {
                            isSaved = false,
                            Message = "0 Teams or Error"
                        };
                    }
                    //Distinct a los teams
                    List<Team> teams = result.Select(t => new Team
                    {
                        Name = t.TeamName,
                        Teamid = t.Teamid,
                    }).GroupBy(r => r.Teamid).Select(a => a.First()).ToList();

                    //Obtener los usuarios ligados a los teams
                    teams = teams.Select(t => new Team
                    {
                        Teamid = t.Teamid,
                        Name = t.Name,
                        Users = result.Where(r => r.UserId != null && r.Teamid == t.Teamid) //Filtrar por team
                        .Select(u => new User { Userid = u.UserId.Value, UserLogin = u.UserLogin, FullName = u.FullName }).ToList() //Obtener lista de user
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
                throw ex;
            }
        }

        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    if (request.Teamid > 0) //Usuario existente
                    {
                        var t = GetTeam(request.Teamid);
                        if (t.Teams.Count == 0)
                        {
                            return new BasicResponse
                            {
                                isSaved = false,
                                Message = "Team not Found for update"
                            };
                        }
                    }

                    #region Params

                    var xml = new XElement("Users", request.Users.Select(i => new XElement("user", new XAttribute("id", i)))).ToString();

                    var pTeamid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Teamid",
                        Value = request.Teamid
                    };

                    var pTeamName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@TeamName",
                        Value = request.TeamName
                    };

                    var pUsers = new SqlParameter
                    {
                        DbType = System.Data.DbType.Xml,
                        ParameterName = "@Users",
                        Value = xml
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };

                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_InsertUpdateTeams] @Teamid, @TeamName, @Users, @HasError OUTPUT",
                                                        pTeamid, pTeamName, pUsers, pHasError);

                    if (Convert.ToBoolean(pHasError.Value))
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
                throw ex;
            }
        }

        public GetTeamLogsResponse GetTeamLogs()
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    var result = context.TeamLogs.Join(context.Teams,
                        log => log.Teamid,
                        team => team.Teamid,
                        (log, team) => new TeamLog
                        {
                            Name = team.TeamName,
                            Teamid = team.Teamid,
                            DateOfMovement = log.DateofMovement,
                            Users = XmlUtility.ToUserList(log.OldUsers),
                            NewUsers = XmlUtility.ToUserList(log.NewUsers)
                        }).ToList();

                    if (result?.Count == 0)
                    {
                        return new GetTeamLogsResponse
                        {
                            isSaved = false,
                            Message = "0 TeamsLogs or Error"
                        };
                    }

                    return new GetTeamLogsResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        Logs = result
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BasicResponse DeleteTeam(int Teamid)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pTeamid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Teamid",
                        Value = Teamid
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };

                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_DeleteTeam] @Teamid, @HasError OUTPUT", pTeamid, pHasError);

                    if (Convert.ToBoolean(pHasError.Value))
                    {
                        return new BasicResponse
                        {
                            isSaved = false,
                            Message = "Delete Error"
                        };
                    }

                    return new BasicResponse
                    {
                        isSaved = true,
                        Message = "Delete Success"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
