using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.Model.Page;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;

namespace CBD.BAL.Managers
{
    public class PageManager : IPageManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(PAGEParams model)
        {
            var result = new PagingResult();
            var skipRecord = model.PageSize * model.PageNumber;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.PageRepository.Search(model);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search pages unsuccessfully!";
                        return result;
                    }

                    result.Total = data.Count();
                    var dataList = GetChildren(data, model.PARENT_ID, SystemConfiguration.PREFIXC, string.Empty, null, true);
                    dataList = dataList.Skip(skipRecord).Take(model.PageSize).ToList();

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search pages successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search pages unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(SYS_PAGES model)
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
                    var data = unitOfWork.PageRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Page already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.PageRepository.FirstOrDefault(x => x.CODE == model.CODE);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Page's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_SYS_PAGES();
                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.NAME_VI = model.NAME_VI;
                    data.NAME_EN = model.NAME_EN;
                    data.URL = model.URL;
                    data.ICON = model.ICON;
                    data.PARENT_ID = model.PARENT_ID;
                    data.ORDER = model.ORDER;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.PageRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The page Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The page Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(SYS_PAGES model)
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
                    var data = unitOfWork.PageRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Page did not find. Please check it again!");
                        return result;
                    }

                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.NAME_VI = model.NAME_VI;
                    data.NAME_EN = model.NAME_EN;
                    data.URL = model.URL;
                    data.ICON = model.ICON;
                    data.PARENT_ID = model.PARENT_ID;
                    data.ORDER = model.ORDER;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The page Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The page Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(SYS_PAGES model)
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
                    var data = unitOfWork.PageRepository.GetById(model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Page did not find. Please check it again!");
                        return result;
                    }
                    var delList = new List<TBL_SYS_PAGES>();
                    delList.Add(data);

                    var pages = unitOfWork.PageRepository.QueryAll();
                    var subdata = GetChildren(pages, data.ID);
                    delList.AddRange(subdata);
                    delList.Reverse();

                    unitOfWork.PageRepository.DeleteRange(delList);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The page Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The page Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetAllPages(int? PageId)
        {
            var result = new Result();
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {

                    var data = unitOfWork.PageRepository.GetAllPages();
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "The menu did not found. Please check again!";
                        return result;
                    }

                    var dataList = new List<SYS_PAGES>();
                    if (PageId != null && PageId != 0)
                    {
                        var subdata = GetChildren(data, PageId, string.Empty, string.Empty, null, false);
                        var subIds = subdata.Select(s => s.ID).ToList();
                        subIds.Add(PageId.Value);
                        var newdata = data.Where(x => !subIds.Contains(x.ID)).ToList();
                        dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, subIds, false);
                    }
                    else
                    {
                        dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, null, false);
                    }

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Get all menu successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Get all menu unsuccessfully!";
                return result;
            }
            return result;
        }

        #region Private Method
        private List<SYS_PAGES> GetChildren(IQueryable<SYS_PAGES> dataList, int? parentId, string prefixc, string parentprefix, List<int> disableIds, bool isSearch)
        {
            var dataLst = new List<SYS_PAGES>();
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
                    item.USEDSTATE_NAME = item.USED_STATE != null && item.USED_STATE > 0 ? Enums.Description((PAGE_USED_STATE)item.USED_STATE) : string.Empty;
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

        public Result GetNodes()
        {
            var result = new Result();
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.PageRepository.QueryAll();
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search pages unsuccessfully!";
                        return result;
                    }
                    var resultData = GetNodes(data, null);

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = resultData;
                    result.Message = "Search pages successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search pages unsuccessfully!";
                return result;
            }
            return result;
        }

        //public List<Model.Node> GetNodes()
        //{
        //    var dataList = new List<Model.Node>();
        //    try
        //    {
        //        using (IUnitOfWork unitOfWork = new UnitOfWork())
        //        {
        //            var data = unitOfWork.PageRepository.QueryAll();
        //            if (data == null)
        //            {
        //                return null;
        //            }
        //            dataList = GetNodes(data, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex);
        //        return null;
        //    }
        //    return dataList;
        //}

        private List<TBL_SYS_PAGES> GetChildren(IQueryable<TBL_SYS_PAGES> dataList, int? parentId)
        {
            var dataLst = new List<TBL_SYS_PAGES>();
            var objs = dataList.Where(f => f.PARENT_ID == parentId).OrderBy(o => o.ID);

            foreach (var item in objs)
            {
                dataLst.Add(item);
                var subdata = GetChildren(dataList, item.ID);
                dataLst.AddRange(subdata);
            }

            return dataLst;
        }

        private List<Model.Node> GetNodes(IQueryable<TBL_SYS_PAGES> dataList, int? parentId)
        {
            var dataLst = new List<Model.Node>();
            var objs = dataList.Where(f => f.PARENT_ID == parentId).OrderBy(o => o.ID);
            Model.Node node;
            foreach (var item in objs)
            {
                node = new Model.Node();
                node.CODE = item.CODE;
                node.NAME = item.NAME;
                node.NAME_VI = item.NAME_VI;
                node.NAME_EN = item.NAME_EN;
                node.URL = item.URL;
                node.ICON = item.ICON;
                node.PARENT_ID = item.PARENT_ID;
                node.ORDER = item.ORDER;
                node.USED_STATE = item.USED_STATE;
                node.Nodes = GetNodes(dataList, item.ID);

                dataLst.Add(node);
            }

            return dataLst;
        }
        #endregion
    }
}
