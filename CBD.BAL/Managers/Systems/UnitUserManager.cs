using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.Model.UnitUser;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;

namespace CBD.BAL.Managers
{
    public class UnitUserManager : IUnitUserManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(UNITUSERTParams model)
        {
            var result = new PagingResult();
            var totalRecords = 0;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var dataList = unitOfWork.UnitUserRepository.Search(model, out totalRecords);
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search unit user unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecords;
                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search unit user successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search unit user unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(List<SYS_UNIT_USERS> model)
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
                    foreach (var item in model)
                    {
                        var data = unitOfWork.UnitUserRepository.FirstOrDefault(x => x.USER_NAME.Equals(item.USER_NAME) && x.UNIT_ID.Equals(item.UNIT_ID));
                        if (data != null)
                        {
                            data.CODE = item.CODE;
                            //data.USER_NAME = item.USER_NAME;
                            //data.UNIT_ID = item.UNIT_ID;
                            data.IS_DEEP = item.IS_DEEP;
                            data.ORDER = item.ORDER;
                            data.USED_STATE = item.USED_STATE;
                            data.DESCRIPTION = item.DESCRIPTION;
                            data.MODIFIED_DATE = DateTime.Now;
                            data.MODIFIED_BY = item.CREATED_BY;
                        }
                        else
                        {
                            data = new TBL_SYS_UNIT_USERS();
                            data.CODE = item.CODE;
                            data.USER_NAME = item.USER_NAME;
                            data.UNIT_ID = item.UNIT_ID;
                            data.IS_DEEP = item.IS_DEEP;
                            data.ORDER = item.ORDER;
                            data.USED_STATE = item.USED_STATE;
                            data.DESCRIPTION = item.DESCRIPTION;
                            data.CREATED_DATE = DateTime.Now;
                            data.CREATED_BY = item.CREATED_BY;

                            unitOfWork.UnitUserRepository.Add(data);
                        }
                    }
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Unit Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Unit Added unsuccessfully!");
                return result;
            }
        }

        //public Result Update(SYS_UNITS model)
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
        //            var data = unitOfWork.UnitRepository.FirstOrDefault(x => x.ID == model.ID);
        //            if (data == null)
        //            {
        //                result.Code = (short)HttpStatusCode.NotFound;
        //                result.Message = string.Format("The Unit did not find. Please check it again!");
        //                return result;
        //            }

        //            data.CODE = model.CODE;
        //            data.NAME = model.NAME;
        //            data.PARENT_ID = model.PARENT_ID;
        //            data.USED_STATE = model.USED_STATE;
        //            data.DESCRIPTION = model.DESCRIPTION;
        //            data.MODIFIED_DATE = DateTime.Now;
        //            data.MODIFIED_BY = model.MODIFIED_BY;

        //            unitOfWork.SaveChanges();

        //        }

        //        result.Code = (short)HttpStatusCode.OK;
        //        result.Message = string.Format("The Unit Updated successfully!");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = string.Format("The Unit Updated unsuccessfully!");
        //        return result;
        //    }
        //}

        //public Result Delete(SYS_UNITS model)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        if (model == null)
        //        {
        //            result.Code = (short)HttpStatusCode.BadRequest;
        //            result.Message = "The ID is null. Please check again!";
        //            return result;
        //        }
        //        using (IUnitOfWork unitOfWork = new UnitOfWork())
        //        {
        //            var data = unitOfWork.UnitRepository.GetById(model.ID);
        //            if (data == null)
        //            {
        //                result.Code = (short)HttpStatusCode.NotFound;
        //                result.Message = string.Format("The Unit did not find. Please check it again!");
        //                return result;
        //            }

        //            unitOfWork.UnitRepository.Delete(data);
        //            unitOfWork.SaveChanges();

        //        }

        //        result.Code = (short)HttpStatusCode.OK;
        //        result.Message = string.Format("The Unit Deleted successfully!");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = string.Format("The Unit Deleted unsuccessfully!");
        //        return result;
        //    }
        //}

        //public Result GetAllUnits(int? UnitId)
        //{
        //    var result = new Result();
        //    try
        //    {
        //        using (IUnitOfWork unitOfWork = new UnitOfWork())
        //        {

        //            var data = unitOfWork.UnitRepository.GetAllUnits();
        //            if (data == null)
        //            {
        //                result.Code = (short)HttpStatusCode.NotFound;
        //                result.Message = "The menu did not found. Please check again!";
        //                return result;
        //            }

        //            var dataList = new List<SYS_UNITS>();
        //            if (UnitId != null && UnitId != 0)
        //            {
        //                var subdata = GetChildren(data, UnitId, string.Empty, string.Empty, null, false);
        //                var subIds = subdata.Select(s => s.ID).ToList();
        //                subIds.Add(UnitId.Value);
        //                var newdata = data.Where(x => !subIds.Contains(x.ID)).ToList();
        //                dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, subIds, false);
        //            }
        //            else
        //            {
        //                dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, null, false);
        //            }

        //            result.Code = (short)HttpStatusCode.OK;
        //            result.Data = dataList;
        //            result.Message = "Get all menu successfully!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = "Get all menu unsuccessfully!";
        //        return result;
        //    }
        //    return result;
        //}

        #region Private Method
        
        #endregion
    }
}
