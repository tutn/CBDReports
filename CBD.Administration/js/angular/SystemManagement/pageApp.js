//var app = angular.module("app", ['trNgGrid']);
app.controller("pageController", pageController);
app.$inject = ["$scope", "cbdService"];

function pageController($scope, cbdService) {
    $scope.pageNumber = 0;
    $scope.pageSize = 10;
    $scope.orderby = "";
    $scope.isdesc = false;

    $scope.Items = [];
    $scope.Total = 0;
    $scope.IsEdit = false;

    $scope.Status = [];
    $scope.Pages = [];

    $scope.init = function () {
        $scope.UsedState();
        $scope.getAllPages('');
        $scope.clear(false);
    }

    $scope.clear = function (isreset) {
        $scope.sCode = '';
        $scope.sName = '';
        $scope.sOrder = '';
        if (isreset === true) {
            $scope.sParentid = undefined;
            $scope.sStatus = undefined;
        }

        $scope.parentid = '';
        $scope.code = "";
        $scope.name = "";
        $scope.namevi = "";
        $scope.nameen = "";
        $scope.url = "";
        $scope.icon = "";
        $scope.order = "";
        $scope.status = "";
    }

    $scope.changeCode = function (code) {
        $scope.code = code.toUpperCase();
    }

    $scope.setPageSize = function (pageSize) {
        $scope.pageSize = pageSize;
    }

    $scope.addModel = function () {
        $scope.clear();
        $('#dialogModal').modal('show');
        $scope.IsEdit = false;
    }

    $scope.UsedState = function () {
        $scope.Status.push({ Value: 1, Name: 'Actived' });
        $scope.Status.push({ Value: 2, Name: 'Inactived' });
        $scope.Status.push({ Value: 3, Name: 'New' });
        $scope.Status.push({ Value: 4, Name: 'Demo' });
    }

    //Called from on-data-required directive.
    $scope.GenerateData = function (currentPage, pageItems) {
        $scope.pageNumber = currentPage;
        $scope.getData();
    }

    $scope.search = function () {
        $scope.getData();
    }

    $scope.getData = function () {
        ShowLoader();
        let options = {
            CODE: $scope.sCode, NAME: $scope.sName, PARENT_ID: $scope.sParentid === null || $scope.sParentid === undefined ? null : $scope.sParentid, ORDER: $scope.sOrder, USED_STATE: $scope.sStatus === null || $scope.sStatus === undefined ? 0 : $scope.sStatus, PageSize: $scope.pageSize, PageNumber: $scope.pageNumber, OrderBy: $scope.orderby, IsDesc: $scope.isdesc
        };
        cbdService.getData(options).then(function (d) {
            $scope.Items = d.data.Data;
            $scope.Total = d.data.Total;
            // Success
            HideLoader();
        }, function (status) {
            HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.editRecord = function (record) {
        $scope.IsEdit = true;
        ShowLoader();
        $scope.getAllPages(record.ID);
        HideLoader();
        $scope.id = record.ID;
        $scope.code = record.CODE;
        $scope.name = record.NAME;
        $scope.namevi = record.NAME_VI;
        $scope.nameen = record.NAME_EN;
        $scope.url = record.URL;
        $scope.icon = record.ICON;
        $scope.parentid = record.PARENT_ID;
        $scope.order = record.ORDER;
        $scope.status = record.USED_STATE;

        $('#dialogModal').modal('show');
    }

    $scope.addOrUpdate = function () {
        let record = {
            ID: $scope.IsEdit === true ? $scope.id : 0,
            CODE: $scope.code,
            NAME: $scope.name,
            NAME_VI: $scope.namevi,
            NAME_EN: $scope.nameen,
            URL: $scope.url,
            ICON: $scope.icon,
            PARENT_ID: $scope.parentid,
            ORDER: $scope.order,
            USED_STATE: $scope.status,
        }

        if ($scope.IsEdit) {
            $scope.update(record);
        }
        else {
            $scope.add(record);
        }
        $scope.getAllPages();
        $scope.clear();
    };

    $scope.add = function (record) {

        ShowLoader();
        cbdService.AddRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //$scope.Items.unshift(d.data.Data);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'Page has been added successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.update = function (record) {
        ShowLoader();
        cbdService.UpdateRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //var currentRecord = _.first(_.where($scope.Items, { "ID": record.ID }));
                //var index = $scope.Items.indexOf(currentRecord);
                //$scope.Items[index] = angular.copy(record);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'Page has been updated successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.deleteRecord = function (record) {
        ShowLoader();
        cbdService.DeleteRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //var index = $scope.Items.indexOf(record);
                //if (index > -1) {
                //    $scope.Items.splice(index, 1);
                //}
                $scope.getData();
                DisplayMessage('Success', 'Page has been deleted successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.getAllPages = function (record) {
        //ShowLoader();
        cbdService.GetAllPages(record).then(function (d) {
            // Success
            //HideLoader();
            $scope.Pages = d.data.Data;
        }, function (status) {
            //HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }
}

app.factory('cbdService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.getData = function (options) {
        return $http.get(baseUrl + '/api/Page/Search?CODE=' + options.CODE + '&NAME=' + options.NAME + '&PARENT_ID=' + options.PARENT_ID + '&ORDER=' + options.ORDER + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    fac.AddRecord = function (record) {
        return $http.post(baseUrl + '/api/Page/Add', record);
    }
    fac.UpdateRecord = function (record) {
        return $http.post(baseUrl + '/api/Page/Update', record);
    }
    fac.DeleteRecord = function (record) {
        return $http.post(baseUrl + '/api/Page/Delete', record);
    }
    fac.GetAllPages = function (PageId) {
        return $http.get(baseUrl + '/api/Page/GetAllPages?PageId=' + PageId);
    }
    return fac;
}]);
