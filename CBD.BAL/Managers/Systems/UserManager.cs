using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.Model.User;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;
using CBD.Model.Utilities;

namespace CBD.BAL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(USERParams model)
        {
            var result = new PagingResult();            
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var totalRecords = 0;
                    var dataList = unitOfWork.UserRepository.Search(model, out totalRecords);
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search Users unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecords;                   

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search Users successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search Users unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(SYS_USERS model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The model is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.UserRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The User already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.UserRepository.FirstOrDefault(x => x.USER_NAME.Equals(model.USER_NAME));
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The User's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_SYS_USERS();
                    data.USER_NAME = model.USER_NAME;
                    data.PASSWORD = SHAUtils.ToSHAPassword(model.PASSWORD);
                    var depass = SHAUtils.DeSHAPassword(data.PASSWORD);
                    data.FULL_NAME = model.FULL_NAME;
                    data.EMAIL = model.EMAIL;
                    data.AVATAR = model.AVATAR;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.UserRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The User Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The User Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(SYS_USERS model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The model is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.UserRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The User did not find. Please check it again!");
                        return result;
                    }

                    data.USER_NAME = model.USER_NAME;
                    //data.PASSWORD = model.PASSWORD;
                    data.FULL_NAME = model.FULL_NAME;
                    data.EMAIL = model.EMAIL;
                    data.AVATAR = model.AVATAR;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The User Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The User Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(SYS_USERS model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The ID is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.UserRepository.GetById(model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The User did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.UserRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The User Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The User Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetByUser(SYS_USERS model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The ID is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.UserRepository.GetByUser(model);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The User did not find. Please check it again!");
                        return result;
                    }

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = data;
                    result.Message = string.Format("The User find successfully!");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The User find unsuccessfully!");
                return result;
            }
        }


        #region Private Method
        #endregion
    }
}
