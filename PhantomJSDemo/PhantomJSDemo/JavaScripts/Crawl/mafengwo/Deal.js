var page = require('webpage').create();
var system = require("system");
var fs = require("fs");
var pageParams = {
    rootDir: "",
    isDebug: false,
    jquery: "",
    url: ""
};


/*加载cookies*/
function loadCookies(page, url) {
    var host = getHostByUrl(url);
    var path = pageParams.rootDir + "\\cookies\\" + host + ".js";
    var file = fs.open(path, 'a+');
    var cookies = file.read();
    if (cookies == "")
        return;
    cookies = JSON.parse(cookies);
    for (var index = 0; index < cookies.length; index++)
        page.addCookie(cookies[index]);
}

/*缓存cookies*/
function cacheCookies(page, url) {
    var host = getHostByUrl(url);
    var cookies = JSON.stringify(page.cookies);
    fs.write(pageParams.rootDir + "\\cookies\\" + host + ".js", cookies, 'a+');
}

/*根据url获取Host*/
function getHostByUrl(url) {
    try {
        var t = url.substr(url.indexOf("//") + 2);
        if (t.indexOf("/"))
            t = t.substr(0, t.indexOf("/"));
        return t.toLowerCase();
    } catch (e) {
        console.error(e);
        return "";
    }
}

/*返回方法*/
function doResult(result) {
    if (typeof result !== "string")
        result = JSON.stringify(result)
    console.log("<`R>" + result + "</~R>");
}


/*数据抓取*/
function grab(args) {
    var result = {
        productId: $.trim(_SALES_INFO_GLOBAL_.sales_id),//商品Id
        productName: $.trim(_SALES_INFO_GLOBAL_.sales_name),//商品名称
        startingPrice: $.trim(_SALES_INFO_GLOBAL_.price),//起价
        departure: "",//出发城市
        arrivalCity: "",//目的地国家
        arrivalCountry: "",//目的地城市
        supplier: $.trim($(".ota-link a").attr("title")),//供应商
        pv: $.trim($(".see em").html()),//访问量
        soldCount: $.trim($(".see em:eq(1)").html()),//已售数量
        air: $.trim($(".flight-info tbody:first tr:first td:eq(1)").html()),//航司
        collect: $.trim($(".num_fav_total").html())//收藏次数
    };
    var contents = $("#target_goods_info").next().next().text();
    var temp1 = contents.substr(contents.indexOf("出发地：") + 4);
    var temp2 = temp1.substr(0, temp1.indexOf("目的地："));
    result.departure = $.trim(temp2);
    var temp3 = temp1.substr(temp1.indexOf("目的地：") + 4);
    var temp4 = temp3.substr(0, temp3.indexOf("预订时间："));
    result.arrivalCity = temp4;
    return result;
}

/*请求页面*/
function open() {
    /*加载页面*/
    page.open(pageParams.url, function (status) {
        if (status !== "success") {
            console.log('FAIL to load the address', url);
            phantom.exit();
            return;
        }
        cacheCookies(page, pageParams.url);
        if (pageParams.isDebug) {
            var title = page.evaluate(function () {
                return _SALES_INFO_GLOBAL_.sales_id;
            });
            page.render(title + ".jpg");
        }
        var result = page.evaluate(grab);
        doResult(result);
        phantom.exit()
    });
}

/*初始化*/
function init() {
    //解析页面输入参数
    pageParams = JSON.parse(system.stdin.readLine());
    /*测试环境下输出页面控制台log*/
    if (pageParams.isDebug) {
        page.onConsoleMessage = function (msg) {
            console.log('PCL:', msg);
        };
    }
    phantom.onError = function (msg, trace) {
        console.error(msg, trace);
    }
    loadCookies(page, pageParams.url);
    open();
}

//初始化
init();
