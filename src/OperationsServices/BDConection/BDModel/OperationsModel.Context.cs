﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BDConection.BDModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OperationsEntities : DbContext
    {
        public OperationsEntities()
            : base("name=OperationsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int St_InsertUpdateUser(Nullable<int> userid, string fullName, Nullable<int> roleid, string email, string password, string userLogin, string salt, Nullable<bool> passwordEdited, ObjectParameter hasError)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(int));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var roleidParameter = roleid.HasValue ?
                new ObjectParameter("Roleid", roleid) :
                new ObjectParameter("Roleid", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var userLoginParameter = userLogin != null ?
                new ObjectParameter("UserLogin", userLogin) :
                new ObjectParameter("UserLogin", typeof(string));
    
            var saltParameter = salt != null ?
                new ObjectParameter("Salt", salt) :
                new ObjectParameter("Salt", typeof(string));
    
            var passwordEditedParameter = passwordEdited.HasValue ?
                new ObjectParameter("PasswordEdited", passwordEdited) :
                new ObjectParameter("PasswordEdited", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_InsertUpdateUser", useridParameter, fullNameParameter, roleidParameter, emailParameter, passwordParameter, userLoginParameter, saltParameter, passwordEditedParameter, hasError);
        }
    
        public virtual int St_UpdateUserProfile(Nullable<int> userid, string fullName, string englishLevel, string knowlEdge, string urlResume, ObjectParameter hasError)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(int));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var englishLevelParameter = englishLevel != null ?
                new ObjectParameter("EnglishLevel", englishLevel) :
                new ObjectParameter("EnglishLevel", typeof(string));
    
            var knowlEdgeParameter = knowlEdge != null ?
                new ObjectParameter("KnowlEdge", knowlEdge) :
                new ObjectParameter("KnowlEdge", typeof(string));
    
            var urlResumeParameter = urlResume != null ?
                new ObjectParameter("UrlResume", urlResume) :
                new ObjectParameter("UrlResume", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_UpdateUserProfile", useridParameter, fullNameParameter, englishLevelParameter, knowlEdgeParameter, urlResumeParameter, hasError);
        }
    
        public virtual ObjectResult<St_GetUsers_Result> St_GetUsers(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<St_GetUsers_Result>("St_GetUsers", useridParameter);
        }
    
        public virtual ObjectResult<St_GetTeams_Result> St_GetTeams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<St_GetTeams_Result>("St_GetTeams");
        }
    
        public virtual int St_InsertUpdateTeams(Nullable<int> teamid, string teamName, string users, ObjectParameter hasError)
        {
            var teamidParameter = teamid.HasValue ?
                new ObjectParameter("Teamid", teamid) :
                new ObjectParameter("Teamid", typeof(int));
    
            var teamNameParameter = teamName != null ?
                new ObjectParameter("TeamName", teamName) :
                new ObjectParameter("TeamName", typeof(string));
    
            var usersParameter = users != null ?
                new ObjectParameter("Users", users) :
                new ObjectParameter("Users", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_InsertUpdateTeams", teamidParameter, teamNameParameter, usersParameter, hasError);
        }
    
        public virtual int St_InsertUpdateAccount(Nullable<int> accountid, string accountName, string clientName, string operatorName, Nullable<int> teamid, ObjectParameter hasError)
        {
            var accountidParameter = accountid.HasValue ?
                new ObjectParameter("Accountid", accountid) :
                new ObjectParameter("Accountid", typeof(int));
    
            var accountNameParameter = accountName != null ?
                new ObjectParameter("AccountName", accountName) :
                new ObjectParameter("AccountName", typeof(string));
    
            var clientNameParameter = clientName != null ?
                new ObjectParameter("ClientName", clientName) :
                new ObjectParameter("ClientName", typeof(string));
    
            var operatorNameParameter = operatorName != null ?
                new ObjectParameter("OperatorName", operatorName) :
                new ObjectParameter("OperatorName", typeof(string));
    
            var teamidParameter = teamid.HasValue ?
                new ObjectParameter("Teamid", teamid) :
                new ObjectParameter("Teamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_InsertUpdateAccount", accountidParameter, accountNameParameter, clientNameParameter, operatorNameParameter, teamidParameter, hasError);
        }
    
        public virtual ObjectResult<St_GetAccounts_Result> St_GetAccounts(Nullable<int> accountid)
        {
            var accountidParameter = accountid.HasValue ?
                new ObjectParameter("Accountid", accountid) :
                new ObjectParameter("Accountid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<St_GetAccounts_Result>("St_GetAccounts", accountidParameter);
        }
    
        public virtual int st_InsertAppLogs(string @class, string method, string errorMessage, Nullable<System.DateTime> dateOfInsert)
        {
            var classParameter = @class != null ?
                new ObjectParameter("Class", @class) :
                new ObjectParameter("Class", typeof(string));
    
            var methodParameter = method != null ?
                new ObjectParameter("Method", method) :
                new ObjectParameter("Method", typeof(string));
    
            var errorMessageParameter = errorMessage != null ?
                new ObjectParameter("ErrorMessage", errorMessage) :
                new ObjectParameter("ErrorMessage", typeof(string));
    
            var dateOfInsertParameter = dateOfInsert.HasValue ?
                new ObjectParameter("DateOfInsert", dateOfInsert) :
                new ObjectParameter("DateOfInsert", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("st_InsertAppLogs", classParameter, methodParameter, errorMessageParameter, dateOfInsertParameter);
        }
    
        public virtual int St_DeleteAccount(Nullable<int> accountid, ObjectParameter hasError)
        {
            var accountidParameter = accountid.HasValue ?
                new ObjectParameter("Accountid", accountid) :
                new ObjectParameter("Accountid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_DeleteAccount", accountidParameter, hasError);
        }
    
        public virtual int St_DeleteTeam(Nullable<int> teamid, ObjectParameter hasError)
        {
            var teamidParameter = teamid.HasValue ?
                new ObjectParameter("Teamid", teamid) :
                new ObjectParameter("Teamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_DeleteTeam", teamidParameter, hasError);
        }
    
        public virtual int St_DeleteUser(Nullable<int> userid, ObjectParameter hasError)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("Userid", userid) :
                new ObjectParameter("Userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("St_DeleteUser", useridParameter, hasError);
        }
    
        public virtual ObjectResult<St_GetTeamsLogs_Result> St_GetTeamsLogs()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<St_GetTeamsLogs_Result>("St_GetTeamsLogs");
        }
    
        public virtual ObjectResult<St_GetTeamsUsers_Result> St_GetTeamsUsers(Nullable<int> teamid)
        {
            var teamidParameter = teamid.HasValue ?
                new ObjectParameter("Teamid", teamid) :
                new ObjectParameter("Teamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<St_GetTeamsUsers_Result>("St_GetTeamsUsers", teamidParameter);
        }
    }
}
