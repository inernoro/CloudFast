
var apiHelper = {
    //可以直接在浏览器测试，若是不想写回调函数，设置为true则将回调值自动输出到控制台
    isInputErrorLog: false,
    urlPath: "/api/services/app/",
    defaultType: "POST",
    defaultDataType: "JSON",
    debug: false,
    defaultIsAsync: false,
    isAsync: false,
    isJsonParameter: true,
    data: {},
    cloud: {},
    local: {}
};


apiHelper.ajax = function (ajaxObject) {
    if (apiHelper.debug) {
        apiHelper.isInputErrorLog = true;
    }
    var datas = "data" in ajaxObject ? ajaxObject.data : {};
    apiHelper.isInputErrorLog && console.log(ajaxObject);
    if (!("url" in ajaxObject)) {
        if (!("controllerName" in ajaxObject) || !("actionName" in ajaxObject))
            throw new Error(apiHelper.ErrorArray(0));
        ajaxObject["url"] = ("areasName" in ajaxObject ? ajaxObject.areasName : "");
        ajaxObject.url = ajaxObject.url + ajaxObject.controllerName + "/" + ajaxObject.actionName;
        apiHelper.isInputErrorLog && console.log("url====>" + ajaxObject.url);
        debugger;
    }
    var ajaxObj = {
        url: apiHelper.urlPath + ajaxObject.url,
        data: apiHelper.isJsonParameter ? JSON.stringify(datas) : datas,
        type: "type" in ajaxObject ? ajaxObject.type : apiHelper.defaultType,
        dataType: "dataType" in ajaxObject ? ajaxObject.dataType : apiHelper.defaultDataType,
        contentType: "application/json",
        success: function (returnData) {
            if (apiHelper.debug)
                apiHelper.data = returnData;
            apiHelper.isInputErrorLog && console.log(returnData);
            if ("success" in ajaxObject && typeof (ajaxObject.success) === "function")
                if (returnData.success) {
                    ajaxObject.success(returnData.result);
                    return;
                } else {
                    if ("error" in ajaxObject && typeof (ajaxObject.error) === "function")
                        ajaxObject.error(returnData.error.message);
                    if (apiHelper.isInputErrorLog) {
                        console.log("ERROR", returnData.error.message);
                        return;
                    }
                }
            console.log(returnData);
        },
        error: function (resual, text) {
            if ("error" in ajaxObject && typeof (ajaxObject.error) === "function")
                ajaxObject.error(resual, text);
            if (apiHelper.isInputErrorLog) {
                console.log("ERROR", resual, text);
            }
        }
    };
    if (apiHelper.debug)
        console.log(ajaxObj);
    $.ajax(ajaxObj);
}

//错误列表
apiHelper.ErrorArray = function (number) {
    var errorArray = [
        { 0: "遇到一个错误，您可能没有填写url或者是 【controlName，actionName】 中的一项" },
        { 1: "参数错误，用户Key不能为空 !" },
        { 2: "ERROR ! 手机号码和验证码其中一项不能为空" }
    ];
    if (!arguments[1]) return errorArray[number][number];
    throw new Error(arguments[1]);
}

//Get 请求
apiHelper.getSingle = function (url, callbackFunction) {

    apiHelper.isInputErrorLog && console.log(url, callbackFunction);
    $.get(apiHelper.urlPath + url, callbackFunction);
}

//适合单页面使用
apiHelper.Post = function (url, data, callback) {
    apiHelper.isInputErrorLog && console.log(url, data, callback);
    apiHelper.ajax({
        url: url,
        data: data,
        success: callback
    });
}