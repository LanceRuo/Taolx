var page = require('webpage').create();
var system = require("system");
var fs = require("fs");
var pageParams = {
    rootDir: "",
    isDebug: false,
    jquery: "",
    url: ""
};

/*保存屏幕截图*/
function savePageScreen(name) {
    if (!pageParams.isDebug) return; 
    var activityId = pageParams.url.substr(0, pageParams.url.lastIndexOf("/"));
    activityId = activityId.substr(activityId.lastIndexOf("/") + 1);
    page.render("PageScreen\\Qyer\\Activity\\" + activityId + "_" + name);
}

/*加载cookies*/
function loadCookies(page, url) {
    var host = getHostByUrl(url);
    var path = pageParams.rootDir + "\\JavaScripts\\Cookies\\" + host + ".js";
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
    var path = pageParams.rootDir + "\\JavaScripts\\Cookies\\" + host + ".js";
    fs.write(path, cookies, 'w');
}

/*根据url获取Host*/
function getHostByUrl(url) {
    try {
        var t = url.substr(url.indexOf("//") + 2);
        if (t.indexOf("/"))
            t = t.substr(0, t.indexOf("/"));
        t = t.replace("www.", "");
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
    var links = [];
    $(".content-list li a").each(function () {
        links.push(this.href);
    });
    return links;
}

/*请求页面*/
function open() {
    /*加载页面*/
    page.open(pageParams.url, function (status) {
        savePageScreen("init.jpg");
        if (status !== "success") {
            console.log('FAIL to load the address', url);
            phantom.exit();
            return;
        }
        //cacheCookies(page, pageParams.url);
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
    //不加载图片
    page.settings.loadImages = false;
    //超时时间 5000ms
    page.settings.resourceTimeout = 5000;
    //加载错误事件
    phantom.onError = function (msg, trace) {
        console.error(msg, trace);
        phantom.exit(1);
    }
    /*测试环境下输出页面控制台log*/
    if (pageParams.isDebug) {
        page.onConsoleMessage = function (msg) {
            console.log('PCL:', msg);
        };
    }
    //loadCookies(page, pageParams.url);
    open();
}


//初始化
init();
