using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.Model.UnitGroupPage;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;

namespace CBD.BAL.Managers
{
    public class UnitGroupPageManager : IUnitGroupPageManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(UNITGROUPPAGEParams model)
        {
            var result = new PagingResult();
            var totalRecords = 0;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    
                    var dataList = unitOfWork.UnitGroupPageRepository.Search(model, out totalRecords);
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search units unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecords;

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search units successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search units unsuccessfully!";
                return result;
            }
            return result;
        }

        //public Result Add(SYS_UNITS model)
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
        //            if (data != null)
        //            {
        //                result.Code = (short)HttpStatusCode.Conflict;
        //                result.Message = string.Format("The Unit already existed. Please check it again!");
        //                return result;
        //            }

        //            data = unitOfWork.UnitRepository.FirstOrDefault(x => x.CODE == model.CODE);
        //            if (data != null)
        //            {
        //                result.Code = (short)HttpStatusCode.Conflict;
        //                result.Message = string.Format("The Unit's Code already existed. Please check it again!");
        //                return result;
        //            }

        //            data = new TBL_SYS_UNITS();
        //            data.CODE = model.CODE;
        //            data.NAME = model.NAME;
        //            data.PARENT_ID = model.PARENT_ID;
        //            data.USED_STATE = model.USED_STATE;
        //            data.DESCRIPTION = model.DESCRIPTION;
        //            data.CREATED_DATE = DateTime.Now;
        //            data.CREATED_BY = model.CREATED_BY;

        //            unitOfWork.UnitRepository.Add(data);
        //            unitOfWork.SaveChanges();

        //        }

        //        result.Code = (short)HttpStatusCode.OK;
        //        result.Message = string.Format("The Unit Added successfully!");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex.Message);
        //        result.Code = (short)HttpStatusCode.ExpectationFailed;
        //        result.Message = string.Format("The Unit Added unsuccessfully!");
        //        return result;
        //    }
        //}

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

        #region Private Method
        private List<SYS_UNITS> GetChildren(IQueryable<SYS_UNITS> dataList, int? parentId, string prefixc, string parentprefix, List<int> disableIds, bool isSearch)
        {
            var dataLst = new List<SYS_UNITS>();
            var prefixcharactor = string.Empty;
            var objs = dataList.Where(f => f.PARENT_ID == parentId).OrderBy(o => o.ID);
            
            foreach (var item in objs)
            {
                if (disableIds == null || disableIds.Count == 0)
                {
                    item.IS_DISABLE = false;
                }
                else
                {
                    if (disableIds.Contains(item.ID))
                    {
                        item.IS_DISABLE = true;
                    }
                }
                
                if (isSearch)
                {
                    item.NAME = string.Format("{0} {1}", prefixcharactor, item.NAME);
                    item.USEDSTATE_NAME = item.USED_STATE != null && item.USED_STATE > 0 ? Enums.Description((USED_STATE)item.USED_STATE) : string.Empty;
                }
                else
                {
                    if (parentId != null)
                    {
                        prefixcharactor = string.Format("{0}{1}", prefixc, parentprefix);
                    }
                    item.NAME = string.Format("{0} {1}", prefixcharactor, item.NAME);
                }

                dataLst.Add(item);
                var subdata = GetChildren(dataList, item.ID, prefixc, prefixcharactor, disableIds, isSearch);
                dataLst.AddRange(subdata);
            }

            return dataLst;
        }
        #endregion
    }
}
