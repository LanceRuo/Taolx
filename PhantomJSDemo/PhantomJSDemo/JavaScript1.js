var page = require('webpage').create();
var system = require("system");
var pageParams = { rootDir: "", env: "test", jquery: "" };
var url = 'https://accounts.taolx.com';
pageParams = JSON.parse(system.stdin.readLine());

/*返回方法*/
var doResult = function (result) {
    if (typeof result !== "string")
        result = JSON.stringify(result)
    console.log("<`R>" + result + "</~R>");
}

/*数据抓取*/
var grab = function (args) {
    var result = { s: "", b: "" };
    return document.cookie;
}

/*测试环境下输出页面控制台log*/
if (pageParams.env == "test") {
    page.onConsoleMessage = function (msg) {
        console.log('PCL:', msg);
    };
}

/*加载页面*/
page.open(url, function (status) {
    if (status !== "success") {
        console.log('FAIL to load the address', url);
        phantom.exit();
        return;
    }
    /*加载jQuery*/
    page.includeJs(pageParams.jquery, function () {
        var result = page.evaluate(grab);
        doResult(result);
        phantom.exit()
    });
});


