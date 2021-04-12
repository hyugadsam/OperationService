using ApplicationServices.Validations;
using BDConection.Model;
using Models.Request;
using Models.Response;
using System;

namespace ApplicationServices
{
    public class TeamsAppService : TeamsValidations
    {
        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            try
            {
                var valid = SaveTeamValidations(request); //Validaciones
                if (!valid.isSaved)
                {
                    return valid;
                }

                var Service = new TeamsModel();
                return Service.SaveTeam(request);
            }
            catch (Exception ex)
            {
                SaveError("TeamsAppService", "SaveTeam", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }

        }

        public GetTeamsResponse GetAllTeams()
        {
            try
            {
                var Service = new TeamsModel();
                return Service.GetTeams();
            }
            catch (Exception ex)
            {
                SaveError("TeamsAppService", "GetAllTeams", ex.ToString(), DateTime.Now);
                return new GetTeamsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

        public GetTeamsResponse GetTeam(int Teamid)
        {
            try
            {
                var Service = new TeamsModel();
                return Service.GetTeam(Teamid);
            }
            catch (Exception ex)
            {
                SaveError("TeamsAppService", "GetAllTeams", ex.ToString(), DateTime.Now);
                return new GetTeamsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

        public GetTeamLogsResponse GetTeamLogs()
        {
            try
            {
                var Service = new TeamsModel();
                return Service.GetTeamLogs();
            }
            catch (Exception ex)
            {
                SaveError("TeamsAppService", "GetTeamLogs", ex.ToString(), DateTime.Now);
                return new GetTeamLogsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

        public BasicResponse DeleteTeam(int Teamid)
        {
            try
            {
                var Service = new TeamsModel();
                return Service.DeleteTeam(Teamid);
            }
            catch (Exception ex)
            {
                SaveError("TeamsAppService", "DeleteTeam", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

    }
}
