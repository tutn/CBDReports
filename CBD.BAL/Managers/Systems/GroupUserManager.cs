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
    public class GroupUserManager : IGroupUserManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(USERParams model)
        {
            var result = new PagingResult();
            var totalRecords = 0;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    
                    var users = unitOfWork.UserRepository.Search(model, out totalRecords);
                    if (users == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search Groups unsuccessfully!";
                        return result;
                    }
                    
                     

                    result.Total = totalRecords;                   

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = users;
                    result.Message = "Search Groups successfully!";
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

        //public Result Add(SYS_GROUPS model)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        if (model == null)
        //        {
        //            result.Code = (short)HttpStatusCode.BadRequest;
        //            result.Message = "The model is null. Please check again!";
        //            return result;
        //        }
        //        using (IUnitOfWork unitOfWork = new UnitOfWork())
        //        {
        //            var data = unitOfWork.GroupRepository.FirstOrDefault(x => x.ID == model.ID);
        //            if (data != null)
        //            {
        //                result.Code = (short)HttpStatusCode.Conflict;
        //                result.Message = string.Format("The Group already existed. Please check it again!");
        //                return result;
        //            }

        //            data = unitOfWork.GroupRepository.FirstOrDefault(x => x.CODE.Equals(model.CODE));
        //            if (data != null)
        //            {
        //                result.Code = (short)HttpStatusCode.Conflict;
        //                result.Message = string.Format("The Group's Code already existed. Please check it again!");
        //                return result;
        //            }

        //            data = new TBL_SYS_GROUPS();
        //            data.CODE = model.CODE;
        //            data.NAME = model.NAME;
        //            data.ORDER = model.ORDER;
        //            data.USED_STATE = model.USED_STATE;
        //            data.DESCRIPTION = model.DESCRIPTION;
        //            data.CREATED_DATE = DateTime.Now;
        //            data.CREATED_BY = model.CREATED_BY;

        //            unitOfWork.GroupRepository.Add(data);
        //            unitOfWork.SaveChanges();

        //        }

        //        result.Code = (short)HttpStatusCode.OK;
        //        result.Message = string.Format("The Group Added successfully!");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = string.Format("The Group Added unsuccessfully!");
        //        return result;
        //    }
        //}

        //public Result Update(SYS_GROUPS model)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        if (model == null)
        //        {
        //            result.Code = (short)HttpStatusCode.BadRequest;
        //            result.Message = "The model is null. Please check again!";
        //            return result;
        //        }
        //        using (IUnitOfWork unitOfWork = new UnitOfWork())
        //        {
        //            var data = unitOfWork.GroupRepository.FirstOrDefault(x => x.ID == model.ID);
        //            if (data == null)
        //            {
        //                result.Code = (short)HttpStatusCode.NotFound;
        //                result.Message = string.Format("The Group did not find. Please check it again!");
        //                return result;
        //            }

        //            data.CODE = model.CODE;
        //            data.NAME = model.NAME;
        //            data.ORDER = model.ORDER;
        //            data.USED_STATE = model.USED_STATE;
        //            data.DESCRIPTION = model.DESCRIPTION;
        //            data.MODIFIED_DATE = DateTime.Now;
        //            data.MODIFIED_BY = model.MODIFIED_BY;

        //            unitOfWork.SaveChanges();

        //        }

        //        result.Code = (short)HttpStatusCode.OK;
        //        result.Message = string.Format("The Group Updated successfully!");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = string.Format("The Group Updated unsuccessfully!");
        //        return result;
        //    }
        //}

        //public Result Delete(SYS_GROUPS model)
        //{
        //    var result = new Result();
        //    if (model == null)
        //    {
        //        result.Code = (short)HttpStatusCode.BadRequest;
        //        result.Message = "The ID is null. Please check again!";
        //        return result;
        //    }

        //    // Xóa cái item children của group
        //    using (IUnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        try
        //        {
        //            using (var transaction = unitOfWork.DbContext.Database.BeginTransaction())
        //            {
        //                //var groupusers = unitOfWork.Grou
        //                var data = unitOfWork.GroupRepository.GetById(model.ID);
        //                if (data == null)
        //                {
        //                    result.Code = (short)HttpStatusCode.NotFound;
        //                    result.Message = string.Format("The Group did not find. Please check it again!");
        //                    return result;
        //                }

        //                unitOfWork.GroupRepository.Delete(data);
        //                unitOfWork.SaveChanges();
        //            }

        //            result.Code = (short)HttpStatusCode.OK;
        //            result.Message = string.Format("The Group Deleted successfully!");
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Error(ex.Message);
        //            result.Code = (short)HttpStatusCode.ExpectationFailed;
        //            result.Message = string.Format("The Group Deleted unsuccessfully!");
        //            return result;
        //        }
        //    }            
        //}

        #region Private Method
        
        #endregion
    }
}
