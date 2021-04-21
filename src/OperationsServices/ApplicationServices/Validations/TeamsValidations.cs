using Models.Request;
using Models.Response;

namespace ApplicationServices.Validations
{
    public class TeamsValidations: BaseMethods
    {
        internal BasicResponse SaveTeamValidations(SaveTeamRequest request)
        {
            if (string.IsNullOrEmpty(request?.TeamName))
            {
                return new BasicResponse
                {
                    isSaved = false,
                    Message = "Team Name invalid"
                };
            }

            if (request?.Users?.Count == 0)
            {
                return new BasicResponse
                {
                    isSaved = false,
                    Message = "Provide at least 1 user for the team"
                };
            }

            return new BasicResponse { isSaved = true };
        }

    }
}
