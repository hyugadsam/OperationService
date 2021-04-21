using ApplicationServicesCore.Validations;
using BdConectionCore.Services;
using Microsoft.Extensions.Configuration;
using ModelsCore.Request;
using ModelsCore.Response;
using System;

namespace ApplicationServicesCore
{
    public class TeamsAppService : TeamsValidations
    {
        private string Conection;
        public TeamsAppService(IConfiguration iConfig)
        {
            Conection = iConfig.GetConnectionString("OperationBD");
            this.BaseConection = Conection;
        }

        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            try
            {
                var valid = SaveTeamValidations(request); //Validaciones
                if (!valid.isSaved)
                {
                    return valid;
                }

                var Service = new TeamsService(Conection);
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
                var Service = new TeamsService(Conection);
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
                var Service = new TeamsService(Conection);
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
                var Service = new TeamsService(Conection);
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
                var Service = new TeamsService(Conection);
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
