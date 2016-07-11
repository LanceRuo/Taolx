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
        productId: $.trim(lid),//商品Id
        productName: $.trim($(".fontYaHei").html()),//商品名称
        startingPrice: $.trim($(".after-price em").html()),//起价
        departure: "",//出发城市
        arrivalCity: "",//目的地国家
        arrivalCountry: "",//目的地城市
        supplier: "",//供应商
        pv: $.trim($(".gallery-bottom p:first span").html()),//访问量
        soldCount: $.trim($(".gallery-bottom p:eq(1) span").html()),//已售数量
        air: $.trim($(".triffc-company p").html()),//航司
        collect: 0//收藏次数
    };
    var contents = $(".sub-content .p-cont");
    result.departure = $.trim($(contents[0]).html());
    //计算到达城市和到达国家
    var arrival = $(contents[1]);
    var arrivalCity = "";
    var arrivalCountry = "";
    $(contents[1]).find("a").each(function () {
        result.arrivalCity += $.trim($(this).html()) + ","
    });
    result.arrivalCity = result.arrivalCity.substring(0, result.arrivalCity.length - 1);
    var lastDH = result.arrivalCity.lastIndexOf(",");
    result.arrivalCountry = result.arrivalCity.substr(lastDH + 1);
    result.arrivalCity = result.arrivalCity.substr(0, lastDH);
    //供应商
    result.supplier = $.trim($(contents[3]).find("a").html());
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
                return lid;
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
